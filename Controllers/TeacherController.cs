using Microsoft.AspNetCore.Mvc;
using Semester_Project.Models;
using Semester_Project.Models.Interfaces;

namespace Semester_Project.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IQuestionRepository questionRepository;
        public TeacherController() {
             questionRepository = new QuestionRepository();

        }
        public IActionResult HomeTeacher()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                List<Question> questions = questionRepository.Reterive();
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
        public IActionResult ViewQuestions()
        {
            List<Question> questions = questionRepository.Reterive();
            if (questions == null)
            {
                return View("No Record Found!");
            }
            else
            {
                return View(questions);
            }
        }
        public ViewResult Answer(int id)
        {
            Question q = questionRepository.FindQuestion(id);
            return View(q);
        }
        [HttpPost]
        public ViewResult Answer(Question q)
        {
            questionRepository.AnswerQuestion(q);
            return View();
        }
        public IActionResult GetSearchingData(string SearchBy, string SearchValue)
        {
            ProjectContext context = new ProjectContext();
            List<Question> questions = questionRepository.Reterive();
            if (SearchBy == "Tag")
            {
                try
                {
                    questions = context.Questions.Where(x => x.Tag.Equals(SearchValue) || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} Is Not A TAG ", SearchValue);
                }
                Console.WriteLine(SearchValue);
                return Json(questions);
            }
            else
            {
                questions = context.Questions.Where(x => x.User.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(questions);

            }
        }
    }
}
