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
    public DbSet<LanguageCode> LanguageCodes { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestItem> RequestItems { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Literature>()
        .HasIndex(i => i.Symbol)
        .IsUnique();
      modelBuilder.Entity<LanguageCode>()
        .HasIndex(i => i.Code)
        .IsUnique();
    }
  }
}