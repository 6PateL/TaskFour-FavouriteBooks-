using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskFour_FavouriteBooks_.Data;
using TaskFour_FavouriteBooks_.Models;

namespace TaskFour_FavouriteBooks_.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext _db;

        public RegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ActionName("Register")]
        public IActionResult RegisterPost(User user)
        {
            if (ModelState.IsValid)
            {
                if(_db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Пользователь с таким адресом электронной почты уже существует");
                }
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(); 
        }

        [HttpPost, ActionName("Login")]
        public IActionResult Login(User inputUser)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == inputUser.Email);
            if (ModelState.IsValid)
            {
                if(user != null)
                {
                    if(user.Password == inputUser.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Неверный пароль");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "такого пользователя не существует");
                }
            }
            return View();
        }
    }
}
