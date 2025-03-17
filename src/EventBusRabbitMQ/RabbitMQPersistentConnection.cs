using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ;

public class RabbitMQPersistentConnection(IConnectionFactory factory,
ILogger<RabbitMQPersistentConnection> logger)
: IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _factory = factory;
    private readonly ILogger<RabbitMQPersistentConnection> _logger = logger;
    private IConnection _connection;
    private bool _disposed;

    private readonly object _lock = new();

    private readonly int _retryCount = 10; // 可配置化

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public bool TryConnect()
    {
        lock (_lock)
        {
            var policy = Policy
                .Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        _logger.LogWarning(ex,
                            "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})",
                            $"{time.TotalSeconds:n1}", ex.Message);
                    });

            policy.Execute(async () => { _connection = await _factory.CreateConnectionAsync(); });

            if (IsConnected)
            {
                _connection.ConnectionShutdownAsync += OnConnectionShutdownAsync;
                _connection.CallbackExceptionAsync += OnCallbackExceptionAsync;
                _connection.ConnectionBlockedAsync += OnConnectionBlockedAsync;
                _logger.LogInformation("RabbitMQ persistent connection acquired");
                return true;
            }

            _logger.LogCritical("FATAL ERROR: RabbitMQ persistent connection could not be created");
            return false;
        }
    }

    /// <summary>
    /// 连接阻塞事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="event"></param>
    /// <returns></returns>
    private Task OnConnectionBlockedAsync(object sender, ConnectionBlockedEventArgs @event)
    {
        if (_disposed) return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection is blocked. Trying to re-connect...");
        TryConnect();
        return Task.CompletedTask;
    }
    /// <summary>
    /// 回调异常事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="event"></param>
    /// <returns></returns>
    private Task OnCallbackExceptionAsync(object sender, CallbackExceptionEventArgs @event)
    {
        if (_disposed) return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection threw an exception. Trying to re-connect...");
        TryConnect();
        return Task.CompletedTask;
    }
    /// <summary>
    /// 连接关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="event"></param>
    /// <returns></returns>
    private Task OnConnectionShutdownAsync(object sender, ShutdownEventArgs @event)
    {
        if (_disposed) return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection is shutting down. Trying to re-connect...");
        TryConnect();
        return Task.CompletedTask;
    }
    /// <summary>
    /// 创建模型
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<IChannel> CreateChannleAsync()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return await _connection.CreateChannelAsync();
    }
}