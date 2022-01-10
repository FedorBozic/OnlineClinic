﻿using Integration.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public interface IEventRepository
    {
        public List<Event> GetAll();
        public Event GetById(int eventId);
        public void Save(Event newEvent);
    }
}
