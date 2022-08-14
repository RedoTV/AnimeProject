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
            if (HttpContext.Request.Cookies.ContainsKey("animeid"))
            {
                HttpContext.Response.Cookies.Delete("animeid");
            }
            List<Anime> animes = AnimeDB.Animes.ToList();
            return View(animes);
        }



        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Index(string animeName, string animeDiscription, string genres, IFormFile photo)
        {
            string pathToWebRoot = env.WebRootPath;
            string fullPath = Path.Combine(pathToWebRoot, "priview_img", animeName + ".png");
            using (FileStream imgStream = new FileStream(fullPath, FileMode.Create))
            {
                await photo.CopyToAsync(imgStream);
            }
            Anime animeModel = new Anime();
            animeModel.AnimeName = animeName;
            animeModel.AnimeDescription = animeDiscription;
            animeModel.Genres = genres;
            animeModel.Priority = 1;

            string imgInRoot = Path.Combine("priview_img", animeName + ".png");
            animeModel.PathToPreview = imgInRoot;

            AnimeDB.Animes.Add(animeModel);
            await AnimeDB.SaveChangesAsync();
            return Redirect("~/");
        }
    }
}
