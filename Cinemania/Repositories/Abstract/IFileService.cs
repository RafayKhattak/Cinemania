using Microsoft.AspNetCore.Http; // Importing the namespace for ASP.NET Core HTTP features

namespace Cinemania.Repositories.Abstract
{
    public interface IFileService // Defining the IFileService interface
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile); // Method signature for saving an image file, returning a tuple containing a status code and the saved image file name
        public bool DeleteImage(string imageFileName); // Method signature for deleting an image file, accepting the file name as input
    }
}
