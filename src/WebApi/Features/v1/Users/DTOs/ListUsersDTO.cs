using Microsoft.AspNetCore.Mvc;

namespace WebApi.Features.v1.Users.DTOs;

public class ListUsersDTO
{
  /// <summary>
  /// Nome do usuario
  /// </summary>
  [FromQuery]
  public string? Name { get; set; }
}
