#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace BishesNDishes.Models;

public class MyContext : DbContext
{
  public MyContext(DbContextOptions options) : base(options) { }

// for every table you will have another dbset
  public DbSet<Dish> Dishes {get;set;}
}