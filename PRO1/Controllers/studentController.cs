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
        public IActionResult Index()
        {
            return View();
        }
    }
}