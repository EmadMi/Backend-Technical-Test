﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AreaRule
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int Year { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TaxFee { get; set; }
    }
}
