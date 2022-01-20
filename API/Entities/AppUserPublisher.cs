namespace API.Entities
{
    public class AppUserPublisher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
