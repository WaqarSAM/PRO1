using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRO1.Models;

namespace PRO1.Controllers
{
    public class studentController : Controller
    {
        private StudentRegContext ORM = null;
        public studentController (StudentRegContext ORM)
        {
            this.ORM = ORM;
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            ORM.Add(student);

            ORM.SaveChanges();
            ViewBag.Message = "Registration Done Successfully";
            ModelState.Clear();
            return View();
        }
        public IActionResult StudentDetail(int Id)
        {
            Student S = ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return View(S);
        }



        [HttpGet]
        public IActionResult AllStudents()
        {
            IList<Student> AllStudents = ORM.Student.ToList<Student>();
            return View(AllStudents);
        }

        [HttpPost]
        public IActionResult AllStudents(string SearchByName, string SearchByClass)
        {

            IList<Student> AllStudents = ORM.Student.Where(m => m.Name.Contains(SearchByName) || m.Class.Contains(SearchByName)).ToList<Student>();
            return View(AllStudents);
        }
        public IActionResult DeleteStudent(Student S)
        {
            // Student S =  ORM.Student.Where(a => a.Id == Id).FirstOrDefault<Student>();
            ORM.Student.Remove(S);
            ORM.SaveChanges();
            //return View("AllStudents");
            return RedirectToAction("AllStudents");
        }
        [HttpGet]
        public IActionResult EditStudent(int Id)
        {

            Student S = ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return View(S);
        }
        [HttpPost]
        public IActionResult EditStudent(Student S)
        {
            ORM.Student.Update(S);
            ORM.SaveChanges();
            //Student S = ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return RedirectToAction("AllStudents");
        }






















        public IActionResult Index()
        {
            return View();
        }
    }
}