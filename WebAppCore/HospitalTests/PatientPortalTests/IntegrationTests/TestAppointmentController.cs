﻿using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestAppointmentController
    {
        public AppointmentRepository _appointmentRepository;

        public AppointmentController _appointmentController;

        public TestAppointmentController()
        {
            GetInMemoryPersonRepository();
        }

        private void GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureCreated();
            _appointmentRepository = new AppointmentRepository(hospitalContext);
            _appointmentController = new AppointmentController(new AppointmentService(_appointmentRepository));
        }

        [Fact]
        public void Test_appointment_by_patient_not_found()
        {
            var controller = new AppointmentController();
            var response = controller.GetAppointmentsByPatientId(50);

            //Assert
            Assert.Empty(response.Value.ToList());

        }

        [Fact]
        public void Test_appointment_by_patient_found()
        {
            Appointment appointment = new Appointment()
            {
                Id = 1,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = DateTime.Now,
                Time = TimeSpan.Zero
            };
            _appointmentRepository.AddAppointment(appointment);
            var response = _appointmentController.GetAppointmentsByPatientId(1);

            Assert.NotNull(response);
            foreach (Appointment appointmentIterator in response.Value)
            {
                Assert.Equal(1, appointmentIterator.PatientForeignKey);
            }
        }

        [Fact]
        public void Test_add_valid_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 2,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2025, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(appointment);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Test_add_invalid_historical_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 3,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(1990, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(appointment);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void Test_add_invalid_overlapping_appointment()
        {
            Appointment validAppointment = new Appointment()
            {
                Id = 4,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2030, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            Appointment overlappingAppointment = new Appointment()
            {
                Id = 5,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2030, 6, 6, 11, 30, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(validAppointment);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = _appointmentController.AddAppointment(overlappingAppointment);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Appointment addedAppointment = _appointmentController.GetAppointment(4).Value;
            Appointment notAddedAppointment = _appointmentController.GetAppointment(5).Value;

            Assert.NotNull(addedAppointment);
            Assert.Null(notAddedAppointment);
        }
    }
}
