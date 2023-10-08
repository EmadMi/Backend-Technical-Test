using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Holidays
{
    public class GetHolidayDTO
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        public DateTime HolyDate { get; set; }
    }
}
