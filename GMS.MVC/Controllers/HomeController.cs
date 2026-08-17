using GMS.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Services.Implmentations;
using System.Diagnostics;

namespace GMS.MVC.Controllers {
    public class HomeController(IServiceManger serviceManger, ILogger<HomeController> _logger) : Controller {

        public async Task<IActionResult> Index() {
            var analyticData = await serviceManger.AnalyticsService.GetAnalyticData();
            return View(analyticData);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
