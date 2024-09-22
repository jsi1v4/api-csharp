namespace WebApi.Features.v1.Users.Models;

public class UserModel
{
  public int Id;
  public string Email = "";
  public string Name = "";
  public DateTime? Birthday;
  public string? Photo;
}
