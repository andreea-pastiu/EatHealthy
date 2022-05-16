using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.DTO
{
    public class FoodDiaryDTO
    {
        public List<FoodDiary> FoodDiaryEntries { get; set; }
        public DateTime MealDate { get; set; }

        public string Diary { get; set; }
    }
}