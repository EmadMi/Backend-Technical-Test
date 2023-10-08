using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Areas
{
    public class GetAreaDTO
    {
        public int Id { get; set; }
        [Display(Name = "Area Name")]
        public string Name { get; set; }
        [Display(Name = "Maximum Tax Fee")]
        public int MaxTaxFee { get; set; }
        public int Order { get; set; }
    }
}
