using Application.DTOs.Results;
using Application.DTOs.Vehicles;
using Application.Interfaces.Vehicles;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;

namespace TaxCalculatorWebsite.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _VehicleService;
        public VehicleController(IVehicleService VehicleService)
        {
            _VehicleService = VehicleService;
        }
        public IActionResult Index()
        {
            return View(_VehicleService.GetVehicles());
        }
        public IActionResult Details(int id)
        {
            var CurVehicle = _VehicleService.GetVehicleById(id);
            //
            if (CurVehicle == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "Vehicle");
            }
            //
            return View(CurVehicle);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVehicleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetVehicleDTO> Result = new ResultDTO<GetVehicleDTO>();
            //
            Result = _VehicleService.CreateVehicle(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details", "Vehicle", new { id = Result.Data.Id });
            }
            //
            TempData["CreateError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var CurVehicle = _VehicleService.GetVehicleById(id);
            //
            if (CurVehicle == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "Vehicle");
            }
            //
            UpdateVehicleDTO model = new UpdateVehicleDTO()
            {
                Id = CurVehicle.Id,
                Name = CurVehicle.Name,
                IsTaxFree = CurVehicle.IsTaxFree,
                Order = CurVehicle.Order
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateVehicleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetVehicleDTO> Result = new ResultDTO<GetVehicleDTO>();
            //
            Result = _VehicleService.UpdateVehicle(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details", "Vehicle", new { id = Result.Data.Id });
            }
            //
            TempData["EditError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var CurVehicle = _VehicleService.GetVehicleById(id);
            //
            if (CurVehicle == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "Vehicle");
            }
            //
            DeleteVehicleDTO model = new DeleteVehicleDTO()
            {
                Id = CurVehicle.Id,
                Name = CurVehicle.Name,
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteVehicleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO Result = new ResultDTO();
            //
            Result = _VehicleService.DeleteVehicle(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Index", "Vehicle");
            }
            //
            TempData["DeleteError"] = Result.Message;
            //
            return View(model);
        }
    }
}
