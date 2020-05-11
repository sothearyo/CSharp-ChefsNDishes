using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsDishes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        // -------------- Render Views -----------------------------
        // Show All Chefs
        [HttpGet("")]
        public ViewResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(c => c.DishesMade)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
            return View(AllChefs);
        }
        // Show All Dishes
        [HttpGet("dishes")]
        public ViewResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes
                .Include(d => d.DishChef)
                .OrderByDescending(d => d.CreatedAt)
                .ToList();
            return View(AllDishes);
        }
        // Show Page to create new dish
        [HttpGet("new_dish")]
        public ViewResult NewDish()
        {
            // Wrapper wrap = new Wrapper();
            // wrap.AllChefs = dbContext.Chefs.ToList();
            // wrap.AllDishes = dbContext.Dishes.ToList();
            ViewBag.AllChefs = dbContext.Chefs;
            return View();
        }
        // Show Page to create new chef
        [HttpGet("new_chef")]
        public ViewResult NewChef()
        {
            return View();
        }
        // --------------- Methods ----------------------------
        // Method to add a chef
        [HttpPost("add_chef")]
        public IActionResult AddChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewChef");
            }
        }
        

        [HttpGet("{dish_id}")]
        public IActionResult OneDish(int dish_id)
        {
            Dish this_dish = dbContext.Dishes.SingleOrDefault(d => d.DishId == dish_id);
            return View(this_dish);
        }

        [HttpPost("add_dish")]
        public IActionResult AddDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewDish");
            }
        }

        [HttpGet("edit_dish/{dish_id}")]
        public IActionResult EditDish(int dish_id)
        {
            Dish this_dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == dish_id);
            return View(this_dish);
        }

        [HttpPost("update_dish/{dish_id}")]
        public IActionResult UpdateDish(int dish_id, Dish fromForm)
        {
            if (ModelState.IsValid)
            {
                fromForm.DishId = dish_id;
                dbContext.Update(fromForm);
                dbContext.Entry(fromForm).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();
                return RedirectToAction ("OneDish", new{dish_id = fromForm.DishId});
            }
            else
            {
                return View("EditDish", fromForm);
            }
        }
        
        [HttpGet("delete_dish/{dish_id}")]
        public IActionResult DeleteDish(int dish_id)
        {
            Dish retrievedDish = dbContext.Dishes.SingleOrDefault(d => d.DishId == dish_id);
            dbContext.Dishes.Remove(retrievedDish);
            dbContext.SaveChanges();
            return RedirectToAction ("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
