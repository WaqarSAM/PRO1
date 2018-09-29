using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRO1.Models;

namespace PRO1.Controllers
{
    public class studentController : Controller
    {
        StudentRegContext _ORM = null;
        IHostingEnvironment _ENV = null;
        public studentController(StudentRegContext ORM, IHostingEnvironment ENV)
        {
            _ORM = ORM;
            _ENV = ENV;
          
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student S, IFormFile Cv)
        
            {
            string WwwRoot = _ENV.WebRootPath;
            string FTPpath = WwwRoot + "/WebData/Cv";
            string UniqueName = Guid.NewGuid().ToString();
            string FileExtension = Path.GetExtension(Cv.FileName);
            FileStream Fs = new FileStream(FTPpath + UniqueName + FileExtension, FileMode.Create);
            Cv.CopyTo(Fs);
            Fs.Close();
            S.Cv = "/WebData/Cv" + UniqueName + FileExtension;

            _ORM.Add(S);
            _ORM.SaveChanges();
           
            ModelState.Clear();
            MailMessage oEmail = new MailMessage();
            oEmail.From = new MailAddress("waqar1235@gmail.com");
            oEmail.To.Add(new MailAddress(S.Email));
            //oEmail.CC.Add(new MailAddress("XXXX@XXXX.com"));
            oEmail.Subject = "Thanks for Regisration";
            oEmail.Body = "Dear " + S.Name + ",<br><br>" +
                "Thanks for registering with ABC, We are glad to have you in our system." +
                "<br><br>" +
                "<b>Regards</b>,<br>ABC Team";
            oEmail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(S.Cv))
            {
                oEmail.Attachments.Add(new Attachment(WwwRoot + S.Cv));
            }


            SmtpClient oSMTP = new SmtpClient();
            oSMTP.Host = "smtp.gmail.com";
            oSMTP.Port = 465;
            oSMTP.EnableSsl = true;
            oSMTP.Credentials = new System.Net.NetworkCredential("wa625775@gmail.com", "programer");
            try
            {
                oSMTP.Send(oEmail);
            }
            catch (Exception ex)
            {

            }
            //
            ViewBag.Message = "Student Record has been added";
            return View();
        }
        //ViewBag.message = "Record Added Successfully"; 
        //                return View()

        [HttpGet]
        public IActionResult ViewList()
        {
            IList<Student> list = _ORM.Student.ToList<Student>();
            return View(list);
        }
        [HttpPost]
        public IActionResult ViewList(string name, string sclass, string department)
        {
            IList<Student> list = _ORM.Student.Where(m => m.Name.Contains(name) || m.Class.Contains(sclass) || m.Department.Contains(department)).ToList<Student>();
            return View(list);
        }
            public IActionResult StudentDetail(int Id)
        {
            Student S = _ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return View(S);
        }
        
        public string  deleteStudent (Student S)
        {
            string result = "";
            try
            {
                _ORM.Student.Remove(S);
                _ORM.SaveChanges();
                result = "Yes";
            }
            catch (Exception e)
            {
                result = "No";
            }
            return result;
        }
        [HttpGet]
        public IActionResult EditStudent(int Id)
        {

            Student S = _ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return View(S);
        }
        [HttpPost]
        public IActionResult EditStudent(Student S)
        {
           _ORM.Student.Update(S);
           _ORM.SaveChanges();
            //Student S = ORM.Student.Where(m => m.Id == Id).FirstOrDefault<Student>();
            return RedirectToAction("viewlist");
        }






















        public IActionResult Index()
        {
            return View();
        }
    }
}