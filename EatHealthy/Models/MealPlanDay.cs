using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class MealPlanDay
    {
        public int ID { get; set; }
        public DayOfWeek Day { get; set; }
        public int Week { get; set; }
        public List<Meal> Meals { get; set; }
    }
}