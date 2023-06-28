using System.ComponentModel.DataAnnotations;

namespace DailySalesSummary.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        // Additional fields like Email, FirstName, LastName, etc.
        public MindbodySettings? Mindbody { get; set; }
    }
}
