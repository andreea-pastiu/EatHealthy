using EatHealthy.DTO;
using EatHealthy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EatHealthy.Controllers
{
    public class AppointmentController : Controller
    {
        private NutritionistService nutritionistService;
        private AppointmentService appointmentService;

        public AppointmentController(NutritionistService nutritionistService, AppointmentService appointmentService)
        {
            this.nutritionistService = nutritionistService;
            this.appointmentService = appointmentService;
        }

        /// <summary>
        /// Displays the patient appointments page
        /// </summary>
        /// <returns>The view with the appointments, if the token is valid, login page otherwise</returns>
        public ActionResult Patient()
        {
            if(HttpContext.Session["PatientId"] == null || HttpContext.Session["Token"] == null)
            {
                return RedirectToAction("Login", "Patient");
            }
            var patientId = int.Parse(HttpContext.Session["PatientId"].ToString());
            var token = HttpContext.Session["Token"].ToString();
            if(!TokenDecoder.CheckPatientToken(token, patientId))
            {
                return RedirectToAction("Login", "Patient");
            }
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.Nutritionists = nutritionistService.GetAll();
            appointmentDTO.AppointmentDate = DateTime.Now;
            appointmentDTO.Appointments = appointmentService.GetAppointmentsForPatient(patientId);
            return View(appointmentDTO);
        }

        /// <summary>
        /// Adds a new appointment
        /// </summary>
        /// <param name="appointmentDTO">Object containing the appointment info</param>
        /// <returns>Appointments page if token is valid, login page otherwise</returns>
        [HttpPost]
        public ActionResult AddAppointment(AppointmentDTO appointmentDTO)
        {
            Logger.LogInformation("Adding a new appointment...");
            if (HttpContext.Session["PatientId"] == null || HttpContext.Session["Token"] == null)
            {
                return RedirectToAction("Login", "Patient");
            }
            var patientId = int.Parse(HttpContext.Session["PatientId"].ToString());
            var token = HttpContext.Session["Token"].ToString();
            if (!TokenDecoder.CheckPatientToken(token, patientId))
            {
                return RedirectToAction("Login", "Patient");
            }
            appointmentService.Add(patientId, appointmentDTO.NutritionistId, appointmentDTO.AppointmentDate);
            Logger.LogInformation("Appointment added");
            return RedirectToAction("Patient", "Appointment");
        }

        /// <summary>
        /// Displays the nutritionist appointments page
        /// </summary>
        /// <returns>The view with the appointments, if the token is valid, login page otherwise</returns>
        public ActionResult Nutritionist()
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
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.Appointments = appointmentService.GetAppointmentsForNutritionist(nutritionistId);
            return View(appointmentDTO);
        }

        /// <summary>
        /// Set status of the appointment to 'Accepted'
        /// </summary>
        /// <param name="appointmentId">Id of the appointment to be updated</param>
        /// <returns>Success if token is valid and appointment can be updated, login page otherwise</returns>
        [HttpPost]
        public ActionResult Accept(int appointmentId)
        {
            Logger.LogInformation("Accepting appointment " + appointmentId);
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
            appointmentService.Accept(appointmentId);
            return new HttpStatusCodeResult(200);
        }

        /// <summary>
        /// Set status of the appointment to 'Rejected'
        /// </summary>
        /// <param name="appointmentId">Id of the appointment to be updated</param>
        /// <returns>Success if token is valid and appointment can be updated, login page otherwise</returns>
        [HttpPost]
        public ActionResult Reject(int appointmentId)
        {
            Logger.LogInformation("Rejecting appointment " + appointmentId);
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
            appointmentService.Reject(appointmentId);
            return new HttpStatusCodeResult(200);
        }
    }
}