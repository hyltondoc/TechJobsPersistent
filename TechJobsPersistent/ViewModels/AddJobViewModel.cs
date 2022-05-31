using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int EmployerId { get; set; }

       public List<SelectListItem> AllEmployers { get; set; }
       public List<Skill> Skills { get; set; }


        public AddJobViewModel(List<Employer> allEmployers, List<Skill> allSkills)
        {
            AllEmployers = new List<SelectListItem>();        

            foreach (var employer in allEmployers)
            {
                AllEmployers.Add(new SelectListItem

                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
         
            Skills = allSkills;
        }

        public AddJobViewModel() 
        { 
        }

    }



}
