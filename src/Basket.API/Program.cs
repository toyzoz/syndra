using Basket.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

var app = builder.Build();


app.MapGrpcService<BasketService>();

app.Run();

public partial class Program;