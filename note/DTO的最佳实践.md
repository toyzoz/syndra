# 数据传输对象（DTO）最佳实践与规范

## 1. DTO 的命名规范

- DTO 类名应以 `Dto` 结尾，例如 `UserDto`、`OrderDto`。
- 对于请求和响应的 DTO，可使用前缀区分，例如 `CreateUserRequestDto`、`UserResponseDto`。

## 2. DTO 的分层设计

- 根据功能模块划分 DTO，例如：
  - `UserManagement.UserDto`
  - `OrderManagement.OrderDto`
- 避免将所有 DTO 放在一个文件夹中，保持项目结构清晰。

## 3. 继承预构建的 DTO

- 尽量继承预构建的 DTO 基类，例如 `BaseDto`，以便统一管理通用属性（如 `Id`、`CreatedDate` 等）。
- 避免重复定义通用字段，提升代码复用性。

## 4. 使用数据注释验证 DTO 的属性

- 使用数据注释（Data Annotations）对 DTO 属性进行验证，例如：

  ```csharp
  public class UserDto
  {
      [Required]
      [StringLength(50)]
      public string Name { get; set; }

      [EmailAddress]
      public string Email { get; set; }
  }
  ```

- 对于复杂的验证逻辑，建议实现 `IValidatableObject` 接口：

  ```csharp
  public class CustomDto : IValidatableObject
  {
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }

      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
          if (StartDate > EndDate)
          {
              yield return new ValidationResult("StartDate cannot be later than EndDate.");
          }
      }
  }
  ```

## 5. 避免在 DTO 中添加业务逻辑

- DTO 应仅用于数据传输，不应包含任何业务逻辑。
- 将业务逻辑封装在服务层或领域模型中，保持 DTO 的职责单一。

## 6. DTO 的数据类型

- **使用基础数据类型**：优先使用基础数据类型（如 `int`、`string`、`bool`）而非复杂类型。
- **避免使用数据库特定类型**：例如，避免直接使用 `SqlDateTime`，而应使用 `DateTime`。
- **使用可空类型**：对于可能为空的字段，使用可空类型（如 `int?`、`bool?`）。
- **枚举类型**：对于固定值的字段，优先使用枚举类型，并在文档中说明枚举值的含义。
  ```csharp
  public enum OrderStatus
  {
      Pending = 0,
      Completed = 1,
      Cancelled = 2
  }
  ```
- **集合类型**：优先使用通用集合类型（如 `List<T>`、`IEnumerable<T>`），避免使用数组。
- **日期时间类型**：统一使用 `DateTime` 或 `DateTimeOffset`，并明确时区信息。
  - 推荐使用 `DateTimeOffset` 以避免时区问题。
  ```csharp
  public DateTimeOffset CreatedAt { get; set; }
  ```
- **布尔类型**：对于布尔值字段，命名应清晰表达其含义，例如 `IsActive`、`HasPermission`。

## 7. DTO 的序列化

- 确保 DTO 可正确序列化和反序列化，避免使用不支持序列化的类型（如 `DbContext`）。
- 使用 `JsonProperty` 或类似注解自定义序列化字段名称：
  ```csharp
  public class ProductDto
  {
      [JsonProperty("product_name")]
      public string Name { get; set; }
  }
  ```
- 对于性能敏感的场景，使用高效的序列化工具（如 `System.Text.Json` 或 `Newtonsoft.Json`）。

## 8. DTO 的映射

- 使用自动映射工具（如 AutoMapper）简化实体与 DTO 之间的转换：
  ```csharp
  var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
  var mapper = config.CreateMapper();
  var userDto = mapper.Map<UserDto>(user);
  ```
- 避免手动映射，减少代码冗余和错误风险。

## 9. DTO 的版本管理

- 在 API 版本升级时，创建新的 DTO 版本，例如 `UserDtoV1`、`UserDtoV2`。
- 确保旧版本的 DTO 仍然可用，以支持向后兼容。

## 10. DTO 的文档化

- 使用 XML 注释或 Swagger 为 DTO 添加文档说明，便于开发者理解其用途和字段含义：
  ```csharp
  /// <summary>
  /// 用户数据传输对象
  /// </summary>
  public class UserDto
  {
      /// <summary>
      /// 用户名
      /// </summary>
      public string Name { get; set; }
  }
  ```

## 11. 其他建议

- 定期审查 DTO，确保其字段与实际需求一致，避免冗余字段。
- 避免在 DTO 中使用复杂的嵌套结构，保持其简单性和可读性。
