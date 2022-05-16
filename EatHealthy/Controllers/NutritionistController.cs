using EatHealthy.DTO;
using EatHealthy.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EatHealthy.Controllers
{
    public class NutritionistController : Controller
    {

        private NutritionistService nutritionistService;
        private PatientService patientService;

        public NutritionistController(NutritionistService nutritionistService, PatientService patientService)
        {
            this.nutritionistService = nutritionistService;
            this.patientService = patientService;
        }

        /// <summary>
        /// Shows the nutritionist register page
        /// </summary>
        /// <returns>The nutritionist register page</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers a new nutritionist
        /// </summary>
        /// <param name="nutritionistRegisterDTO">Object containing info about the new nutritionist</param>
        /// <returns>Login page if the registration is succesful, register page otherwise</returns>
        [HttpPost]
        public ActionResult Register(NutritionistRegisterDTO nutritionistRegisterDTO)
        {
            Logger.LogInformation("Registering a new nutritionist...");
            var nutritionist = nutritionistService.Register(nutritionistRegisterDTO.Name, nutritionistRegisterDTO.Email, nutritionistRegisterDTO.Password, nutritionistRegisterDTO.PhoneNumber, nutritionistRegisterDTO.Address);
            if (nutritionist == null)
            {
                Logger.LogInformation("Could not register a new nutritionist");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Nutritionist");
            }
        }

        /// <summary>
        /// Show the nutritionist login page
        /// </summary>
        /// <returns>The nutritionist login page</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Performs nutritionist login
        /// </summary>
        /// <param name="nutritionistLoginDTO">Object containing login data</param>
        /// <returns>Appointments page if the credentials are valid, login page otherwise</returns>
        [HttpPost]
        public ActionResult Login(NutritionistLoginDTO nutritionistLoginDTO)
        {
            var nutritionist = nutritionistService.Login(nutritionistLoginDTO.Email, nutritionistLoginDTO.Password);
            if (nutritionist == null)
            {
                Logger.LogInformation("Invalid nutritionist username or password...");
                return View();
            }
            else
            {
                var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("NutritionistId", nutritionist.ID.ToString()),
                        new Claim("Type", "Nutritionist"),
                    }),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var finalToken = tokenHandler.WriteToken(token);
                HttpContext.Session.Add("NutritionistId", nutritionist.ID);
                HttpContext.Session.Add("Token", finalToken);
                return RedirectToAction("Nutritionist", "Appointment");
            }
        }

        /// <summary>
        /// Shows the page with the patients of a nutritionist
        /// </summary>
        /// <returns>A page with the patients if the token is valid, login page otherwise</returns>
        public ActionResult ViewPatients()
        {
            if (HttpContext.Session["NutritionistId"] == null || HttpContext.Session["Token"] == null)
            {
                return RedirectToAction("Login", "Nutritionist");
            }
            var nutritionistId = int.Parse(HttpContext.Session["NutritionistId"].ToString());
            var token = HttpContext.Session["Token"].ToString();
            if (!TokenDecoder.CheckNutritionistToken(token, nutritionistId))
            {
                return RedirectToAction("Login", "Nutritionist");
            }
            PatientsDTO patientsDTO = new PatientsDTO();
            patientsDTO.Patients = patientService.GetPatientsByNutritionistId(nutritionistId);
            return View(patientsDTO);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("NutritionistId");
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "Nutritionist");
        }
    }
}