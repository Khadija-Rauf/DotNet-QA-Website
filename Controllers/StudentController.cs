using Microsoft.AspNetCore.Mvc;
using Semester_Project.Models;
using Semester_Project.Models.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Semester_Project.Controllers
{
    public class StudentController : Controller
    {
        private readonly IQuestionRepository questionRepository;
        public StudentController()
        {
            questionRepository = new QuestionRepository();
        }
        public IActionResult HomeStudent()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                List<Question> questions = questionRepository.Select(HttpContext.Session.GetString("UserName"));
                if (questions == null)
                {
                    return View("No Record Found!");
                }
                else
                {
                    return View(questions);
                }
            }
            else
            {
                return RedirectToAction("login", "Users");
            }
        }
        public IActionResult Ask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ask(Question q)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = " Your Question has been added!";
                questionRepository.Add(q);
                questionRepository.Save();
                return View();
            }
            return View();
        }
        public ViewResult Edit(int id)
        {
            Question q = questionRepository.FindQuestion(id);
            return View(q);
        }
        [HttpPost]
        public ViewResult Edit(Question q)
        {
            questionRepository.UpdateQuestion(q);
            return View();
        }
        public IActionResult Delete(int id)
        {
            string name = questionRepository.GetUser(id);
            questionRepository.RemoveQuestion(id);
            return RedirectToAction("HomeStudent");
        }
    }
}
