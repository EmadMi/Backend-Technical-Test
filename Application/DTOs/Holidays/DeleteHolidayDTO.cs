using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.DTOs.Holidays
{
    public class DeleteHolidayDTO
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        public DateTime HolyDate { get; set; }
    }
}
