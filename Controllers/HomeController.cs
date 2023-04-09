using Microsoft.AspNetCore.Mvc;
using Semester_Project.Models;
using Semester_Project.Models.Interfaces;
using System.Diagnostics;

namespace Semester_Project.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public ViewResult Index()
        {
            return View();
        }
    }
}