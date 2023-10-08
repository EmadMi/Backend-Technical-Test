using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TaxCalculators
{
    public class GetTaxCalculatorDatesDTO
    {
        [Display(Name = "Pass Date and Time")]
        [Required(ErrorMessage = "Pass Date and Time is required")]
        public DateTime PassDateTime { get; set; }
    }
}
