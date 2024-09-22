using Microsoft.Data.SqlClient;

namespace WebApi.Database;

public class DB
{
  private ConfigurationManager _configuration;

  public DB(
    ConfigurationManager configuration
  )
  {
    _configuration = configuration;
  }

  public SqlConnection Conn()
  {
    var connectionString = _configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
  }
}
