using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Shared.DTOs.TrainerDTOs;

namespace GMS.MVC.Controllers {
    public class TrainersController(IServiceManger serviceManger) : Controller {

        #region ==== Get All Trainers ====
        public async Task<ActionResult> Index() {
            var trainers = await serviceManger.TrainerService.GetAllTrainers();
            return View(trainers);
        } 
        #endregion

        #region ==== Get Trainer Details ====
        public async Task<ActionResult> TrainerDetails(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = await serviceManger.TrainerService.GetTrainerDetails(id);
            if (Trainer is null) {
                TempData["ErorrMessage"] = $"Trainer With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);
        }
        #endregion

        #region ==== Create Trainer ====
        public ActionResult Create() {
            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Create The Trainer
        public async Task<ActionResult> CreateTrainer(CreateTrainerDTO createTrainerDTO) {

            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(Create), createTrainerDTO);
            }

            // Create The Trainer
            var result = await serviceManger.TrainerService.CreateTrainer(createTrainerDTO);

            if (result) {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Create Trainer Failed, Check The Phone Or Email";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ==== Edit Trainer ====
        public async Task<ActionResult> EditTrainer(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = await serviceManger.TrainerService.GetTrainerToUpdate(id);

            if (Trainer is null) {
                TempData["ErorrMessage"] = $"Trainer With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(Trainer);
        }

        [HttpPost] // Get DTO From Client Side Then Update The Trainer
        public async Task<ActionResult> EditTrainer([FromRoute] int id, TrainerToUpdateDTO TrainerToUpdateDTO) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(EditTrainer), TrainerToUpdateDTO);
            }

            // Update The Memeber
            var result = await serviceManger.TrainerService.UpdateTrainerDetails(TrainerToUpdateDTO, id);

            if (result) {
                TempData["SuccessMessage"] = "Trainer Updated Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Update Trainer Failed, Check The Data";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ==== Delete Trainer ====
        public async Task<ActionResult> Delete(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = await serviceManger.TrainerService.GetTrainerDetails(id);

            if (Trainer is null) {
                TempData["ErorrMessage"] = $"Trainer With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerName = Trainer.Name;
            ViewBag.Id = Trainer.Id;

            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Delete The Trainer
        public async Task<ActionResult> DeleteTrainer([FromForm] int id) {

            var result = await serviceManger.TrainerService.RemoveTrainer(id);

            if (result) {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Delete Trainer Failed, Check The Active Session";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
