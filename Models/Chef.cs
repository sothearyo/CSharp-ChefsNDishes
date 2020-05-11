using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace ChefsDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}

        [Required(ErrorMessage="Please enter a first name.")]
        [Display(Name="First Name:")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Please enter a last name.")]
        [Display(Name="Last Name:")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Please enter a birthdate.")]
        [Display(Name="Date of birth:")]
        [Date(ErrorMessage="Chef must be 18 years or older.")]
        public DateTime Birthdate {get;set;}

        public int Age 
        {
            get 
            {
                int age = 0;
                age = DateTime.Now.Year - Birthdate.Year;
                if (DateTime.Now.DayOfYear < Birthdate.DayOfYear)
                {
                    age -= 1;
                }
                return age;
            }
        }

        // Navigation property for the list of dish objects created by any given chef
        public List<Dish> DishesMade {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }

    public class DateAttribute : RangeAttribute
    {
            public DateAttribute()
            : base(typeof(DateTime), 
                DateTime.Now.AddYears(-999).ToString("MM/dd/yyyy"),     
                DateTime.Now.AddYears(-18).ToString("MM/dd/yyyy")) 
            { } 
    }
}