using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.DTO
{
    public class AppointmentDTO
    {
        public DateTime AppointmentDate { get; set; }

        public int NutritionistId { get; set; }

        public List<Nutritionist> Nutritionists { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}