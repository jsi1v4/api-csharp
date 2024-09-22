using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer");
builder.Services.AddControllers();

Swagger.BuildSwagger(builder);

var app = builder.Build();

Swagger.UseSwagger(app);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
