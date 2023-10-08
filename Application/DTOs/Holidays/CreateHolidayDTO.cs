using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Holidays
{
    public class CreateHolidayDTO
    {
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        public DateTime HolyDate { get; set; }
    }
}
