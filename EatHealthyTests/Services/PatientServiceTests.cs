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
    [TestClass()]
    public class PatientServiceTests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            var mockSet = new Mock<DbSet<Patient>>();

            var data = new List<Patient>
            {
                new Patient { Name = "Andreea", Email = "andreea@gmail.com" },
            }.AsQueryable();

            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EatHealthyContext>();
            mockContext.Setup(m => m.Patients).Returns(mockSet.Object);

            var service = new PatientService(mockContext.Object);
            service.Register("Andreea2", "andreea2@gmail.com", "andreea", 169, 73);

            mockSet.Verify(m => m.Add(It.IsAny<Patient>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}