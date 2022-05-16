using Microsoft.VisualStudio.TestTools.UnitTesting;
using EatHealthy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using EatHealthy.Models;

namespace EatHealthy.Services.Tests
{
    [TestClass]
    class NutritionistServicesTests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            var mockSet = new Mock<DbSet<Nutritionist>>();

            var data = new List<Nutritionist>
            {
                new Nutritionist { Name = "Andreea", Email = "andreea@gmail.com" },
            }.AsQueryable();

            mockSet.As<IQueryable<Nutritionist>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Nutritionist>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Nutritionist>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Nutritionist>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EatHealthyContext>();
            mockContext.Setup(m => m.Nutritionists).Returns(mockSet.Object);

            var service = new NutritionistService(mockContext.Object);
            service.Register("Andreea2", "andreea2@gmail.com", "andreea", "0123456789", "Address 1");

            mockSet.Verify(m => m.Add(It.IsAny<Nutritionist>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
