using Animes.HelperClasses;
using Animes.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animes.Controllers
{
    public class LoginController : Controller
    {
        AnimeContext AnimeDB;
        UserContext UserContext;
        IWebHostEnvironment env;
        public LoginController(AnimeContext animeDBContext, IWebHostEnvironment env, UserContext userContext)
        {
            AnimeDB = animeDBContext;
            this.env = env;
            UserContext = userContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login,string password)
        {
            User? person = UserContext.Users.FirstOrDefault(p => p.Login == login && p.Password == password);
            bool personNull = true;
            if (person == null) { Results.Unauthorized(); }
            else
            {
                personNull = false;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            return personNull ? View() : Redirect("/");
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login,string password)
        {
            User person = new User();
            person.Login = login;
            person.Password = password;
            person.Role = "admin";
            UserContext.Users.Add(person);
            UserContext.SaveChangesAsync();
            return Redirect("/login/login");
        }
        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
