using Application.DTOs.AreaRules;
using Application.DTOs.Holidays;
using Application.DTOs.Results;
using Application.DTOs.TaxCalculators;
using Application.Interfaces.AreaRules;
using Application.Interfaces.Areas;
using Application.Interfaces.Calculators;
using Application.Interfaces.Holidays;
using Application.Interfaces.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Calculators
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IVehicleService _VehicleService;
        private readonly IAreaService _AreaService;
        private readonly IAreaRuleService _AreaRuleService;
        private readonly IHolidayService _HolidayService;

        public CalculatorService(IVehicleService VehicleService, IAreaService AreaService, IAreaRuleService AreaRuleService,IHolidayService HolidayService)
        {
            _AreaRuleService = AreaRuleService;
            _VehicleService = VehicleService;
            _AreaService = AreaService;
            _HolidayService = HolidayService;
        }
        public ResultDTO<GetTaxCalculatorDTO> TaxCalculator(GetTaxCalculatorDTO model)
        {
            model.PassDateTimeList = model.PassDateTimeList.OrderBy(x => x.PassDateTime).ToList();
            //
            DateTime FirstEnternce = model.PassDateTimeList.FirstOrDefault().PassDateTime;
            int FirstEnteranceYear = FirstEnternce.Year;
            //
            int MaximumTaxForArea = _AreaService.GetAreaById(model.AreaId).MaxTaxFee;
            model.RealTaxFee = 0;
            //
            List<string>? Messages = new List<string>();
            //
            foreach (var PassDate in model.PassDateTimeList)
            {
                string NextFeeMessage = "";
                int NextFee = GetTollFee(FirstEnteranceYear, model.AreaId, model.VehicleId, PassDate.PassDateTime, out NextFeeMessage);
                string TempFeeMessage = "";
                int TempFee = GetTollFee(FirstEnteranceYear, model.AreaId, model.VehicleId, FirstEnternce, out TempFeeMessage);
                //
                Messages.Add(NextFeeMessage);
                //
                long Minutes = (long)(PassDate.PassDateTime - FirstEnternce).TotalMinutes;

                if (Minutes <= 60)
                {
                    if (model.RealTaxFee > 0) model.RealTaxFee -= TempFee;
                    if (NextFee >= TempFee) TempFee = NextFee;
                    model.RealTaxFee += TempFee;
                }
                else
                {
                    model.RealTaxFee += NextFee;
                }
            }
            //
            if (model.RealTaxFee > MaximumTaxForArea)
            {
                model.TaxFee = MaximumTaxForArea;
            }
            else
            {
                model.TaxFee = model.RealTaxFee;
            }
            //
            return new ResultDTO<GetTaxCalculatorDTO>()
            {
                Data = model,
                IsSuccess = true
            };
        }
        private int GetTollFee(int FirstEnteranceYear, int AreaId, int VehicleId, DateTime Date, out string Message)
        {
            if (IsTollDateIsFree(Date) || IsTollFreeVehicle(VehicleId)) 
            {
                Message = "It's Free For You";
                return 0; 
            }
            //
            TimeSpan RequestedTimeSpan = new TimeSpan(Date.Hour, Date.Minute, 00);
            //
            List<GetAreaRuleDTO> RuleList = _AreaRuleService.GetAreaRules(AreaId);
            //
            int? TollFee = null;
            var TollFeeList = new List<GetAreaRuleDTO>();
            //
            if (RuleList.Any(x => x.Year.Equals(FirstEnteranceYear)))
            {
                Message = "";
                TollFee = RuleList.Where(x => ((x.StartTime <= RequestedTimeSpan && x.EndTime >= RequestedTimeSpan) || (x.StartTime >= RequestedTimeSpan && x.EndTime <= RequestedTimeSpan)) && x.Year.Equals(FirstEnteranceYear)).FirstOrDefault()?.TaxFee;
                TollFeeList = RuleList.Where(x => ((x.StartTime <= RequestedTimeSpan && x.EndTime >= RequestedTimeSpan) || (x.StartTime >= RequestedTimeSpan && x.EndTime <= RequestedTimeSpan)) && x.Year.Equals(FirstEnteranceYear)).ToList();
            
            }
            else
            {
                Message = "This Calculation is for " + RuleList.Max(x => x.Year).ToString() + " year";
                //
                TollFee = RuleList.OrderByDescending(x => x.Year).Where(x => (x.StartTime <= RequestedTimeSpan && x.EndTime >= RequestedTimeSpan) || (x.StartTime >= RequestedTimeSpan && x.EndTime <= RequestedTimeSpan)).FirstOrDefault()?.TaxFee;
                TollFeeList = RuleList.Where(x => ((x.StartTime <= RequestedTimeSpan && x.EndTime >= RequestedTimeSpan) || (x.StartTime >= RequestedTimeSpan && x.EndTime <= RequestedTimeSpan)) && x.Year.Equals(FirstEnteranceYear)).ToList();
            }
            //
            if (TollFee == null)
            {
                TollFee = 0;
            }
            //
            return TollFee.Value;
        }
        private Boolean IsTollFreeVehicle(int VehicleId)
        {
            var SelectedVehicle = _VehicleService.GetVehicleById(VehicleId);
            //
            if (SelectedVehicle == null)
            {
                return false;
            }
            //
            return SelectedVehicle.IsTaxFree;
        }
        private Boolean IsTollDateIsFree(DateTime Date)
        {
            if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday) return true;
            //
            List<GetHolidayDTO> HolidaysList = _HolidayService.GetHolidays();
            //
            if (HolidaysList.Any(x => (x.HolyDate.Equals(Date) || x.HolyDate.AddDays(-1).Equals(Date))) || Date.Month == 7)
            {
                return true;
            }
            //
            return false;
        }
    }
}
