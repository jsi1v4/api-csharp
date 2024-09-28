using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebApi;

public static class Swagger
{
  public static IServiceCollection AddCustomSwagger(this IServiceCollection services, ConfigurationManager configManager)
  {
    services.AddSwaggerGen(setup =>
    {
      var configs = configManager.GetSection("Configs").GetSection("App");

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

      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

      setup.IncludeXmlComments(xmlPath);
    });

    return services;
  }

  public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, ConfigurationManager configManager)
  {
    app.UseSwagger(setup =>
    {
    });
    app.UseSwaggerUI(options =>
    {
      var configs = configManager.GetSection("Configs").GetSection("App");

      options.DocumentTitle = configs.GetValue<string>("Title");
    });

    return app;
  }
}
