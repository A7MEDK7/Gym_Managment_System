using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Abstraction.Contract;
using Shared.DTOs.PlanDTOs;
using Shared.DTOs.SessionDTOs;
using Shared.DTOs.TrainerDTOs;

namespace GMS.MVC.Controllers {
    public class SessionController(IServiceManger serviceManger) : Controller {

        #region ==== Get Session Details & Get All Sessions ====
        public async Task<ActionResult> Index() {
            var sessions = await serviceManger.SessionService.GetAllSessions();
            return View(sessions);
        }
        public async Task<ActionResult> Details(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var session = await serviceManger.SessionService.GetSessionById(id);
            if (session is null) {
                TempData["ErorrMessage"] = $"Session With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }
        #endregion

        #region ==== Create Session ====
        public async Task<ActionResult> Create() {
            await GetCategoriesForDropdown();
            await GetTrainersForDropdown();
            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Create The Session
        public async Task<ActionResult> Create(CreateSessionDTO createSessionDTO) {

            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                await GetCategoriesForDropdown();
                await GetTrainersForDropdown();
                return View(nameof(Create), createSessionDTO);
            }

            // Create The Session
            var result = await serviceManger.SessionService.CreateSession(createSessionDTO);

            if (result) {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else {
                TempData["ErorrMessage"] = $"Create Session Failed, Check The Data";
                await GetCategoriesForDropdown();
                await GetTrainersForDropdown();
                return View(nameof(Create), createSessionDTO);
            }
        }
        #endregion

        #region ==== Edit Session ====
        public async Task<ActionResult> Edit(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var plan = await serviceManger.SessionService.GetSessionToUpdate(id);

            if (plan is null) {
                TempData["ErorrMessage"] = $"Session With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            // Get Only Trainers
            await GetTrainersForDropdown();

            return View(plan);
        }

        [HttpPost] // Get DTO From Client Side Then Update The Session
        public async Task<ActionResult> Edit([FromRoute] int id, UpdateSessionDTO updateSessionDTO) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                await GetTrainersForDropdown();
                return View(nameof(Edit), updateSessionDTO);
            }

            // Update The Memeber
            var result = await serviceManger.SessionService.UpdateSession(updateSessionDTO, id);

            if (result) {
                TempData["SuccessMessage"] = "Session Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else {
                TempData["ErorrMessage"] = $"Update Session Failed, Check The Data";
                await GetTrainersForDropdown();
                return View(nameof(Edit), updateSessionDTO);
            }

        }
        #endregion

        #region ==== Delete Session ====
        public async Task<ActionResult> Delete(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var session = await serviceManger.SessionService.GetSessionById(id);

            if (session is null) {
                TempData["ErorrMessage"] = $"Session With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionName = session.CategoryName;
            ViewBag.Id = session.Id;

            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Delete The Session
        public async Task<ActionResult> DeleteSession([FromForm] int id) {

            var result = await serviceManger.SessionService.RemoveSession(id);

            if (result) {
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Delete Session Failed, Check The Active Booking";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ==== Helper Method ====
        private async Task GetTrainersForDropdown() {
            var trainers = await serviceManger.SessionService.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        } 
        private async Task GetCategoriesForDropdown() {
            var categories = await serviceManger.SessionService.GetCategoriesForDropdown();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        } 
        #endregion
    }
}
