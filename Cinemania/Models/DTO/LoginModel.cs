using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations

namespace Cinemania.Models.DTO
{
    public class LoginModel // Defining the LoginModel class
    {
        [Required] // Data annotation specifying that the Username property is required
        public string? Username { get; set; } // Property representing the username entered during login

        [Required] // Data annotation specifying that the Password property is required
        public string? Password { get; set; } // Property representing the password entered during login
    }
}
