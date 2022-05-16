using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.DTO
{
    public class PatientRegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

    }
}