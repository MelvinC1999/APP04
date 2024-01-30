using AbstracFactory_RedSocial_DAO_DTO_Docker.DataAccessObject;
using AbstracFactory_RedSocial_DAO_DTO_Docker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker.Controllers
{
    public class HomeController : Controller
    {
        DAO_RedeSociales Dao;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Dao = new DAO_RedeSociales();

            //Dao.FeachtDataFacebook();
            List<string> datosYouTube = await Dao.FeacthDataYoutube();
            ViewData["DatosYouTube"] = datosYouTube;

            return View();
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