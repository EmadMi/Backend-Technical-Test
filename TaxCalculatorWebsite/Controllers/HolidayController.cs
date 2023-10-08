using Application.DTOs.Areas;
using Application.DTOs.Holidays;
using Application.DTOs.Results;
using Application.Interfaces.Holidays;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;

namespace TaxCalculatorWebsite.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IHolidayService _HolidayService;
        public HolidayController(IHolidayService HolidayService)
        {
            _HolidayService = HolidayService;
        }
        public IActionResult Index()
        {
            return View(_HolidayService.GetHolidays());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateHolidayDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetHolidayDTO> Result = new ResultDTO<GetHolidayDTO>();
            //
            Result = _HolidayService.CreateHoliday(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("", "Holiday");
            }
            //
            TempData["CreateError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var CurHoliday = _HolidayService.GetHolidayById(id);
            //
            if (CurHoliday == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "Holiday");
            }
            //
            UpdateHolidayDTO model = new UpdateHolidayDTO()
            {
                Id = CurHoliday.Id,
                HolyDate = CurHoliday.HolyDate
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateHolidayDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetHolidayDTO> Result = new ResultDTO<GetHolidayDTO>();
            //
            Result = _HolidayService.UpdateHoliday(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("", "Holiday");
            }
            //
            TempData["EditError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            GetHolidayDTO CurHoliday = _HolidayService.GetHolidayById(id);
            //
            if (CurHoliday == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                //
                return RedirectToAction("", "Holiday");
            }
            //
            DeleteHolidayDTO model = new DeleteHolidayDTO()
            {
                Id = CurHoliday.Id,
                HolyDate = CurHoliday.HolyDate
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteHolidayDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO Result = new ResultDTO();
            //
            Result = _HolidayService.DeleteHoliday(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("", "Holiday");
            }
            //
            TempData["DeleteError"] = Result.Message;
            //
            return View(model);
        }
    }
}
