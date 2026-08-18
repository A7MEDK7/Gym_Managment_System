using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;

namespace Presentation.MVC.Controllers {
    public class MembersController(IServiceManger serviceManger) : Controller {
        public async Task<ActionResult> Index() {
            var members = await serviceManger.MemberService.GetAllMembers();
            return View(members);
        }

        public async Task<ActionResult> MemberDetails(int id) {
            if(id <= 0)
                return RedirectToAction(nameof(Index));

            var member = await serviceManger.MemberService.GetMemberDetailsById(id);

            if(member is null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }
    }
}

