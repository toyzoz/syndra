using Catalog.API.Data;
using Catalog.API.Extensions;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
await app.UseDataSeedAsync();

app.UseAuthorization();

app.MapControllers();

app.Run();