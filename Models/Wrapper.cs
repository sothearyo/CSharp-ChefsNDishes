using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace ChefsDishes.Models
{
    public class Wrapper
    {
        public List<Dish> AllDishes {get;set;}
        public List<Chef> AllChefs {get;set;}
    }
}