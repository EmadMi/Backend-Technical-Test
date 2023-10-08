using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AreaRules
{
    public class CreateAreaRuleDTO
    {
        [Required(ErrorMessage = "Area is required")]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "Rule Year")]
        [Range(1993,2040,ErrorMessage = "Year must between 1993 to 2040")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Start Time is required")]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "End Time is required")]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Tax Fee is required")]
        [Display(Name = "Tax Fee")]
        public int TaxFee { get; set; }
    }
}
