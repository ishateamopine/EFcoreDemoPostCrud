using EFcoreDemo.Models.ConfigOptions;
using EFcoreDemo.Models.ErrorViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace EFcoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConfigOptions _pos;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IOptions<ConfigOptions> pos, IConfiguration config)
        {
            _logger = logger;
            _pos = pos.Value;
            _config = config;
        }
        #region
        /// <summary>
        // Displays the home page with position details.
        /// </summary>
        public IActionResult Index()
        {
            var title = _config["Position:Title"];
            var name = _config["Position:Name"];
            return View(_pos);
        }
        #endregion
        #region
        /// <summary>
        // Displays the privacy policy page.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }
        #endregion
        #region
        /// <summary>
        // Handles errors and displays the error view.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
