using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Shared.DTOs.MemberDTOs;

namespace Presentation.MVC.Controllers {
    public class MembersController(IServiceManger serviceManger) : Controller {

        #region ==== Get All Members ====
        public async Task<ActionResult> Index() {
            var members = await serviceManger.MemberService.GetAllMembers();
            return View(members);
        } 
        #endregion

        #region ==== Get Member Details ====
        public async Task<ActionResult> MemberDetails(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var member = await serviceManger.MemberService.GetMemberDetailsById(id);
            if (member is null) {
                TempData["ErorrMessage"] = $"Member With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        public async Task<ActionResult> HealthRecordDetails(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var healthRecord = await serviceManger.MemberService.GetMemberHealthRecordDTO(id);

            if (healthRecord is null) {
                TempData["ErorrMessage"] = $"Member With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        } 
        #endregion

        #region ==== Create Member ====
        public ActionResult Create() {
            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Create The Member
        public async Task<ActionResult> CreateMember(CreateMemberDTO createMemberDTO) {

            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(Create), createMemberDTO);
            }

            // Create The Memeber
            var result = await serviceManger.MemberService.CreateMember(createMemberDTO);

            if (result) {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Create Member Failed, Check The Phone Or Email";
            }

            return RedirectToAction(nameof(Index));
        } 
        #endregion

        #region ==== Edit Member ====
        public async Task<ActionResult> EditMember(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var member = await serviceManger.MemberService.GetMemberToUpdate(id);

            if (member is null) {
                TempData["ErorrMessage"] = $"Member With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost] // Get DTO From Client Side Then Update The Member
        public async Task<ActionResult> EditMember([FromRoute] int id, MemberToUpdateDTO memberToUpdateDTO) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(EditMember), memberToUpdateDTO);
            }

            // Update The Memeber
            var result = await serviceManger.MemberService.UpdateMemberDetails(id, memberToUpdateDTO);

            if (result) {
                TempData["SuccessMessage"] = "Member Updated Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Update Member Failed, Check The Data";
            }

            return RedirectToAction(nameof(Index));
        } 
        #endregion

        #region ==== Delete Member ====
        public async Task<ActionResult> Delete(int id) {
            if (id <= 0) {
                TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative Value";
                return RedirectToAction(nameof(Index));
            }
            var member = await serviceManger.MemberService.GetMemberDetailsById(id);

            if (member is null) {
                TempData["ErorrMessage"] = $"Member With Id {id} Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberName = member.Name;
            ViewBag.Id = member.Id;

            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Delete The Member
        public async Task<ActionResult> DeleteMember([FromForm] int id) {

            var result = await serviceManger.MemberService.RemoveMember(id);

            if (result) {
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            }
            else {
                TempData["ErorrMessage"] = $"Delete Member Failed, Check The Active Session";
            }

            return RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}