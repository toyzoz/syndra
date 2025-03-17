using Basket.API.Services;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc()
// .AddJsonTranscoding()
    ;

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new() { Title = "Basket.API", Version = "v1" });
// });

WebApplication? app = builder.Build();

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
