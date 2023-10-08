using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TaxCalculators
{
    public class GetTaxCalculatorDTO
    {
        [Required(ErrorMessage = "Area is required.")]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "Vehicle is required.")]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public List<GetTaxCalculatorDatesDTO> PassDateTimeList { get; set; }
        [Display(Name = "Tax Fee Must Pay")]
        public int? TaxFee { get; set; }
        [Display(Name = "All Tax Fee")]
        public int? RealTaxFee { get; set; }
    }
}
