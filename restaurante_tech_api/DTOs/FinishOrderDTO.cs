﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.DTOs
{
    public class FinishOrderDTO
    {
        public string orderId { get; set; }
        public string userId { get; set; }
        public int usedPoints { get; set; }

    }
}
