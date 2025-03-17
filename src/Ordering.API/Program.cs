using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddApplicationService()
    .AddDatabase(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => { options.Servers = Array.Empty<ScalarServer>(); });
}

await app.ApplyMigrationsAsync();

// app.UseAuthorization();
app.MapControllers();

app.Run();

namespace Ordering.API
{
    public class Program;
}