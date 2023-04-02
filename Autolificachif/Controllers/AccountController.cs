using Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace Authentication.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private SqlDbContext db;
        public AccountController(SqlDbContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //При регистрации пользователю будет присваиваться роль "user", которая, как ожидается, добавляется в базу данных с помощью инициализации в классе Startup.


        public async Task<IActionResult> Login(Login model)
        {
            //if (ModelState.IsValid)
            //{
            //    User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            //    if (user != null)
            //    {
            //        await Authenticate(user);
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //}
            //return View(model);
            if (ModelState.IsValid)
            {

                #region
                User uuser = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (uuser != null)
                {
                    if (uuser.Email == "admin@mail.ru") { await Authenticate(uuser); return RedirectToAction("About", "Home"); }
                    await Authenticate(uuser); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }
            #endregion
        
     



        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            const string SessionName = "UserName";
            HttpContext.Session.SetString(SessionName, "UserName");
            //  Session["UserName"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }




        private async Task Authenticate(User  user)//(string userName)
        {
            #region


            // создаем один claim
            //var claims = new List<Claim>
            //  {
            //          new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            //    };
            // создаем объект ClaimsIdentity
            //ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            #endregion

            #region
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
             ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            //При регистрации пользователю будет присваиваться роль "user", которая, как ожидается, добавляется в базу данных с помощью инициализации в классе Startup.
            #endregion


            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            //    new Claim(ClaimTypes.Locality, user.City),
            //    new Claim("company",user.Company)
            // };

        //    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
        //        ClaimsIdentity.DefaultRoleClaimType);
        //// установка аутентификационных куки
        //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }



        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Regist model)
        {
            if (ModelState.IsValid)
            {
                //  User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                    //{
                    //    await Authenticate(user); // аутентификация

                    //    return RedirectToAction("Index", "Home");
                    //}
                    // добавляем пользователя в бд
                    //user = new User
                    //{
                    //    Email = model.Email,
                    //    Password = model.Password,
                    //    Year = model.Year,
                    //    City = model.City,
                    //    Company = model.Company
                    //};

                db.Users.Add(user);
                await db.SaveChangesAsync();

                await Authenticate(user);

                return RedirectToAction("Index", "Home");

            }
            else
            {



                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);

            #region

            //if (ModelState.IsValid) 
            //{
            //    //Вначале смотрим, а есть ли с таким же email в базе данных какой-либо пользователь, если такой пользователь имеется в БД, то выполняем аутентификацию и устанавливаем аутентификационные куки
            //      User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            // //   user.RegistiD = new List<Regist> { model };

            //    if (user == null)
            //    {



            //        // добавляем пользователя в бд

            //    // db.Users.Add(new User { Email=model.Email,Password=model.Password});
            //        // добавляем пользователя в бд


            //        user = new User { Email = model.Email, Password = model.Password };
            //        Role userRole = await db.Roles.FirstOrDefaultAsync(r=>r.Name == "user");
            //        if (userRole != null)
            //            user.Role = userRole;

            //        db.Users.Add(user);
            //       await db.SaveChangesAsync();
            //        await Authenticate(user);
            //        // await Authenticate(model.Email); // аутентификация

            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Пользователь с таким email уже зарегистрирован");
            //    }
            //}
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            //    // Используйте список errors для вывода сообщений об ошибках валидации
            //}
            //return View(model);
            #endregion
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }

}
