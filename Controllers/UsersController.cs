using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Semester_Project.Models;
using Semester_Project.Models.Interfaces;

namespace Semester_Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository repository;
        ProjectContext context = new ProjectContext();
        private readonly IWebHostEnvironment Environment;
        public UsersController(IWebHostEnvironment
            environment)
        {

            Environment = environment;
            repository = new UserRepository();
        }
        public ViewResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ViewResult SignUp(User u)
        { 
            if (ModelState.IsValid)
            {
                var isNameAlreadyExists = context.Users.Any(x => x.Name == u.Name);
                if (isNameAlreadyExists)
                {
                    ViewBag.Message = "User with this username already exists!";
                    return View();
                }
                FileUpload(u.ProfilePhoto, u.Name, u.PhotoPath);
                ViewBag.Message = "Your account has been created!";
                repository.Add(u);
                repository.Save();
                return View("login");
            }
            return View(u);
        }
        public ViewResult login()
        {
            return View();
        }
        [HttpPost]
         public IActionResult login(User user)
        {
            if (ModelState.IsValid && user != null)
            {
                var res = repository.authenticateUser(user);
                if (res[1] == 1) 
                {
                    string name = user.Name;
                    ViewBag.Name = name;
                    if (res[0] == 0)
                    {
                        string data = String.Empty;
                        if (HttpContext.Session.Keys.Contains("UserName"))
                        {
                            data = $"Welcome back {name}";
                            HttpContext.Session.SetString("UserName", data);

                        }
                        else
                        {
                            HttpContext.Session.SetString("UserName", name);
                        }
                        return RedirectToAction("HomeStudent","Student");

                    }
                    else
                    {
                        string data = String.Empty;
                        if (HttpContext.Session.Keys.Contains("UserName"))
                        {
                            data = $"Welcome back {name}";
                            HttpContext.Session.SetString("UserName", data);

                        }
                        else
                        {
                            HttpContext.Session.SetString("UserName", name);
                        }
                        return RedirectToAction("HomeTeacher", "Teacher");

                    }
                }
                else
                {
                    ViewBag.Message = "Incorrect Username or Password!";
                    return View("login");
                }
            }
            return View();
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                UserRepository repository = new UserRepository();
                User user = repository.show(HttpContext.Session.GetString("UserName"));
                return View(user);
            }
            else
            {
                return RedirectToAction("login","Users");
            }
        }
        public ViewResult logout()
        {
            HttpContext.Session.Remove("UserName");
            return View();
        }
        public IActionResult FileUpload(IFormFile? file, string? Name, string Photopath)
        {
            string path = " ";
            if (file.Length > 0)
            {
                string wwwPath = this.Environment.WebRootPath;
                string userdir = "Profiles/" + Name;
                path = Path.Combine(wwwPath, userdir);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var stream = new FileStream(Path.Combine(path, "Name.png"), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Photopath = path;
                return Json(true);
            }
            else
                return Json(false);
        }
    }
}
