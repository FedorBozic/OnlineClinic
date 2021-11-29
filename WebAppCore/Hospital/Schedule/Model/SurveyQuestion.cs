﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class SurveyQuestion
    {
        [Key]
        private int id;
        private string question; 
        private SurveySectionType section; 
        public int Answer { get; set; } 
        public int IdSurvey { get; set; }

        public SurveyQuestion() { }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        
       public SurveySectionType Section
       {
           get { return section; }
           set { section = value; }
       }

    }
}
