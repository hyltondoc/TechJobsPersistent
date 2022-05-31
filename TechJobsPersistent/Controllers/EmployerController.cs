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
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel employerViewModel = new AddEmployerViewModel();
            return View(employerViewModel);
        }

        [HttpPost]
        //[Route("/")]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel employerViewModel)
        {
            if (ModelState.IsValid)
            {
              
                string employerName = employerViewModel.Name;
                string employerLocation = employerViewModel.Location;

                List<Employer> existingEmployers = context.Employers
                    .Where(employer => employer.Name == employerName)
                    .Where(employer => employer.Location == employerLocation)
                    .ToList();

                if (existingEmployers.Count == 0)
                {
                    Employer employer = new Employer
                    {
                        Name = employerName,
                        Location = employerLocation
                    };
                  context.Employers.Add(employer);
                  context.SaveChanges();

                }

                return Redirect("Index");
                
            }
            return Redirect("Index");
        
        }

        public IActionResult About(int id)
        {
            List<Employer> employers = context.Employers
                .Where(employer => employer.Id == id)
                .ToList();
          //Employer theEmployer = context.Employers.Find(id);
          //  context.SaveChanges();
            return View(employers);
            //return View();
        }
    }
}
