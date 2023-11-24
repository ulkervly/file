using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokPractice.DAL;
using PustokPractice.Models;

namespace PustokPractice.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Service> Services = _context.Services.ToList();
            return View(Services);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {

            if (!ModelState.IsValid) return View();

            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.id == id);
            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {
            Service existService1 = _context.Services.FirstOrDefault(x => x.id == service.id);
            if (existService1 == null)
            {
                return NotFound();
            }
            _context.Services.Remove(existService1);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.id == id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            
            Service existService1 = _context.Services.FirstOrDefault(x => x.id == service.id);
            if (existService1 == null)
            {
                return NotFound();
            }
            existService1.Title = service.Title;
            existService1.Description = service.Description;
            existService1.Icon = service.Icon;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
