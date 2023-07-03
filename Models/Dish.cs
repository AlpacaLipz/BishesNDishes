#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BishesNDishes.Models;

public class Dish
{ // primary key -> class name + Id like DishId
  [Key]
  public int DishId {get;set;}
  [Required(ErrorMessage = "Name is required")]
  public string Name {get;set;}
  [Required(ErrorMessage = "Chef is required")]
  public string Chef {get;set;}
  [Range(1,5)]
  public int Tastiness {get;set;}
  [Range(0,Int32.MaxValue)]
  public int Calories {get;set;}
  [MinLength(10)]
  public string Description {get;set;}
  public DateTime CreatedAt {get;set;} = DateTime.Now;
  public DateTime UpdatedAt {get;set;} = DateTime.Now;
}