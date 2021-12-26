using school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace school.dtos
{
    public class AbsentCelander
    {      
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int NumOfDay { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public bool?[,] absentMat;
        public void SetNumOfDay()
        {
           
           startDate = new DateTime(year, month, 1);
                       
            // var month = startDate.Month;
            endDate = startDate.AddMonths(1).AddDays(-1);
            NumOfDay = 1 + endDate.Day - startDate.Day;
        }

        public void  MatrixSet(IEnumerable<Student> students, SchoolContext context)
        {
            int count = students.Count();
            int i = 0;
            absentMat = new bool?[count, NumOfDay];
            Students = new string[count];
            foreach (Student student in students)
            {
                Students[i] = student.StudentName;
                foreach (var absent in context.Absent.Where(a=>a.StudentId == student.StudentId && a.DateAbsent.Month == month && a.DateAbsent.Year == year)) { 
                            int j = absent.DateAbsent.Day - 1;
                            absentMat[ i, j] = absent.AbsentCheck;
                }
                i++;
            }
        }
        public string[] Students { get; set;}
    }
}
