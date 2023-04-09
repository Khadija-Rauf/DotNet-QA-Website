using Microsoft.AspNetCore.Mvc;
using Semester_Project.Models.Interfaces;

namespace Semester_Project.Models
{
    public class QuestionRepository:IQuestionRepository
    {
        ProjectContext context = new ProjectContext();
        public void Add(Question question)
        {
            context.Questions.Add(question);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public List<Question> Reterive()
        {
            return context.Questions.ToList();
        }
        public List<Question> Select(string Name)
        {
            List<Question> data = new List<Question>();
            //LINQ query
            var Questions = from m in context.Questions
                            select m;
            foreach (var question in Questions)
            {
                if (Name.Equals(question.User))
                {
                    data.Add(question);
                }
            }
            return data;
        }
        public Question FindQuestion(int id)
        {
            Question q = context.Questions.SingleOrDefault(q => q.Id == id);
            return q;
        }
        public void UpdateQuestion(Question q)
        {
            foreach (Question question in context.Questions)
            {
                if (question.Id == q.Id)
                {
                    question.Title = q.Title;
                    question.Description = q.Description;
                    question.Tag = q.Tag;
                    break;
                }
            }
            Save();
        }
        public void RemoveQuestion(int id)
        {
            Question q = FindQuestion(id);
            context.Questions.Remove(q);
            Save();
        }
        public string GetUser(int id)
        {
            Question q = FindQuestion(id);
            return q.User;
        }
        public void AnswerQuestion(Question q)
        {
            foreach (Question question in context.Questions)
            {
                if (question.Id == q.Id)
                {
                    question.Title = question.Title;
                    question.Description = q.Description;
                    question.Tag = question.Tag;
                    break;
                }
            }
            Save();
        }
    }
}
