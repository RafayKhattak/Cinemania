# Cinemania

Cinemania is a web application for managing movies, genres, and user authentication. It allows users to browse a collection of movies information, add new genres, and manage user accounts.

![Capture](https://github.com/RafayKhattak/Cinemania/assets/90026724/99a3dd55-ae8b-4fa7-8db5-b51a52cdf3d6)


## Features

- **User Authentication:** Secure user registration and login functionality.
- **Movie Management:** Add, edit, and delete movies.
- **Genre Management:** Add new genres for categorizing movies.
- **Search Functionality:** Users can search for movies by title.
- **Pagination:** Provides pagination for browsing through large collections of movies.

## Technologies Used

- **ASP.NET Core:** Backend framework for building web applications.
- **Entity Framework Core:** Object-relational mapping (ORM) framework for database interaction.
- **Microsoft Identity:** Framework for managing user authentication and authorization.
- **HTML/CSS/JavaScript:** Frontend development technologies for creating the user interface.
- **Bootstrap:** Frontend framework for responsive and mobile-first web development.
- **Repository Pattern:** Utilized for data access abstraction and separation of concerns.

## Getting Started

To run this project locally, follow these steps:

1. Clone this repository to your local machine.
2. Install the necessary dependencies using `dotnet restore`.
3. Update the connection string in `appsettings.json` to point to your database.
4. Apply any pending migrations using `dotnet ef database update`.
5. Run the application using `dotnet run`.
6. Access the application in your web browser at `http://localhost:5000`.
