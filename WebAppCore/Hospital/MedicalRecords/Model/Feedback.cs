﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPublishable { get; set; }
        public bool IsAnonymousss { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public Feedback()
        {
            this.Date = DateTime.Now;
            this.UserName = "User";
        }
    }
}
