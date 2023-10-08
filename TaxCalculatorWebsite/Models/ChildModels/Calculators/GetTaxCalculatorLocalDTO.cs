using Application.DTOs.TaxCalculators;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculatorWebsite.Models.ChildModels.Calculators
{
    public class GetTaxCalculatorLocalDTO : GetTaxCalculatorDTO
    {
        public List<SelectListItem>? Areas { get; set; }
        public List<SelectListItem>? Vehicles { get; set; }
    }
}
