using Basket.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc()
// .AddJsonTranscoding()
;

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new() { Title = "Basket.API", Version = "v1" });
// });

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/Swagger/v1/swagger.json", "My API V1");
//     });
// }

app.MapGrpcService<BasketService>();

app.Run();

public partial class Program;