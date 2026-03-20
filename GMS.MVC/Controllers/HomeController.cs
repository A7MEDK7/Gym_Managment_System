using GMS.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contract;
using Services.Implmentations;
using System.Diagnostics;

namespace GMS.MVC.Controllers {
    public class HomeController(IAnalyticsService _analyticsService, ILogger<HomeController> _logger) : Controller {

        public async Task<IActionResult> Index() {
            var analyticData = await _analyticsService.GetAnalyticData();
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
