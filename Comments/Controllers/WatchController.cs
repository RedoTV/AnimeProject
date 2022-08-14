using Animes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animes.Controllers
{
    public class WatchController : Controller
    {
        AnimeContext AnimeDB;
        IWebHostEnvironment env;
        public WatchController(AnimeContext animeDBContext, IWebHostEnvironment env)
        {
            AnimeDB = animeDBContext;
            this.env = env;
        }

        public IActionResult ChoseSeries(int animeid)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("animeid"))
            {
                HttpContext.Response.Cookies.Append("animeid", animeid.ToString());
            }
            List<Anime> animes = AnimeDB.Animes.ToList();
            Anime anime = animes[animeid];
            List<AnimeSeries> animeSeries = AnimeDB.Series.Where(x => x.Anime == anime).Select(x=>x).ToList();
            return View(animeSeries);
        }

        public IActionResult WatchSeries(int seriesNumber)
        {
            List<Anime> animes = AnimeDB.Animes.ToList();
            int animeid = int.Parse(HttpContext.Request.Cookies["animeid"]);
            Anime currentAnime = animes[animeid];
            List<AnimeSeries> animeSeries = AnimeDB.Series.Where(x => x.Anime == currentAnime).Select(x => x).ToList();
            AnimeSeries oneSeries = animeSeries[seriesNumber];
            return View(oneSeries);
        }
    }
}
