using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semester_Project.Models
{
    public class User
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }
        [Key]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string? PhotoPath { get; set; }
        [Display(Name = "Upload Profile Photo")]
        [NotMapped]
        public IFormFile? ProfilePhoto { get; set; } 
    }
}
