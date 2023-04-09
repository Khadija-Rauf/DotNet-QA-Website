using Microsoft.EntityFrameworkCore;

namespace Semester_Project.Models
{
    public class ProjectContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ASKME;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
