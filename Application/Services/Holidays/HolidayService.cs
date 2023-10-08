using Application.DTOs.Areas;
using Application.DTOs.Holidays;
using Application.DTOs.Results;
using Application.Interfaces.Contexts;
using Application.Interfaces.Holidays;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Holidays
{
    public class HolidayService : IHolidayService
    {
        private readonly IBaseContext _db;
        public HolidayService(IBaseContext db)
        {
            _db = db;
        }
        public ResultDTO<GetHolidayDTO> CreateHoliday(CreateHolidayDTO model)
        {
            ResultDTO<GetHolidayDTO> Result = new ResultDTO<GetHolidayDTO>()
            {
                Data = new GetHolidayDTO()
            };
            //
            try
            {
                var ExistHoliday = _db.Holidays.Any(x => x.HolyDate.Equals(model.HolyDate));
                //
                if (ExistHoliday)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Holiday already exist";
                    //
                    return Result;
                }
                //
                Holiday NewHoliday = new Holiday()
                {
                    HolyDate = model.HolyDate
                };
                //
                _db.Holidays.Add(NewHoliday);
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetHolidayById(NewHoliday.Id);
                //
                return Result;
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                Result.Message = ex.Message;
                //
                return Result;
            }
        }

        public ResultDTO DeleteHoliday(DeleteHolidayDTO model)
        {
            ResultDTO Result = new ResultDTO();
            //
            try
            {
                Holiday CurHoliday = _db.Holidays.Find(model.Id);
                //
                if (CurHoliday == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Holiday Not Found";
                    //
                    return Result;
                }
                //
                _db.Holidays.Remove(CurHoliday);
                _db.Entry(CurHoliday).State = EntityState.Deleted;
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                //
                return Result;
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                Result.Message = ex.Message;
                //
                return Result;
            }
        }

        public GetHolidayDTO? GetHolidayById(int Id)
        {
            Holiday? SelectedHoliday = _db.Holidays.Find(Id);
            //
            if (SelectedHoliday == null)
            {
                return null;
            }
            //
            return new GetHolidayDTO()
            {
                Id = SelectedHoliday.Id,
                HolyDate = SelectedHoliday.HolyDate
            };
        }

        public List<GetHolidayDTO> GetHolidays()
        {
            return _db.Holidays.OrderBy(x => x.HolyDate).Select(x => new GetHolidayDTO()
            {
                Id = x.Id,
                HolyDate = x.HolyDate
            }).ToList();
        }

        public ResultDTO<GetHolidayDTO> UpdateHoliday(UpdateHolidayDTO model)
        {
            ResultDTO<GetHolidayDTO> Result = new ResultDTO<GetHolidayDTO>()
            {
                Data = new GetHolidayDTO()
            };
            //
            try
            {
                var ExistHoliday = _db.Holidays.Any(x => x.HolyDate.Equals(model.HolyDate) && !x.Id.Equals(model.Id));
                //
                if (ExistHoliday)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Holiday already exist";
                    //
                    return Result;
                }
                //
                Holiday CurHoliday = _db.Holidays.Find(model.Id);
                //
                if (CurHoliday == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Holiday Not Found.";
                    //
                    return Result;
                }
                //
                CurHoliday.HolyDate = model.HolyDate;
                //
                _db.Entry(CurHoliday).State = EntityState.Modified;
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetHolidayById(model.Id);
                //
                return Result;
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                Result.Message = ex.Message;
                //
                return Result;
            }
        }
    }
}
