using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class Patient : User
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMI { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<FoodDiary> FoodDiaries { get; set; }
    }
}