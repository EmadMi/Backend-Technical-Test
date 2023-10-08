using Application.DTOs.Areas;
using Application.DTOs.Results;
using Application.DTOs.Vehicles;
using Application.Interfaces.Contexts;
using Application.Interfaces.Vehicles;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IBaseContext _db;
        public VehicleService(IBaseContext db)
        {
            _db = db;
        }
        public ResultDTO<GetVehicleDTO> CreateVehicle(CreateVehicleDTO model)
        {
            ResultDTO<GetVehicleDTO> Result = new ResultDTO<GetVehicleDTO>()
            {
                Data = new GetVehicleDTO()
            };
            //
            try
            {
                var ExistVehicleName = _db.Vehicles.Any(x => x.Name.ToLower().Equals(model.Name.ToLower()));
                //
                if (ExistVehicleName)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Vehicle Name is taken";
                    //
                    return Result;
                }
                //
                int Order = _db.Vehicles.Any() ? _db.Vehicles.Max(x => x.Order) + 1 : 1;
                Vehicle NewVehicle = new Vehicle()
                {
                    Name = model.Name,
                    IsTaxFree = model.IsTaxFree,
                    Order = Order
                };
                //
                _db.Vehicles.Add(NewVehicle);
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetVehicleById(NewVehicle.Id);
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

        public ResultDTO DeleteVehicle(DeleteVehicleDTO model)
        {
            ResultDTO Result = new ResultDTO();
            //
            try
            {
                Vehicle CurVehicle = _db.Vehicles.Find(model.Id);
                //
                if (CurVehicle == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Vehicle Not Found";
                    //
                    return Result;
                }
                //
                _db.Vehicles.Remove(CurVehicle);
                _db.Entry(CurVehicle).State = EntityState.Deleted;
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

        public GetVehicleDTO? GetVehicleById(int Id)
        {
            Vehicle? SelectedVehicle = _db.Vehicles.Find(Id);
            //
            if (SelectedVehicle == null)
            {
                return null;
            }
            //
            return new GetVehicleDTO()
            {
                Id = SelectedVehicle.Id,
                Name = SelectedVehicle.Name,
                Order = SelectedVehicle.Order,
                IsTaxFree = SelectedVehicle.IsTaxFree
            };
        }

        public List<GetVehicleDTO> GetVehicles()
        {
            return _db.Vehicles.OrderBy(x => x.Order).Select(x => new GetVehicleDTO()
            {
                Id = x.Id,
                IsTaxFree = x.IsTaxFree,
                Name = x.Name,
                Order = x.Order
            }).ToList();
        }

        public ResultDTO<GetVehicleDTO> UpdateVehicle(UpdateVehicleDTO model)
        {
            ResultDTO<GetVehicleDTO> Result = new ResultDTO<GetVehicleDTO>()
            {
                Data = new GetVehicleDTO()
            };
            //
            try
            {
                Vehicle CurVehicle = _db.Vehicles.Find(model.Id);
                //
                if (CurVehicle == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Vehicle Not Found.";
                    //
                    return Result;
                }
                //
                var ExistVehicleName = _db.Vehicles.Any(x => x.Name.ToLower().Equals(model.Name.ToLower()) && !x.Id.Equals(model.Id));
                //
                if (ExistVehicleName)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Vehicle Name is taken";
                    //
                    return Result;
                }
                //
                CurVehicle.Name = model.Name;
                CurVehicle.IsTaxFree = model.IsTaxFree;
                CurVehicle.Order = model.Order;
                //
                _db.Entry(CurVehicle).State = EntityState.Modified;
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetVehicleById(model.Id);
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
