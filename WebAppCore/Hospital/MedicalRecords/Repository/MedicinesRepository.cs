﻿using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class MedicinesRepository : IMedicinesRepository
    {
        private HospitalContext dbContext = new HospitalContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void EditMedicine(Medicine editedMedicine)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> GetAll()
        {
            return dbContext.Medicines.ToList();
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
       
        public List<string> GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllMedicines()
        {
            throw new NotImplementedException();
        }

        public Medicine GetByID(string id)
        {
            Medicine medicine = new Medicine();
            medicine = dbContext.Medicines.Find(id);
            return medicine;
        }

        public void Add(Medicine medicine)
        {
            dbContext.Medicines.Add(medicine);
            dbContext.SaveChanges();
        }

        public void Save(Medicine parameter)
        {
            throw new NotImplementedException();
        }

    }
}
