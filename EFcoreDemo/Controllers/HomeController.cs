using System.Diagnostics;
using EFcoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EFcoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PositionOptions _pos;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IOptions<PositionOptions> pos, IConfiguration config)
        {
            _logger = logger;
            _pos = pos.Value;
            _config = config;
        }

        public IActionResult Index()
        {
            var title = _config["Position:Title"];
            var name = _config["Position:Name"];
            ViewBag.Title = _pos.Title;
            ViewBag.Name = _pos.Name;
            return View(_pos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
