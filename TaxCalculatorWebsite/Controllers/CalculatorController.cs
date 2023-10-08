using Application.DTOs.Results;
using Application.DTOs.TaxCalculators;
using Application.Interfaces.Areas;
using Application.Interfaces.Calculators;
using Application.Interfaces.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxCalculatorWebsite.Models.ChildModels.Calculators;

namespace TaxCalculatorWebsite.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _CalculatorService;
        private readonly IAreaService _AreaService;
        private readonly IVehicleService _VehicleService;
        public CalculatorController(ICalculatorService CalculatorService, IAreaService AreaService, IVehicleService VehicleService)
        {
            _CalculatorService = CalculatorService;
            _AreaService = AreaService;
            _VehicleService = VehicleService;
        }
        public IActionResult Index()
        {
            GetTaxCalculatorLocalDTO model = new GetTaxCalculatorLocalDTO()
            {
                Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),
                Vehicles = _VehicleService.GetVehicles().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),
                PassDateTimeList = new List<GetTaxCalculatorDatesDTO> { new GetTaxCalculatorDatesDTO() },
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(GetTaxCalculatorLocalDTO model)
        {
            model.Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            //
            model.Vehicles = _VehicleService.GetVehicles().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            //
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetTaxCalculatorDTO> Result = new ResultDTO<GetTaxCalculatorDTO>();
            //
            Result = _CalculatorService.TaxCalculator(new GetTaxCalculatorDTO()
            {
                AreaId = model.AreaId,
                PassDateTimeList = model.PassDateTimeList,
                VehicleId = model.VehicleId,
            });
            //
            if (Result.IsSuccess)
            {
                ViewBag.State = "Result";
                model.TaxFee = Result.Data.TaxFee;
                model.RealTaxFee = Result.Data.RealTaxFee;
            }
            //
            return View(model);
        }
    }
}
