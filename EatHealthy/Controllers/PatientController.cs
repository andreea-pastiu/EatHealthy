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
    public class PatientController : Controller
    {
        private PatientService patientService;

        public PatientController(PatientService patientService)
        {
            this.patientService = patientService;
        }

        /// <summary>
        /// Show the patient register page
        /// </summary>
        /// <returns>The patient register page</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Perfoms a registration of a patient
        /// </summary>
        /// <param name="patientRegisterDTO">Object containing the details of the patient</param>
        /// <returns>Login page if the registration is succesful, register page otherwise</returns>
        [HttpPost]
        public ActionResult Register(PatientRegisterDTO patientRegisterDTO)
        {
            Logger.LogInformation("New patient registering...");
            var patient = patientService.Register(patientRegisterDTO.Name, patientRegisterDTO.Email, patientRegisterDTO.Password, patientRegisterDTO.Height, patientRegisterDTO.Weight);
            if(patient == null)
            {
                Logger.LogInformation("Could not register new patient...");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Patient");
            }
        }

        /// <summary>
        /// Shows the patient login page
        /// </summary>
        /// <returns>The patient login page</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Performs login of a patient
        /// </summary>
        /// <param name="patientLoginDTO">Object containing the credentials of the patient</param>
        /// <returns>Appointments page if the credentials are valid, login page otherwise</returns>
        [HttpPost]
        public ActionResult Login(PatientLoginDTO patientLoginDTO)
        {
            var patient = patientService.Login(patientLoginDTO.Email, patientLoginDTO.Password);
            if (patient == null)
            {
                Logger.LogInformation("Invalid patient username or password");
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
                        new Claim("PatientId", patient.ID.ToString()),
                        new Claim("Type", "Patient"),
                    }),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var finalToken = tokenHandler.WriteToken(token);
                HttpContext.Session.Add("PatientId", patient.ID);
                HttpContext.Session.Add("Token", finalToken);
                return RedirectToAction("Patient", "Appointment");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("PatientId");
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "Patient");
        }
    }
}