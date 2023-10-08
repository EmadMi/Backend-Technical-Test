using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.DTOs.Areas
{
    public class UpdateAreaDTO
    {
        public int Id { get; set; }
        [Display(Name = "Area Name")]
        [Required(ErrorMessage = "Area Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Maximum Tax Fee")]
        [Required(ErrorMessage = "Maximum Tax Fee is required.")]
        public int MaxTaxFee { get; set; }
        [Required(ErrorMessage = "Order is required.")]
        public int Order { get; set; }
    }
}
