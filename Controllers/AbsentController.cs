using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.dtos;
using school.Models;

namespace school.Controllers
{
    public class AbsentController : Controller
    {
        private readonly SchoolContext context;
        public AbsentController(SchoolContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index() {
            ViewData["month"] = await context.Absent.Where(a => a.DateAbsent.Year == DateTime.Now.Year).Select(a => a.DateAbsent.Month).Distinct().ToListAsync();
            ViewData["year"] = await context.Absent.Select(a => a.DateAbsent.Year).Distinct().ToListAsync();
            return View();
        }

        public async Task<IActionResult> Table(int month, int year)
        {
            
            int? cls = HttpContext.Session.GetInt32("Class");
            ViewData["Month"] = month;
            var students = await context.Student.Where(c => c.ClassFk == cls).ToListAsync();
            AbsentCelander absentCelander = new AbsentCelander()
            {
                month = month,
                year = year
            };

            absentCelander.SetNumOfDay();
            absentCelander.MatrixSet(students, context);
            return View(absentCelander);
        }

        public async Task<IActionResult> TimeAbsentTable(int? id)
        {
            var list = await context.Student.Where(s => s.ClassFk == id).ToListAsync();
            return View(list);
        }

    }
}