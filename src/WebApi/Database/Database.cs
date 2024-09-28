using Dapper;
using Npgsql;

namespace WebApi.Database;

public static class Database
{
  public static IServiceCollection AddDatabase(this IServiceCollection services)
  {
    services.AddSingleton<DBContext>();

    return services;
  }
}

public class DBContext
{
  private ConfigurationManager _configuration;

  public DBContext(
    ConfigurationManager configuration
  )
  {
    _configuration = configuration;
  }

  public NpgsqlConnection Conn()
  {
    var connectionString = _configuration.GetConnectionString("DefaultConnection");

    return new NpgsqlConnection(connectionString);
  }
}
