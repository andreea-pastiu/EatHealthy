using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Services
{
    public class NutritionistService
    {
        private EatHealthyContext context;

        public NutritionistService(EatHealthyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Performs the registration of a nutritionist
        /// </summary>
        /// <param name="name">Name of the nutritionist</param>
        /// <param name="email">Email of the nutritionist</param>
        /// <param name="password">Password of the nutritionist</param>
        /// <param name="phoneNumber">Phone number of the nutritionist</param>
        /// <param name="address">Address of the nutritionist</param>
        /// <returns>The newly added nutritionist, or null if the email is already in use</returns>
        public Nutritionist Register(string name, string email, string password, string phoneNumber, string address)
        {
            var nutritionist = context.Nutritionists.FirstOrDefault(x => x.Email == email);
            if (nutritionist != null)
            {
                return null;
            }
            else
            {
                PasswordHelper passwordHelper = PasswordHelper.GetPasswordHelper();
                string encryptedPassword = passwordHelper.EncryptPlainTextToCipherText(password);
                Nutritionist n = new Nutritionist
                {
                    Name = name,
                    Email = email,
                    Password = encryptedPassword,
                    PhoneNumber = phoneNumber,
                    Address = address
                };
                context.Nutritionists.Add(n);
                context.SaveChanges();
                return n;
            }
        }

        /// <summary>
        /// Performs login of a nutritionist
        /// </summary>
        /// <param name="email">Email of the nutritionist</param>
        /// <param name="password">Password of the nutritionist</param>
        /// <returns>The nutritionist if the credentials are valid, null otherwise</returns>
        public Nutritionist Login(string email, string password)
        {
            PasswordHelper passwordHelper = PasswordHelper.GetPasswordHelper();
            string encryptedPassword = passwordHelper.EncryptPlainTextToCipherText(password);
            var nutritionist = context.Nutritionists.FirstOrDefault(x => x.Email == email && x.Password == encryptedPassword);
            return nutritionist;
        }

        /// <summary>
        /// Get a list of all the nutritionists in the platform
        /// </summary>
        /// <returns>A list of all the nutritionists in the platform</returns>
        public List<Nutritionist> GetAll()
        {
            return context.Nutritionists.ToList();
        }
    }
}