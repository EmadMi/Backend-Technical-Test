using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vehicles
{
    public class GetVehicleDTO
    {
        public int Id { get; set; }
        [Display(Name = "Vehicle Name")]
        public string Name { get; set; }
        [Display(Name = "Free Tax")]
        public bool IsTaxFree { get; set; }
        public int Order { get; set; }
    }
}
