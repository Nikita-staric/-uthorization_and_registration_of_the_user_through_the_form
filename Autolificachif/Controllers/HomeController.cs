using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Authentication.Controllers
{
    public class HomeController : Controller
    {
        //private SqlDbContext db;
        //public AccountController(SqlDbContext context)
        //{
        //    db = context;
        //}



        //  [Authorize]

        [Authorize(Roles = "admin, user")]
       // [Authorize(Policy = "OnlyForLondon")]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            
            ViewBag.UserName = User.Identity.Name;//RedirectToAction("Login","Account");
            return Content($"ваша роль: {role}");

           // return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            return Content("Вход только для администратора");
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