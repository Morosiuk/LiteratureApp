namespace API.DTOs
{
  public class UserToUpdateDto
  {
    public string UserName { get; set; }
    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool Admin { get; set; }
  }
}