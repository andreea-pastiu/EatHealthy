using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Patient Patient { get; set; }
        public Nutritionist Nutritionist { get; set; }
        public int Status { get; set; }
    }
}