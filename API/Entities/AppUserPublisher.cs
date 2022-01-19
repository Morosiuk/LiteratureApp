using System;

namespace API.Entities
{
    public class AppUserPublisher
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public Publisher Publisher { get; set; }
    }
}
