using Microsoft.OpenApi.Models;

namespace WebApi;

public static class Swagger
{
  public static void BuildSwagger(WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(setup =>
    {
      var configs = builder.Configuration.GetSection("Configs").GetSection("App");

      setup.SwaggerDoc(
          "v1",
          new OpenApiInfo()
          {
            Title = configs.GetValue<string>("Title"),
            Description = configs.GetValue<string>("Description"),
            Version = $"v{configs.GetValue<string>("Version")}",
            Contact = new OpenApiContact()
            {
              Name = configs.GetSection("Author").GetValue<string>("Name"),
              Email = configs.GetSection("Author").GetValue<string>("Email"),
              Url = new Uri(configs.GetSection("Author").GetValue<string>("Url") ?? "")
            },
            License = new OpenApiLicense()
            {
              Name = configs.GetSection("License").GetValue<string>("Name"),
              Url = new Uri(configs.GetSection("License").GetValue<string>("Url") ?? "")
            }
          }
      );

      setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
      {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token jwt utilizado no Authorization."
      });

      setup.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          },
          new string[] {}
        }
      });
    });
  }

  public static void UseSwagger(WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        var configs = app.Configuration.GetSection("Configs").GetSection("App");

        options.DocumentTitle = configs.GetValue<string>("Title");
      });
    }
  }
}
