namespace Semester_Project.Models.Interfaces
{
    public interface IUserRepository
    {
        public void Add(User u);
        public void Save();
        public int[] authenticateUser(User user);
        public User show(string Name);
    }
}
