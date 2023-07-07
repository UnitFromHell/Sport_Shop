using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Sport_Shop.Controllers
{
    public class HomeController : Controller
    {
        private Sport_ShopContext ss;

        public HomeController(Sport_ShopContext context)
        {
            ss = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await ss.Users.ToListAsync());
        }

        public async Task<IActionResult> TShirt()
        {
            return View(await ss.Users.ToListAsync());
        }

        public async Task<IActionResult> Ball()
        {
            return View(await ss.Products.ToListAsync());
        }

        public async Task<IActionResult> Basket()
        {
            return View(await ss.Users.ToListAsync());
        }

        public async Task<IActionResult> Shorts()
        {
            return View(await ss.Users.ToListAsync());
        }
        public async Task<IActionResult> Sneakers()
        {
            return View(await ss.Users.ToListAsync());
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            ss.Users.Add(user);
            await ss.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await ss.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if(user != null)
                    return View(user);
            }
            return NotFound();

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await ss.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult> Edit(User user)
        {
           ss.Users.Update(user);
           await ss.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if(id != null)
            {
                User user = await ss.Users.FirstOrDefaultAsync(x =>x.IdUser == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await ss.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (user != null)
                {
                    ss.Users.Remove(user);
                    await ss.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult SignIn()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await ss.Users.FirstOrDefaultAsync(u => u.LoginUser == model.Login && u.PasswordUser == model.Password);
                if(user != null)
                {
                    HttpContext.Session.SetString("AuthUser", model.Login);
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неверный логин и/или пароль");
               
            }
            return RedirectToAction("SignIn", "Home");
        }

        public async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultNameClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("SignIn");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(User person)
        {
            if (ModelState.IsValid)
            {
                ss.Users.Add(person);
                await ss.SaveChangesAsync();
                return RedirectToAction("SignUp");
            }
            else
            {
                return View(person);
            }
            
        }
       
    }
}