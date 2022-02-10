namespace API.Helpers.Params
{
  public class LiteratureParams : Params
  {
    public string Symbol { get; set; } = "";
    public bool MultipleEditions { get; set; }
    public LiteratureParams()
    {
      OrderBy = "name";
    }
  }
}
