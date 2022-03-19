using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class MealPlan
    {
        public int ID { get; set; }
        public List<MealPlanDay> MealPlanDays { get; set; }
        public Patient Patient { get; set; }
    }
}