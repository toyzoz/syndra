# ASP.NET WEB API 开发规范

## 1. 控制器规范

### 控制器命名

- 控制器名称以复数形式并以`Controller`结尾，例如`UsersController`。

### 控制器设计

- 每个控制器负责一个资源的操作，例如`UsersController`负责用户相关的操作。
- 避免在控制器中编写业务逻辑，业务逻辑应封装在服务层。
- 控制器方法应尽量保持简洁，避免过多的参数，建议使用 DTO（数据传输对象）封装请求和响应数据。
- 使用依赖注入（DI）获取服务实例，避免直接实例化服务。

### 方法命名

- 使用动词+资源的形式，例如`GetUserAsync`、`CreateOrderAsync`。
- 在控制器中可以省略资源名称，例如在`UsersController`中，方法可以命名为`GetListAsync`、`GetByIdAsync`、`CreateAsync`、`DeleteAsync`、`UpdateAsync`。
- 异步方法必须带上`Async`后缀。
- **`Get`与`Find`的语义区别**：
  - `Get`：用于获取明确存在的资源，通常会返回资源或抛出异常（如资源未找到）。
  - `Find`：用于尝试查找资源，可能返回资源或`null`，表示资源可能不存在。

## 2. 路由规则

### RESTful 路由

- 使用 RESTful 风格的路由：
  - `GET /api/users` 获取用户列表
  - `GET /api/users/{id}` 获取指定用户
  - `POST /api/users` 创建用户
  - `PUT /api/users/{id}` 更新用户
  - `DELETE /api/users/{id}` 删除用户

### 路由命名

- 路由中的多个单词应以`-`连接，例如：
  - `PUT /api/pull-requests/{id}` 更新拉取请求
  - 避免使用驼峰命名或下划线，例如`/api/userProfiles`或`/api/user_profiles`。

### 嵌套路由

- 路由尽量简洁，避免嵌套过深。

#### 嵌套过深的反例

以下是一个嵌套过深的路由反例：

```
GET /api/users/{userId}/orders/{orderId}/items/{itemId}
```

这种路由会导致复杂性增加，建议将其拆分为多个独立的资源路由，例如：

```
GET /api/orders/{orderId}/items/{itemId}
```

## 3. 错误处理

### HTTP 状态码

- 使用标准的 HTTP 状态码：
  - `200 OK`：请求成功
  - `400 Bad Request`：请求参数错误
  - `401 Unauthorized`：未授权
  - `404 Not Found`：资源未找到
  - `500 Internal Server Error`：服务器内部错误

### 错误响应格式

- 返回统一的错误响应格式，例如：
  ```json
  {
    "code": 400,
    "message": "Invalid request parameters",
    "details": "The 'email' field is required."
  }
  ```

### 全局异常处理

- 在全局异常处理中间件中捕获未处理的异常，并返回友好的错误信息。

## 4. Minimal API 规范

### 适用场景

- Minimal API 适用于简单的 API 项目或微服务，复杂项目建议使用传统控制器模式。

### 命名规则

- 路由名称应清晰且符合 RESTful 风格，例如：
  ```csharp
  app.MapGet("/api/users", GetUsers);
  app.MapPost("/api/users", CreateUser);
  ```

### 中间件使用

- 确保在管道中正确配置中间件，例如认证、授权和异常处理。

### 依赖注入

- 通过 `builder.Services` 注册依赖项，并在处理程序中使用构造函数注入。

### Minimal API 示例

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/users", () => new[] { "User1", "User2" });
app.MapPost("/api/users", (User user) => Results.Created($"/api/users/{user.Id}", user));

app.Run();

record User(int Id, string Name);
```

## 5. 其他建议

- 使用依赖注入（DI）管理服务和资源。
- 开启 Swagger 文档以便于 API 测试和维护。
- 定期进行代码审查，确保代码质量。
- 编写单元测试和集成测试，确保代码的正确性和稳定性。
- 遵循 SOLID 原则设计代码，提升可维护性和扩展性。
- 使用日志记录框架（如 Serilog 或 NLog）记录关键操作和错误信息。
