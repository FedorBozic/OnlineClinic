﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Schedule.Service;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Model;
using System.Net.Http;
using System.Net;
using Hospital_API.DTO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Hospital.SharedModel;
using Hospital.MedicalRecords.Repository;

namespace Hospital_API.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public AppointmentService _appointmentService { get; set; }

        [ActivatorUtilitiesConstructor]
        public AppointmentController()
        {
            HospitalContext context = new HospitalContext();
            IPatientRepository patientRepository = new PatientRepository(context);
            IDoctorRepository doctorRepository = new DoctorRepository(context, patientRepository);
            _appointmentService = new AppointmentService(new AppointmentRepository(context), doctorRepository);
        }

        public AppointmentController(AppointmentService _appointmentService)
        {
            this._appointmentService = _appointmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _appointmentService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            return _appointmentService.FindById(id);
        }

        [HttpGet("patient/{id}")]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointmentsByPatientId(int id)
        {
            List<Appointment> appointments = _appointmentService.GetByPatientId(id);
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            HospitalContext context = new HospitalContext();
            DoctorRepository doctorRepository = new DoctorRepository(context, new PatientRepository(context));
            SurveyRepository surveyRepository = new SurveyRepository(context);
            foreach (Appointment appointment in appointments)
                appointmentDTOs.Add(new AppointmentDTO(appointment, doctorRepository, surveyRepository));
            return appointmentDTOs;
        }

        [HttpGet("doctor/{id}")]
        public ActionResult<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int id)
        {
            return _appointmentService.GetByDoctorId(id);
        }

        [HttpPost("recommendation_doctor")]
        public ActionResult<IEnumerable<Appointment>> GetRecommendedAppointments([FromBody] AppointmentRecommendationRequestDTO appointmentRecommendationRequest)
        {
            if(String.Equals(appointmentRecommendationRequest.PriorityType,"DOCTOR_PRIORITY"))
                return _appointmentService.GetAvailableByDateRangeAndDoctor(appointmentRecommendationRequest.LowerDateRange, appointmentRecommendationRequest.UpperDateRange, TimeSpan.Parse(appointmentRecommendationRequest.LowerTimeRange), TimeSpan.Parse(appointmentRecommendationRequest.UpperTimeRange), appointmentRecommendationRequest.DoctorId, AppointmentSearchPriority.DOCTOR_PRIORITY);
            
            return _appointmentService.GetAvailableByDateRangeAndDoctor(appointmentRecommendationRequest.LowerDateRange, appointmentRecommendationRequest.UpperDateRange, TimeSpan.Parse(appointmentRecommendationRequest.LowerTimeRange), TimeSpan.Parse(appointmentRecommendationRequest.UpperTimeRange), appointmentRecommendationRequest.DoctorId, AppointmentSearchPriority.DATE_TIME_PRIORITY);
        }

        [HttpPost("add_appointment")]
        public HttpResponseMessage AddAppointment([FromBody] Appointment appointmentToAdd)
        {
            if (appointmentToAdd.Date < DateTime.Now)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            if (_appointmentService.GetByDateAndDoctor(appointmentToAdd.Date, appointmentToAdd.Time, appointmentToAdd.DoctorForeignKey) != null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };

            _appointmentService.AddAppointment(appointmentToAdd);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete("{id}")]
        public ActionResult<Appointment> CancelAppointment(int id)
        {
            var appointment = _appointmentService.FindById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsCancelled = true;
            _appointmentService.Modify(appointment);
            return appointment;
        }
    }
}
