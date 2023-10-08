using Application.DTOs.Areas;
using Application.DTOs.Results;
using Application.DTOs.Vehicles;
using Application.Interfaces.Areas;
using Microsoft.AspNetCore.Mvc;

namespace TaxCalculatorWebsite.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _AreaService;
        public AreaController(IAreaService AreaService)
        {
            _AreaService = AreaService;
        }
        public IActionResult Index()
        {
            return View(_AreaService.GetAreas());
        }
        public IActionResult Details(int id)
        {
            GetAreaDTO CurArea = _AreaService.GetAreaById(id);
            //
            if (CurArea == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                //
                return RedirectToAction("", "Area");
            }
            //
            return View(CurArea);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAreaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetAreaDTO> Result = new ResultDTO<GetAreaDTO>();
            //
            Result = _AreaService.CreateArea(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details", "Area", new { id = Result.Data.Id });
            }
            //
            TempData["CreateError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var CurArea = _AreaService.GetAreaById(id);
            //
            if (CurArea == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "Area");
            }
            //
            UpdateAreaDTO model = new UpdateAreaDTO()
            {
                Id = CurArea.Id,
                Name = CurArea.Name,
                MaxTaxFee = CurArea.MaxTaxFee,
                Order = CurArea.Order
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateAreaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetAreaDTO> Result = new ResultDTO<GetAreaDTO>();
            //
            Result = _AreaService.UpdateArea(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details", "Area", new { id = Result.Data.Id });
            }
            //
            TempData["EditError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            GetAreaDTO CurArea = _AreaService.GetAreaById(id);
            //
            if (CurArea == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                //
                return RedirectToAction("", "Area");
            }
            //
            DeleteAreaDTO model = new DeleteAreaDTO()
            {
                Id = CurArea.Id,
                Name = CurArea.Name
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteAreaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO Result = new ResultDTO();
            //
            Result = _AreaService.DeleteArea(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Index", "Area");
            }
            //
            TempData["DeleteError"] = Result.Message;
            //
            return View(model);
        }
    }
}
