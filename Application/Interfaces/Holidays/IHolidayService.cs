using Application.DTOs.Areas;
using Application.DTOs.Holidays;
using Application.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Holidays
{
    public interface IHolidayService
    {
        GetHolidayDTO? GetHolidayById(int Id);

        List<GetHolidayDTO> GetHolidays();

        ResultDTO<GetHolidayDTO> CreateHoliday(CreateHolidayDTO model);
        ResultDTO<GetHolidayDTO> UpdateHoliday(UpdateHolidayDTO model);
        ResultDTO DeleteHoliday(DeleteHolidayDTO model);
    }
}
