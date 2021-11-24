﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class TransportEquipmentDTO
    {
        public int TargetEqupmentId { get; set; }
        public int TargetRoomId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int Amount { get; set; }
    }
}
