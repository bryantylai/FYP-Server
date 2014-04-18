using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class DoctorService
    {
        private DoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorRepository();
        }

        internal IEnumerable<Doctor> ListOfDoctors()
        {
            return doctorRepository.ListAllDoctors();
        }

        internal IEnumerable<Appointment> ListOfAppointments(Guid guid)
        {
            return doctorRepository.ListAllAppointments(guid);
        }

        internal bool ValidateForm(AppointmentForm appointmentForm)
        {
            object[] keys = { appointmentForm.DoctorId, appointmentForm.AppointmentTime };
            foreach (object key in keys)
            {
                if (key == null) return false;
            }

            return true;
        }

        internal bool CreateAppointment(AppointmentForm appointmentForm, Guid userId)
        {
            Appointment appointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DoctorId = appointmentForm.DoctorId,
                AppointmentTime = appointmentForm.AppointmentTime
            };

            return doctorRepository.RecordAppointment(appointment);
        }
    }
}