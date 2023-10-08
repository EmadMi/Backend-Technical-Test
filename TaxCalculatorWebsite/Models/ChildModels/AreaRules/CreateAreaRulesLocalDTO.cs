using Application.DTOs.AreaRules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculatorWebsite.Models.ChildModels.AreaRules
{
    public class CreateAreaRulesLocalDTO : CreateAreaRuleDTO
    {
        public List<SelectListItem>? Areas { get; set; }
    }
}
