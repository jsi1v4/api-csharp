using WebApi;
using WebApi.Core;
using WebApi.Database;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

#region Services
builder.Services.AddSingleton(configuration);

builder.Services.AddDatabase();
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer");

builder.Services.AddCustomSwagger(configuration);
#endregion

var app = builder.Build();

app.UseAuthorization();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
  app.UseCustomSwagger(configuration);
}

app.MapControllers();

app.Run();
