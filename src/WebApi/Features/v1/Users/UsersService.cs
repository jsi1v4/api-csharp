using System;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users;

public class UsersService
{
  UsersRepository _repository;

  public UsersService(
    UsersRepository repository
  )
  {
    _repository = repository;
  }

  public async Task<List<UserModel>> FindUsers(string? name)
  {
    return await _repository.SelectUsers(name);
  }
}
