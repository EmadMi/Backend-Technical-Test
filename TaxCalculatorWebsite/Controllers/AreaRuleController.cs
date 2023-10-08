using Application.DTOs.AreaRules;
using Application.DTOs.Areas;
using Application.DTOs.Results;
using Application.Interfaces.AreaRules;
using Application.Interfaces.Areas;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxCalculatorWebsite.Models.ChildModels.AreaRules;

namespace TaxCalculatorWebsite.Controllers
{
    public class AreaRuleController : Controller
    {
        private readonly IAreaRuleService _AreaRuleService;
        private readonly IAreaService _AreaService;

        public AreaRuleController(IAreaRuleService AreaRuleService,IAreaService AreaService)
        {
            _AreaRuleService = AreaRuleService;
            _AreaService = AreaService;
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.AreaId = id.Value;
                return View(_AreaRuleService.GetAreaRules(id.Value));
            }
            //
            return View(_AreaRuleService.GetAreaRules());
        }
        public IActionResult Details(int id)
        {
            GetAreaRuleDTO CurAreaRule = _AreaRuleService.GetAreaRuleById(id);
            //
            if (CurAreaRule == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                //
                return RedirectToAction("", "AreaRule");
            }
            //
            return View(CurAreaRule);
        }
        public IActionResult Create(int? id)
        {
            CreateAreaRulesLocalDTO model = new CreateAreaRulesLocalDTO()
            {
                Year = DateTime.Now.Year,
                AreaId = (id.HasValue ? id.Value : 0),
                Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateAreaRulesLocalDTO model)
        {
            model.Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            //
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetAreaRuleDTO> Result = new ResultDTO<GetAreaRuleDTO>()
            {
                Data = new GetAreaRuleDTO()
            };
            //
            Result = _AreaRuleService.CreateAreaRule(new CreateAreaRuleDTO()
            {
                AreaId = model.AreaId,
                EndTime = model.EndTime,
                StartTime = model.StartTime,
                TaxFee = model.TaxFee,
                Year = model.Year
            });
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details","AreaRule",new { id = Result.Data.Id });
            }
            //
            TempData["CreateError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var CurAreaRule = _AreaRuleService.GetAreaRuleById(id);
            //
            if (CurAreaRule == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                return RedirectToAction("", "AreaRule");
            }
            //
            UpdateAreaRuleLocalDTO model = new UpdateAreaRuleLocalDTO()
            {
                Id= CurAreaRule.Id,
                AreaId = CurAreaRule.Area.Id,
                StartTime= CurAreaRule.StartTime,
                EndTime= CurAreaRule.EndTime,
                TaxFee= CurAreaRule.TaxFee,
                Year= CurAreaRule.Year,
                Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateAreaRuleLocalDTO model)
        {
            model.Areas = _AreaService.GetAreas().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            //
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO<GetAreaRuleDTO> Result = new ResultDTO<GetAreaRuleDTO>();
            //
            Result = _AreaRuleService.UpdateAreaRule(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Details", "AreaRule", new { id = Result.Data.Id });
            }
            //
            TempData["EditError"] = Result.Message;
            //
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            GetAreaRuleDTO CurAreaRule = _AreaRuleService.GetAreaRuleById(id);
            //
            if (CurAreaRule == null)
            {
                TempData["Error"] = "Parametr is wrong.";
                //
                return RedirectToAction("", "AreaRule");
            }
            //
            DeleteAreaRuleDTO model = new DeleteAreaRuleDTO()
            {
                Id = CurAreaRule.Id,
                StartTime = CurAreaRule.StartTime,
                Area = CurAreaRule.Area,
                EndTime = CurAreaRule.EndTime,
                TaxFee = CurAreaRule.TaxFee,
                Year = CurAreaRule.Year
            };
            //
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteAreaRuleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //
            ResultDTO Result = new ResultDTO();
            //
            Result = _AreaRuleService.DeleteAreaRule(model);
            //
            if (Result.IsSuccess)
            {
                return RedirectToAction("Index", "AreaRule" +
                    "");
            }
            //
            TempData["DeleteError"] = Result.Message;
            //
            return View(model);
        }
    }
}
