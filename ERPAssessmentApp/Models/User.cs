using System.ComponentModel.DataAnnotations;

namespace ERPAssessmentApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}