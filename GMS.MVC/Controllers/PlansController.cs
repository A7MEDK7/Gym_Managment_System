using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Shared.DTOs.MemberDTOs;
using Shared.DTOs.PlanDTOs;

namespace GMS.MVC.Controllers {
    public class PlansController(IServiceManger serviceManger) : Controller {
        
        #region ==== Get Plan Details & All Plan ====
        public async Task<IActionResult> Index() {
            var plans = await serviceManger.PlanService.GetAllPlans();
            return View(plans);
        }
        public async Task<ActionResult> Details(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var plan = await serviceManger.PlanService.GetPlanById(id);
            if (plan is null) {
                TempData["ErorrMessage"] = $"Plan With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        } 
        #endregion

        #region ==== Edit ====
        public async Task<ActionResult> Edit(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var plan = await serviceManger.PlanService.GetPanToUpdate(id);

            if (plan is null) {
                TempData["ErorrMessage"] = $"Plan With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost] // Get DTO From Client Side Then Update The Plan
        public async Task<ActionResult> Edit([FromRoute] int id, UpdatePlanDTO updatePlanDTO) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(Edit), updatePlanDTO);
            }

            // Update The Memeber
            var result = await serviceManger.PlanService.UpdatePlan(id, updatePlanDTO);

            if (result) {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Update Plan Failed, Check The Data";
            }

            return RedirectToAction(nameof(Index));
        } 
        #endregion

        #region ==== Activate & DeActivate ====
        [HttpPost]
        public async Task<ActionResult> Activate(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var result = await serviceManger.PlanService.ToggleStatus(id);

            if (result) {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Update Plan Failed, Check The Data";
            }

            return RedirectToAction(nameof(Index));
        } 
        #endregion

    }
}
