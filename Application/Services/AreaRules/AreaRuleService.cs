using Application.DTOs.AreaRules;
using Application.DTOs.Areas;
using Application.DTOs.Results;
using Application.Interfaces.AreaRules;
using Application.Interfaces.Areas;
using Application.Interfaces.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AreaRules
{
    public class AreaRuleService : IAreaRuleService
    {
        private readonly IBaseContext _db;
        private readonly IAreaService _AreaService;
        public AreaRuleService(IBaseContext db,IAreaService AreaService)
        {
            _db = db;
            _AreaService = AreaService;
        }

        public ResultDTO<GetAreaRuleDTO> CreateAreaRule(CreateAreaRuleDTO model)
        {
            ResultDTO<GetAreaRuleDTO> Result = new ResultDTO<GetAreaRuleDTO>()
            {
                Data = new GetAreaRuleDTO()
            };
            //
            try
            {
                bool ExistAreaRule = true;
                //
                if (model.StartTime > model.EndTime)
                {
                    ExistAreaRule = _db.AreaRules.Any(x => x.AreaId.Equals(model.AreaId) && x.Year.Equals(model.Year) && ((x.EndTime >= model.StartTime && model.StartTime >= x.StartTime) || (x.StartTime <= model.EndTime && model.EndTime <= x.EndTime)));
                }
                else
                {
                    ExistAreaRule = _db.AreaRules.Any(x => x.AreaId.Equals(model.AreaId) && x.Year.Equals(model.Year) && ((x.StartTime <= model.StartTime && model.StartTime <= x.EndTime) || (x.StartTime <= model.EndTime && model.EndTime <= x.EndTime) || (x.StartTime <= model.StartTime && x.EndTime >= model.EndTime) || (x.StartTime >= model.StartTime && x.EndTime <= model.EndTime)));
                }
                //
                if (ExistAreaRule)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Information interference has occurred";
                    //
                    return Result;
                }
                //
                AreaRule NewAreaRule = new AreaRule()
                {
                    AreaId = model.AreaId,
                    EndTime = model.EndTime,
                    StartTime = model.StartTime,
                    Year = model.Year,
                    TaxFee = model.TaxFee,
                };
                //
                _db.AreaRules.Add(NewAreaRule);
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetAreaRuleById(NewAreaRule.Id);
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

        public ResultDTO DeleteAreaRule(DeleteAreaRuleDTO model)
        {
            ResultDTO Result = new ResultDTO();
            //
            try
            {
                AreaRule CurAreaRule = _db.AreaRules.Find(model.Id);
                //
                if (CurAreaRule == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Area Rule Not Found";
                    //
                    return Result;
                }
                //
                _db.AreaRules.Remove(CurAreaRule);
                _db.Entry(CurAreaRule).State = EntityState.Deleted;
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

        public GetAreaRuleDTO? GetAreaRuleById(int Id)
        {
            AreaRule? SelectedAreaRule = _db.AreaRules.Find(Id);
            //
            if (SelectedAreaRule == null)
            {
                return null;
            }
            //
            return new GetAreaRuleDTO()
            {
                Id = SelectedAreaRule.Id,
                Area = _AreaService.GetAreaById(SelectedAreaRule.AreaId),
                Year = SelectedAreaRule.Year,
                EndTime = SelectedAreaRule.EndTime,
                StartTime = SelectedAreaRule.StartTime,
                TaxFee = SelectedAreaRule.TaxFee
            };
        }

        public List<GetAreaRuleDTO> GetAreaRules()
        {
            return _db.AreaRules.OrderBy(x => x.AreaId).ThenBy(x => x.StartTime).Select(x => new GetAreaRuleDTO()
            {
                Id = x.Id,
                Area = _AreaService.GetAreaById(x.AreaId),
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Year = x.Year,
                TaxFee = x.TaxFee
            }).ToList();
        }

        public List<GetAreaRuleDTO> GetAreaRules(int AreaId)
        {
            return _db.AreaRules.OrderBy(x => x.AreaId).ThenBy(x => x.StartTime).Where(x => x.AreaId.Equals(AreaId)).Select(x => new GetAreaRuleDTO()
            {
                Id = x.Id,
                Area = _AreaService.GetAreaById(x.AreaId),
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Year = x.Year,
                TaxFee = x.TaxFee
            }).ToList();
        }

        public ResultDTO<GetAreaRuleDTO> UpdateAreaRule(UpdateAreaRuleDTO model)
        {
            ResultDTO<GetAreaRuleDTO> Result = new ResultDTO<GetAreaRuleDTO>()
            {
                Data = new GetAreaRuleDTO()
            };
            //
            try
            {
                bool ExistAreaRule = _db.AreaRules.Any(x => !x.Id.Equals(model.Id) && x.AreaId.Equals(model.AreaId) && x.Year.Equals(model.Year) && ((x.StartTime <= model.StartTime && model.StartTime <= x.EndTime) || (x.StartTime <= model.EndTime && model.EndTime <= x.EndTime) || (x.StartTime <= model.StartTime && x.EndTime >= model.EndTime) || (x.StartTime >= model.StartTime && x.EndTime <= model.EndTime)));
                //
                if (ExistAreaRule)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Information interference has occurred";
                    //
                    return Result;
                }
                //
                AreaRule CurAreaRule = _db.AreaRules.Find(model.Id);
                //
                if (CurAreaRule == null)
                {
                    Result.IsSuccess = false;
                    Result.Message = "Requested Area Rule Not Found.";
                    //
                    return Result;
                }
                //
                CurAreaRule.AreaId = model.AreaId;
                CurAreaRule.StartTime = model.StartTime;
                CurAreaRule.EndTime = model.EndTime;
                CurAreaRule.Year = model.Year;
                CurAreaRule.TaxFee = model.TaxFee;
                //
                _db.Entry(CurAreaRule).State = EntityState.Modified;
                //
                _db.SaveChanges();
                //
                Result.IsSuccess = true;
                Result.Data = GetAreaRuleById(model.Id);
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
