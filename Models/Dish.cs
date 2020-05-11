using System.ComponentModel.DataAnnotations;
using System;
namespace ChefsDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required(ErrorMessage="Please enter name of dish.")]
        [Display(Name="Dish Name:")]
        public string Name {get;set;}

        [Required(ErrorMessage="Please select tastiness level.")]
        [Range(1,5)]
        [Display(Name="Tastiness:")]
        public string Tastiness {get;set;}

        [Required(ErrorMessage="Please enter calories.")]
        [Range(0,10000)]
        [Display(Name="Calories:")]
        public int Calories {get;set;}

        [Required(ErrorMessage="Please enter a description.")]
        [Display(Name="Description:")]
        public string Description {get;set;}

        [Required(ErrorMessage="Please select a chef")]
        [Display(Name="Name of Chef:")]

        // Foreign key to Chef
        public int ChefId {get;set;}
        
        // Navigation property: This is not actually tranlated to the database
        public Chef DishChef {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}