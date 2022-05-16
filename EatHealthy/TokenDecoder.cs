using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace EatHealthy
{
    public static class TokenDecoder
    {
        public static bool CheckPatientToken(string token, int patientId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var type = securityToken.Claims.First(claim => claim.Type == "Type").Value;
            if(type != "Patient")
            {
                return false;
            }
            else
            {
                var p = int.Parse(securityToken.Claims.First(claim => claim.Type == "PatientId").Value);
                if(p == patientId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool CheckNutritionistToken(string token, int nutritionistId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var type = securityToken.Claims.First(claim => claim.Type == "Type").Value;
            if (type != "Nutritionist")
            {
                return false;
            }
            else
            {
                var p = int.Parse(securityToken.Claims.First(claim => claim.Type == "NutritionistId").Value);
                if (p == nutritionistId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}