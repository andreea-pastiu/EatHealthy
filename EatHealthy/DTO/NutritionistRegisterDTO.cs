using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.DTO
{
    public class NutritionistRegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}