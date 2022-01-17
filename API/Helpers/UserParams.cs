namespace API.Helpers
{
  public class UserParams : Params
  {

    public int Congregation { get; set; } = 0;
    public UserParams()
    {
      OrderBy = "firstname";
    }
  }
}