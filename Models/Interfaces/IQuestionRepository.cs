namespace Semester_Project.Models.Interfaces
{
    public interface IQuestionRepository
    {
        public void Add(Question question);
        public void Save();
        public List<Question> Reterive();
        public List<Question> Select(string Name);
        public Question FindQuestion(int id);
        public void UpdateQuestion(Question q);
        public void RemoveQuestion(int id);
        public string GetUser(int id);
        public void AnswerQuestion(Question q);
    }
}
