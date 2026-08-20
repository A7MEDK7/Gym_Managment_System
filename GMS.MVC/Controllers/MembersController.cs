using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Shared.DTOs.MemberDTOs;

namespace Presentation.MVC.Controllers {
    public class MembersController(IServiceManger serviceManger) : Controller {
        public async Task<ActionResult> Index() {
            var members = await serviceManger.MemberService.GetAllMembers();
            return View(members);
        }
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
        public ActionResult Create() {
            return View();
        }

        [HttpPost] // Get DTO From Client Side Then Create The Member
        public async Task<ActionResult> CreateMember(CreateMemberDTO createMemberDTO) {

            if(!ModelState.IsValid) {
                ModelState.AddModelError("DataInvalid", "Check The Data And Missing Fields");
                return View(nameof(Create), createMemberDTO);
            }

            // Create The Memeber
            var result = await serviceManger.MemberService.CreateMember(createMemberDTO);

            if(result) {
                TempData["SuccessMessage"] = "Member Created Successfully";
            } else {
                TempData["ErorrMessage"] = $"Create Member Failed, Check The Phone Or Email";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}