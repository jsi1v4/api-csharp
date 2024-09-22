using Dapper;
using WebApi.Database;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users;

public class UsersRepository
{
  private DB _database;

  public UsersRepository(
    DB database
  )
  {
    _database = database;
  }

  public async Task<List<UserModel>> SelectUsers(string? name)
  {
    IEnumerable<UserModel> results;

    try
    {
      using (var conn = _database.Conn())
      {
        var sql = @"SELECT * FROM 'Users' WHERE 1 = 1";

        if (String.IsNullOrEmpty(name))
          sql += @" AND name LIKE '%@NAME%'";

        var param = new
        {
          NAME = name
        };

        results = await conn.QueryAsync<UserModel>(sql, param);
      }
    }
    catch
    {
      throw;
    }

    return results.AsList();
  }
}
