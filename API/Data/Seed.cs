using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class Seed
  {
    public static async Task SeedUsers(DataContext context)
    {
      //Check there are no configured values already in the database
      if (await context.Roles.AnyAsync()) return;

      //Create roles
      new List<Role>()
      {
        new Role() {Name = "Publisher", Description = "Publisher", Default = true},
        new Role() {Name = "Servant", Description = "Literature Servant", Admin = true},
        new Role() {Name = "Overseer", Description = "Congregation Overseer", Admin = true},
        new Role() {Name = "Coordinator", Description = "Literature Co-ordinator", Admin = true}
      }.ForEach(role => context.Roles.AddAsync(role));

      //Create congregations
      new List<Congregation>() 
      {
        new Congregation() {Name = "Wokingham", Code=30825},
        new Congregation() {Name = "Bracknell South", Code=30826},
        new Congregation() {Name = "Bracknell West", Code=30827}
      }.ForEach(cong => context.Congregations.AddAsync(cong));

      //Add users
      var newUsers = await System.IO.File.ReadAllTextAsync("Data/SeedData.Users.json");
      var users = JsonSerializer.Deserialize<List<AppUser>>(newUsers);
      foreach (var user in users)
      {
        using var hmac = new HMACSHA512();

        user.UserName = user.UserName.ToLower();
        user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
        user.PasswordSalt = hmac.Key;
        context.Users.Add(user);
      }
      
      await context.SaveChangesAsync();
    }
  }
}