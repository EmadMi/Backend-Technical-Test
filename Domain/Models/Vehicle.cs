﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTaxFree { get; set; }
        public int Order { get; set; }
    }
}