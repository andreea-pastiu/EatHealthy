using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class Nutritionist : User
    {
        public String PhoneNumber { get; set; }
        public String Address { get; set; }
        public List<Patient> Patients { get; set; }
        public List<MealPlan> MealPlans { get; set; }
    }
}