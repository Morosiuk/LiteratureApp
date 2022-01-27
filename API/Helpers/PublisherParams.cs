namespace API.Helpers
{
    public class PublisherParams : Params
    {
      public string Congregation { get; set; }
      public PublisherParams()
      {
        OrderBy = "firstname";
      }
    }
}
