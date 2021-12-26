using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Authorize(Roles="Admin")]
    public class StudentController : Controller
    {
        private readonly SchoolContext context;
        public StudentController(SchoolContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext.Session.SetInt32("Class", id.Value);
            Class cls = await context.Class.SingleOrDefaultAsync(c => c.Id == id);
            if(cls != null)
            {
                HttpContext.Session.SetString("ClassString", cls.Name);
            }
            ViewData["ListClasses"] = context.Class.Where(c => c.Stage == cls.Stage && c.Id != id.Value);
            ViewData["currentClss"] = id.Value;
            return View(await context.Student.Where(s=>s.ClassFk == id).ToListAsync());
        }
        public async Task<IActionResult> oprtStudent()
        {
           
            Class cls = await context.Class.SingleOrDefaultAsync(c => c.Id == HttpContext.Session.GetInt32("Class").Value);
            if (cls != null)
            {
                HttpContext.Session.SetString("ClassString", cls.Name);
            }
            ViewData["ListClasses"] = context.Class.Where(c => c.Stage == cls.Stage && c.Id != cls.Id);
            ViewData["currentClss"] =cls.Id;
            return View(await context.Student.Where(s => s.ClassFk == cls.Id).ToListAsync());
        }
    }
}