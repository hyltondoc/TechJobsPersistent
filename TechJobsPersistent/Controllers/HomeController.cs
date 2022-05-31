using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
          
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
          // List<Employer> employers = context.Employers.ToList();
          //List<Skill> skills = context.Skills.ToList();


          //ddJobViewModel jobViewModel = new AddJobViewModel(employers, skills);//creates an instance of the view model
            AddJobViewModel jobViewModel = new AddJobViewModel(context.Employers.ToList(), context.Skills.ToList());

            return View(jobViewModel);

            
            //return View();
        }
        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel jobViewModel, string[] selectedSkills)
        {

            Job newjob = new Job
            {
                Name = jobViewModel.Name,
                EmployerId = jobViewModel.EmployerId,
                

            };


            foreach (string skillId in selectedSkills)
            {
                JobSkill jobSkills = new JobSkill
                {
                    JobId = newjob.Id,
                    SkillId = int.Parse(skillId)
                };
                
             
                context.JobSkills.Add(jobSkills);
          
            }
           
            context.Jobs.Add(newjob);
            context.SaveChanges();
            return Redirect("Index");

            //return View(jobViewModel);
        }

   

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
