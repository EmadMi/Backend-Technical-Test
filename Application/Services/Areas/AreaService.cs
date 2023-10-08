using Application.DTOs.Areas;
using Application.DTOs.Results;
using Application.Interfaces.Areas;
using Application.Interfaces.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Areas
{
    public class AreaService : IAreaService
    {
        private readonly IBaseContext _db;
        public AreaService(IBaseContext db)
        {
            _db = db;
        }
        public ResultDTO<GetAreaDTO> CreateArea(CreateAreaDTO model)
        {
            ResultDTO<GetAreaDTO> Result = new ResultDTO<GetAreaDTO>()
            {
                Data = new GetAreaDTO()
            };
            //
            try
            {
                var ExistAreaName = _db.Areas.Any(x => x.Name.ToLower().Equals(model.Name.ToLower()));
                //
                if (ExistAreaName)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Area Name is taken";
                    //
                    return Result;
                }
                //
                int Order = _db.Areas.Any() ? _db.Areas.Max(x => x.Order) + 1 : 1;
                Area NewArea = new Area()
                {
                    Name = model.Name,
                    MaxTaxFee = model.MaxTaxFee,
                    Order = Order
                };
                //
                _db.Areas.Add(NewArea);
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetAreaById(NewArea.Id);
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

        public ResultDTO DeleteArea(DeleteAreaDTO model)
        {
            ResultDTO Result = new ResultDTO();
            //
            try
            {
                Area CurArea = _db.Areas.Find(model.Id);
                //
                if (CurArea == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Area Not Found";
                    //
                    return Result;
                }
                //
                _db.Areas.Remove(CurArea);
                _db.Entry(CurArea).State = EntityState.Deleted;
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

        public GetAreaDTO? GetAreaById(int Id)
        {
            Area? SelectedArea = _db.Areas.Find(Id);
            //
            if (SelectedArea == null)
            {
                return null;
            }
            //
            return new GetAreaDTO()
            {
                Id = SelectedArea.Id,
                MaxTaxFee = SelectedArea.MaxTaxFee,
                Name = SelectedArea.Name,
                Order = SelectedArea.Order
            };
        }

        public List<GetAreaDTO> GetAreas()
        {
            return _db.Areas.OrderBy(x => x.Order).Select(x => new GetAreaDTO()
            {
                Id = x.Id,
                MaxTaxFee = x.MaxTaxFee,
                Name = x.Name,
                Order = x.Order
            }).ToList();
        }

        public ResultDTO<GetAreaDTO> UpdateArea(UpdateAreaDTO model)
        {
            ResultDTO<GetAreaDTO> Result = new ResultDTO<GetAreaDTO>()
            {
                Data = new GetAreaDTO()
            };
            //
            try
            {
                var ExistAreaName = _db.Areas.Any(x => x.Name.ToLower().Equals(model.Name.ToLower()) && !x.Id.Equals(model.Id));
                //
                if (ExistAreaName)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Area Name is taken";
                    //
                    return Result;
                }
                //
                Area CurArea = _db.Areas.Find(model.Id);
                //
                if (CurArea == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Area Not Found.";
                    //
                    return Result;
                }
                //
                CurArea.Name = model.Name;
                CurArea.MaxTaxFee = model.MaxTaxFee;
                CurArea.Order = model.Order;
                //
                _db.Entry(CurArea).State = EntityState.Modified;
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetAreaById(model.Id);
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
