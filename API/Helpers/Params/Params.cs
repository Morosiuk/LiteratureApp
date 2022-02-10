namespace API.Helpers.Params
{
    public abstract class Params
    {
      private const int MaxPageSize = 50;
      private int _pageSize = 10;
      public string OrderBy { get; set; }
      public int PageNumber { get; set; } = 1;
      public string Keyword { get; set; } = "";
      public int PageSize {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
      }
    }
}
