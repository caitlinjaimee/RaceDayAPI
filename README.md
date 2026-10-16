
RaceDayAPI

Project Description

RaceDayAPI is a web-based event management API for running, walking and cycling events. It allows organisers to manage events and participants to register for events.

The system uses ASP.NET Core Web API, C#, Entity Framework Core and SQL Server.

 User Roles

1. Organiser

Organisers can create and manage events, manage event categories and record event results. They are responsible for keeping event information up to date.

2. Participant

Participants can view available events, enter events and view their results and performance information.

Setup and Installation

1. Open the RaceDayAPI project in Visual Studio.
2. Make sure SQL Server is running.
3. Create the `RaceDayDB` database and its required tables using the provided SQL script.
4. Open `appsettings.json` and check that the connection string points to the correct SQL Server instance and database.
5. Restore the NuGet packages if needed.
6. Build the project in Visual Studio.
 How to Run

1. Open the project in Visual Studio.
2. Select the HTTPS launch profile.
3. Click the Run button.
4. Open the Swagger page in your browser to view and test the API endpoints.

 Main Code Structure

- Controllers:Handle API requests and communicate with the database.
- Models: Represent the database tables, such as Users, Events, Categories and Enrolments.
- Data: Contains `RaceDayContext`, which connects the application to SQL Server.
- DTOs: Contain the data used for registration and login requests.
- Program.cs: Configures the API, database connection and authentication.
- appsettings.json: Stores application settings, including the database connection string.

 Main API Endpoints

- `/api/Auth/register` – Register a new user.
- `/api/Auth/login` – Log in.
- `/api/Auth/logout` – Log out.
- `/api/Users` – Manage user information.
- `/api/Events` – Manage and view events.
- `/api/Categories` – Manage event categories.
- `/api/Enrolments` – Manage event enrolments.
- `/api/Results` – Manage event results.
- `/api/Weather` – Access event weather information.
- `/api/Routes` – Access event route information.

The available HTTP methods and access permissions depend on the controller.

 Authentication

The API uses cookie-based authentication. Users log in with their email address and password. The application uses their role to control access to protected endpoints.

Database

The application uses SQL Server with a database named `RaceDayDB`. The database stores user details, events, categories, enrolments, results, weather information and routes.

CI

GitHub Actions is used to build the project automatically when changes are pushed to GitHub. A successful build is shown by a green tick in the Actions tab.

