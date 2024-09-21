using WebApi;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

Swagger.BuildSwagger(builder);
#endregion

#region App
var app = builder.Build();

Swagger.UseSwagger(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
