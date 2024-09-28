using WebApi.Core;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users;

public class UsersService : Service
{
  private readonly UsersRepository _repository;

  public UsersService(UsersRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<UserModel>> FindUsers(string? name)
  {
    return await _repository.SelectUsers(null, name);
  }

  public async Task<UserModel> FindUniqueUser(int? id)
  {
    var users = await _repository.SelectUsers(id);

    return users.First();
  }
}
