﻿using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital.ShiftsAndVacations.Model;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class OnCallShiftsMapper
    {
        internal static OnCallShiftDTO onCallShiftToDTO(OnCallShift shift, DoctorService doctorService)
        {
            Doctor d = doctorService.FindById(shift.DoctorId);
            OnCallShiftDTO obj = new OnCallShiftDTO(shift.Id, d, shift.Date);
           

            return obj;
        }
    }
}