using Microsoft.AspNetCore.Mvc;
using Animes.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace Animes.Controllers
{
    public class HomeController : Controller
    {
        AnimeContext AnimeDB;
        IWebHostEnvironment env;
        public HomeController(AnimeContext animeDBContext, IWebHostEnvironment env)
        {
            AnimeDB = animeDBContext;
            this.env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Anime> animes = AnimeDB.Animes.ToList();
            return View(animes);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
