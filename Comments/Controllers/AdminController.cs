using Animes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Controllers
{
    public class AdminController : Controller
    {
        AnimeContext AnimeDB;
        IWebHostEnvironment env;
        public AdminController(AnimeContext animeDBContext, IWebHostEnvironment env)
        {
            AnimeDB = animeDBContext;
            this.env = env;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddAnime()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddAnime(string animeName, string animeDiscription, string genres, IFormFile photo)
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

        [Authorize(Roles = "admin")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddSeries(int seriesNumber, IFormFile animeSeries)
        {
            List<Anime> animes = AnimeDB.Animes.ToList();
            int animeid = int.Parse(HttpContext.Request.Cookies["animeid"]);
            Anime currentAnime = animes[animeid];

            string pathToWebRoot = env.WebRootPath;
            string folderForAnime = Path.Combine(pathToWebRoot, "all_anime_series", currentAnime.AnimeName);
            if (!Directory.Exists(folderForAnime))
            {
                Directory.CreateDirectory(folderForAnime);
            }

            string fullPath = Path.Combine(pathToWebRoot, "all_anime_series", currentAnime.AnimeName, seriesNumber + ".mp4");
            using (FileStream videoStream = new FileStream(fullPath, FileMode.Create))
            {
                await animeSeries.CopyToAsync(videoStream);
            }
            string videoInRoot = Path.Combine("all_anime_series", currentAnime.AnimeName, seriesNumber + ".mp4");

            AnimeSeries animeSer = new AnimeSeries();
            animeSer.SeriesNumber = seriesNumber;
            animeSer.PathToVideo = videoInRoot;
            animeSer.Anime = currentAnime;

            AnimeDB.Series.Add(animeSer);
            await AnimeDB.SaveChangesAsync();
            return Redirect($"~/watch/choseseries?animeid={animeid}");
        }
    }
}
