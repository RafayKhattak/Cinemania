using Microsoft.AspNetCore.Identity; // Importing the namespace for ASP.NET Core Identity

namespace Cinemania.Models.Domain
{
    public class ApplicationUser : IdentityUser // Defining the ApplicationUser class which inherits from IdentityUser
    {
        public string Name { get; set; } // Adding a custom property for the user's name
    }
}
