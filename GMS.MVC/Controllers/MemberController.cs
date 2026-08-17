using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;

namespace Presentation.MVC.Controllers {
    public class MemberController(IServiceManger serviceManger) : Controller {
        public async Task<ActionResult> Index() {
            var members = await serviceManger.MemberService.GetAllMembers();
            return View(members);
        }
    }
}
