using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class DataContext : DbContext
  {
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Congregation> Congregations { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Literature> Literature { get; set; }
    public DbSet<LanguageCodes> LanguageCodes { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {

    }
  }
}