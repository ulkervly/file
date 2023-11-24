using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokPractice.DAL;
using PustokPractice.Models;

namespace PustokPractice.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context= context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> Sliders = _context.Sliders.ToList();
            return View(Sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image can be only .jpeg or .png");
                    return View();
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be lower than 2mb");
                    return View();
                }

                string fileName = slider.ImageFile.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
                string newFileName = Guid.NewGuid().ToString() + fileName;

                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", newFileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.ImageFile.CopyTo(stream);
                }
                slider.Image = newFileName;
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.id == id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.id == slider.id);
            if (existSlider == null) return NotFound();
            string newFileName = null;

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image can be only .jpeg or .png");
                    return View();
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be lower than 2mb");
                    return View();
                }
                string fileName = slider.ImageFile.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
                newFileName = Guid.NewGuid().ToString() + fileName;

                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", newFileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.ImageFile.CopyTo(stream);
                }
            }
            if (newFileName != null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "uploads/sliders", existSlider.Image);

                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

                existSlider.Image = newFileName;
            }




            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            existSlider.ButtonText = slider.ButtonText;
            existSlider.RedirectUrl = slider.RedirectUrl;
         

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.id == id);
            if (slider == null) return Json(new { status = 404 });

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
