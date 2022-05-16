using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatHealthy.Services
{
    public class AppointmentService
    {
        private EatHealthyContext context;

        public AppointmentService(EatHealthyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a new appointment
        /// </summary>
        /// <param name="patientId">Id of the patient performing the appointment</param>
        /// <param name="nutritionistId">Id of the nutritionist</param>
        /// <param name="appointmentDate">Date of the appointment</param>
        /// <returns>The added appointment</returns>
        public Appointment Add(int patientId, int nutritionistId, DateTime appointmentDate)
        {
            var patient = context.Patients.FirstOrDefault(x => x.ID == patientId);
            var nutritionist = context.Nutritionists.FirstOrDefault(x => x.ID == nutritionistId);
            Appointment appointment = new Appointment
            {
                Date = appointmentDate,
                Status = 0,
                Nutritionist = nutritionist
            };
            if (patient.Appointments == null)
            {
                patient.Appointments = new List<Appointment>();
            }
            patient.Appointments.Add(appointment);
            context.SaveChanges();
            return appointment;
        }

        /// <summary>
        /// Returns all the appointments of a patient
        /// </summary>
        /// <param name="patientId">The id of the patient performing the request</param>
        /// <returns>All the appointments of a patient</returns>
        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return context.Patients.Include("Appointments").Include("Appointments.Nutritionist").FirstOrDefault(x => x.ID == patientId).Appointments.ToList();
        }

        /// <summary>
        /// Returns all the appointments of a nutritionist
        /// </summary>
        /// <param name="nutritionistId">The id of the nutritionist performing the request</param>
        /// <returns>All the appointments of a nutritionist</returns>
        public List<Appointment> GetAppointmentsForNutritionist(int nutritionistId)
        {
            return context.Appointments.Include("Patient").Where(x => x.Nutritionist.ID == nutritionistId).ToList();
        }

        /// <summary>
        /// Sets the status of an appointment to 'Accepted'
        /// </summary>
        /// <param name="appointmentId">Id of the appointment to be updated</param>
        /// <returns>The number of accepted appointments</returns>
        public int Accept(int appointmentId)
        {
            var appointment = context.Appointments.FirstOrDefault(x => x.ID == appointmentId);
            appointment.Status = 1;
            return context.SaveChanges();
        }

        /// <summary>
        /// Sets the status of an appointment to 'Rejected'
        /// </summary>
        /// <param name="appointmentId">Id of the appointment to be updated</param>
        /// <returns>The number of rejected appointments</returns>
        public int Reject(int appointmentId)
        {
            var appointment = context.Appointments.FirstOrDefault(x => x.ID == appointmentId);
            appointment.Status = 2;
            return context.SaveChanges();
        }
    }
}