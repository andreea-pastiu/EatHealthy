using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Services
{
    public class FoodDiaryService
    {
        private EatHealthyContext context;

        public FoodDiaryService(EatHealthyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a new entry to the food diary of a patient
        /// </summary>
        /// <param name="patientId">Id of the patient performing the request</param>
        /// <param name="mealDate">Date of the meal</param>
        /// <param name="diary">Details about the meal</param>
        /// <returns>The newly added entry to the food diary</returns>
        public FoodDiary Add(int patientId, DateTime mealDate, string diary)
        {
            var patient = context.Patients.FirstOrDefault(x => x.ID == patientId);
            if (patient.FoodDiaries == null)
            {
                patient.FoodDiaries = new List<FoodDiary>();
            }
            FoodDiary foodDiary = new FoodDiary
            {
                Date = mealDate,
                Diary = diary
            };
            patient.FoodDiaries.Add(foodDiary);
            context.SaveChanges();
            return foodDiary;
        }

        /// <summary>
        /// Gets all the food diary entries of a patient
        /// </summary>
        /// <param name="patientId">Id of the patient performing the request</param>
        /// <returns>All the food diary entris of a patient</returns>
        public List<FoodDiary> GetFoodDiary(int patientId)
        {
            var patient = context.Patients.Include("FoodDiaries").FirstOrDefault(x => x.ID == patientId);
            return patient.FoodDiaries;
        }
    }
}