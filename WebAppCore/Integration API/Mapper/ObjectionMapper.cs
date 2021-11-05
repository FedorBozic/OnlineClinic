﻿using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class ObjectionMapper
    {
        public static ObjectionDto ObjectionToObjectionDto(Objection objection, String pharmacyName)
        {
            ObjectionDto dto = new ObjectionDto();
            dto.Id = objection.Id;
            dto.Content = objection.Content;
            dto.PharmacyName = pharmacyName;
            return dto;
        }
    }
}