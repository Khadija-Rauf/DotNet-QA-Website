using System.ComponentModel.DataAnnotations.Schema;

namespace Semester_Project.Models.Interfaces
{
    public interface IIdentityModel
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        }
}
