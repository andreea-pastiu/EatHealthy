using EatHealthy.DTO;
using EatHealthy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EatHealthy.Controllers
{
    public class FoodDiaryController : Controller
    {
        private FoodDiaryService foodDiaryService;

        public FoodDiaryController(FoodDiaryService foodDiaryService)
        {
            this.foodDiaryService = foodDiaryService;
        }

        /// <summary>
        /// Shows the food diary page
        /// </summary>
        /// <returns>The food diary page if token is valid, login page otherwise</returns>
        public ActionResult Index()
        {
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
            FoodDiaryDTO foodDiaryDTO = new FoodDiaryDTO();
            foodDiaryDTO.FoodDiaryEntries = foodDiaryService.GetFoodDiary(patientId);
            foodDiaryDTO.MealDate = DateTime.Now;
            return View(foodDiaryDTO);
        }

        /// <summary>
        /// Adds a new entry to the food diary
        /// </summary>
        /// <param name="foodDiaryDTO">Object containing the data of the new entry in the food diary</param>
        /// <returns>Food diary page if the token is valid, login page otherwise</returns>
        [HttpPost]
        public ActionResult Add(FoodDiaryDTO foodDiaryDTO)
        {
            Logger.LogInformation("Adding new entry in the food diary...");
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
            foodDiaryService.Add(patientId, foodDiaryDTO.MealDate, foodDiaryDTO.Diary);
            return RedirectToAction("Index", "FoodDiary");
        }
    }
}