using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class FoodDiary
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public String Diary { get; set; }
    }
}