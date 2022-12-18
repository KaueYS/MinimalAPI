
using Microsoft.AspNetCore.Builder;
using MinimalAPI.ApiEndPoints;
using MinimalAPI.AppServicesExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapAutenticationEndPoints();
app.MapCategoriaEndPoints();
app.MapProdutosEndPoints();

var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
