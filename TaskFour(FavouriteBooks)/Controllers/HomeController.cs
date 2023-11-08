using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskFour_FavouriteBooks_.Data;
using TaskFour_FavouriteBooks_.Models;
using System.IO; 

namespace TaskFour_FavouriteBooks_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db; 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db; 
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<BookModel> list = _db.Books.ToList(); 
            return View(list);
        }

        public IActionResult GetImage(int Id)
        {
            var img = _db.Books.Find(Id).Image;
            //var imgFile = _db.Books.Find(Id).ImageFile;

            //if (!imgFile.ContentType.Equals("images/jpeg"))
            //{
            //    ModelState.AddModelError("ImageType", "The image format does not match");
            //}
            return File(img, "images/jpeg");
        }

        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName); 
                using(var stream = System.IO.File.Create(filePath))
                {
                    model.ImageFile.CopyTo(stream);
                }
                model.FilePath = filePath; 
                using(var memoryStream = new MemoryStream())
                {
                    model.ImageFile.CopyTo(memoryStream);
                    model.Image = memoryStream.ToArray(); 
                }

                _db.Books.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == 0 || Id == null)
            {
                return NotFound(); 
            }
            var book = _db.Books.Find(Id); 
            if(book == null)
            {
                return NotFound(); 
            }
            return View(book); 
        }
        [HttpPost]
        public IActionResult Edit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    model.ImageFile.CopyTo(stream);
                }
                model.FilePath = filePath;
                using (var memoryStream = new MemoryStream())
                {
                    model.ImageFile.CopyTo(memoryStream);
                    model.Image = memoryStream.ToArray();
                }

                _db.Books.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Find(Id); 
            if(book == null)
            {
                return NotFound(); 
            }
            return View(book); 
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            var book = _db.Books.Find(Id);
            if(book == null)
            {
                return NotFound(); 
            }
            if (ModelState.IsValid)
            {
                _db.Books.Remove(book);
                _db.SaveChanges(); 
                return RedirectToAction("Index");   
            }
            return View(); 
        }
    }
}