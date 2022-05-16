using EatHealthy.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace EatHealthy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<AppointmentService, AppointmentService>();
            container.RegisterType<FoodDiaryService, FoodDiaryService>();
            container.RegisterType<NutritionistService, NutritionistService>();
            container.RegisterType<PatientService, PatientService>();

            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}