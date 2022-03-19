using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class Meal
    {
        public int ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}