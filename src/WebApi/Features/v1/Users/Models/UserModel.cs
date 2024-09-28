namespace WebApi.Features.v1.Users.Models;

public class UserModel
{
  public int Id { get; set; }
  public string Name { get; set; } = "";
  public DateTime? Birthday { get; set; }
  public string? Photo { get; set; }
}
