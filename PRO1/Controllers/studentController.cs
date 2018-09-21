using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PRO1.Controllers
{
    public class studentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}