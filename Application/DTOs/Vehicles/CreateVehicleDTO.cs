using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vehicles
{
    public class CreateVehicleDTO
    {
        [Display(Name = "Vehicle Name")]
        [Required(ErrorMessage = "Vehicle Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Free Tax Included")]
        public bool IsTaxFree { get; set; }
    }
}
