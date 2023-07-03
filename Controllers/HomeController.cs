using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BishesNDishes.Models;

namespace BishesNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;


    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.ToList();
        ViewBag.AllDishes = AllDishes;
        return View("Index");
    }
    [HttpGet("/dish/form")]
    public IActionResult Form()
    {
        return View("Form");
    }
    
        [HttpPost("/dish/create")]
    public IActionResult DishCreate(Dish newDish)
    {
        if (!ModelState.IsValid)
        {  
            return Form();

        }
        System.Console.WriteLine(newDish.Name);
        System.Console.WriteLine(newDish.Chef);
        System.Console.WriteLine(newDish.Tastiness);
        System.Console.WriteLine(newDish.Calories);
        System.Console.WriteLine(newDish.Description);
            // database things
        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("/dishes/{id}")]
    public IActionResult Display(int id)
    {
        Dish? OneDish = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        ViewBag.OneDish = OneDish;
        return View("Display");
    }



    [HttpGet("/dishes/{id}/edit")]
    public IActionResult DishEdit(int id)
    {
        Dish? OneDish = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        if (OneDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("DishEdit", OneDish);
    }

    [HttpPost("/dishes/{id}/update")]
    public IActionResult DishUpdate(Dish newDish, int id)
    {
        Dish? OldDish = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        if (OldDish == null)
        {
            return RedirectToAction("Index");
        }
        OldDish.Name = newDish.Name;
        OldDish.Chef = newDish.Chef;
        OldDish.Calories = newDish.Calories;
        OldDish.Tastiness = newDish.Tastiness;
        OldDish.Description = newDish.Description;
        OldDish.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index", id);
    }
    [HttpGet("/dishes/{id}/delete")]
    public IActionResult DishDelete(int id)
    {
        Dish? DishToDelete = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        if (DishToDelete == null)
        {
            return RedirectToAction("Index");
        }

        _context.Dishes.Remove(DishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
