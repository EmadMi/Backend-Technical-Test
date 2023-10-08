using Application.DTOs.Areas;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AreaRules
{
    public class GetAreaRuleDTO
    {
        public int Id { get; set; }
        public GetAreaDTO Area { get; set; }
        [Display(Name = "Rule Year")]
        public int Year { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
        [Display(Name = "Tax Fee")]
        public int TaxFee { get; set; }
    }
}
