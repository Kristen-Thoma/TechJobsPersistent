using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext _context;

        public EmployerController(JobDbContext dbContext)
        {
            _context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = _context.Employers.ToList();

            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel employer = new AddEmployerViewModel();

            return View(employer);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(Employer employer)
        {
            if (ModelState.IsValid)
            {
                _context.Employers.Add(employer);
                _context.SaveChanges();
                return Redirect("/Employer/");
            }

            return View("Add", employer);
        }

        public IActionResult About(int id)
        {
            List<Employer> employers = _context.Employers
                .Where(et => et.Id == id)
                .Include(et => et.Name)
                .Include(et => et.Location)
                .ToList();

            return View(employers);
        }
    }
}
