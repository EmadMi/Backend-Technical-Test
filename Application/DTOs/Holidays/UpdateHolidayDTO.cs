using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.DTOs.Holidays
{
    public class UpdateHolidayDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        public DateTime HolyDate { get; set; }
    }
}
