using Ordering.API.Extensions;
using Ordering.Infrastructure.Extensions;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddApplicationService()
    .AddInfrastructureServices(builder.Configuration);
   

WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Servers = Array.Empty<ScalarServer>();
    });
}

await app.ApplyMigrationsAsync();

app.UseAuthorization();
app.MapControllers();

app.Run();