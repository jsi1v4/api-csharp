using Dapper;
using WebApi.Core;
using WebApi.Database;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users;

public class UsersRepository : Repository
{
  private readonly DBContext _database;

  public UsersRepository(DBContext database
  )
  {
    _database = database;
  }

  public async Task<List<UserModel>> SelectUsers(int? id = null, string? name = null)
  {
    IEnumerable<UserModel> results;

    try
    {
      using (var conn = _database.Conn())
      {
        var sql = @"SELECT * FROM ""User"" WHERE 1 = 1";

        if (id != null)
          sql += @" AND id = @ID";

        if (!String.IsNullOrEmpty(name))
          sql += @" AND name LIKE '%@NAME%'";

        var param = new
        {
          ID = id,
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
