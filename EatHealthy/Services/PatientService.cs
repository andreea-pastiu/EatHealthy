using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Services
{
    public class PatientService
    {
        private EatHealthyContext context;

        public PatientService(EatHealthyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Performs the registration of a patient
        /// </summary>
        /// <param name="name">Name of the patient</param>
        /// <param name="email">Email of the patient</param>
        /// <param name="password">Password of the patient</param>
        /// <param name="height">Height of the patient</param>
        /// <param name="weight">Weight of the patient</param>
        /// <returns>An object containing the details of the new patient, or null if the email is already in use</returns>
        public Patient Register(string name, string email, string password, double height, double weight)
        {
            var patient = context.Patients.FirstOrDefault(x => x.Email == email);
            if (patient != null)
            {
                return null;
            }
            else
            {
                PasswordHelper passwordHelper = PasswordHelper.GetPasswordHelper();
                string encryptedPassword = passwordHelper.EncryptPlainTextToCipherText(password);
                double bmi = weight / (height * height) * 10000;
                Patient p = new Patient
                {
                    Name = name,
                    Email = email,
                    Password = encryptedPassword,
                    Weight = weight,
                    Height = height,
                    BMI = bmi
                };
                context.Patients.Add(p);
                context.SaveChanges();
                return p;
            }
        }

        /// <summary>
        /// Performs login of a patient
        /// </summary>
        /// <param name="email">Email of the patient</param>
        /// <param name="password">Password of the patient</param>
        /// <returns>The patient with the given credentials if valid, null otherwise</returns>
        public Patient Login(string email, string password)
        {
            PasswordHelper passwordHelper = PasswordHelper.GetPasswordHelper();
            string encryptedPassword = passwordHelper.EncryptPlainTextToCipherText(password);
            var patient = context.Patients.FirstOrDefault(x => x.Email == email && x.Password == encryptedPassword);
            return patient;
        }

        /// <summary>
        /// Returns a list of all the patients linked to a nutritionist
        /// </summary>
        /// <param name="nutritionistId">Id of the nutritionist</param>
        /// <returns>A list of all the patients linked to a nutritionist</returns>
        public List<Patient> GetPatientsByNutritionistId(int nutritionistId)
        {
            var patients = context.Patients.Include("Appointments.Nutritionist").Where(x => x.Appointments.Any(y => y.Nutritionist.ID == nutritionistId && y.Status != 0)).ToList();
            return patients;
        }
    }
}