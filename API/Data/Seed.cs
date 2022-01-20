using System;
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
        new Role() {Name = "Servant", Description = "Literature Servant"},
        new Role() {Name = "Overseer", Description = "Congregation Overseer"},
        new Role() {Name = "Coordinator", Description = "Literature Co-ordinator"}
      }.ForEach(role => context.Roles.AddAsync(role));

      //Create congregations
      new List<Congregation>() 
      {
        new Congregation() {Name = "Wokingham", Code=30825, 
          DateCreated=DateTime.Now.AddDays(-200)},
        new Congregation() {Name = "Bracknell South", Code=30826, 
          DateCreated=DateTime.Now.AddDays(-150)},
        new Congregation() {Name = "Bracknell West", Code=30827, 
          DateCreated=DateTime.Now.AddDays(-175)}
      }.ForEach(cong => context.Congregations.AddAsync(cong));

      //Import publishers
      var importFile = await System.IO.File.ReadAllTextAsync("Data/PublisherData.Users.json");
      var publishers = JsonSerializer.Deserialize<List<Publisher>>(importFile);
      context.Publishers.AddRange(publishers);

      await context.SaveChangesAsync();
    }
  }
}