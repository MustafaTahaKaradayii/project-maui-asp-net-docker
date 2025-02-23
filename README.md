
# SimpleNotes Project: MAUI + ASP.NET + Docker

This project demonstrates a **MAUI app** communicating with an **ASP.NET Core Web API** and storing data in a **MySQL database** using Docker.

---

## Project Structure

1. **SimpleNotesApi** (Backend - ASP.NET Core)
   - API to manage notes (CRUD operations).
   - Connects to a MySQL database.
   - Runs inside a Docker container.

2. **SimpleNotesApp** (Frontend - .NET MAUI)
   - A cross-platform MAUI application that communicates with the API.
   - Implements MVVM (Model-View-ViewModel) design pattern for clean architecture.

---

## Prerequisites

Ensure you have the following installed:

- **Docker**: To run containers.
- **.NET SDK 8.0**: For building the MAUI and API projects.
- **Visual Studio 2022** with .NET MAUI workload installed.

---

## Steps to Run the Project

### **1. Clone the Repository**

Open a terminal and clone the project repository:

bash
```
git clone https://github.com/thomasmore-dn/project-maui-asp-net-docker-MustafaTahaKaradayii.git
cd project-maui-asp-net-docker
```

2. Run the Dockerized API and MySQL
Navigate to the SimpleNotesApi folder:

bash
Copy code
```
cd SimpleNotesApi
docker-compose up --build
```

Open a browser or tool like Postman and test the endpoint:

bash
Copy and paste on browser
GET http://localhost:8080/api/notes

Expected output:

json
Copy code
[
   { "id": 1, "title": "Test Note", "content": "Test Content" }
]
MySQL Database: Exposed on port 3307.
API: Exposed on port 8080.

3. Run the MAUI Application
Open the SimpleNotesApp solution:

Launch Visual Studio 2022.
Open the solution file SimpleNotesApp.sln.
Verify the API URL in ApiService.cs:

csharp
Copy code
private const string BaseUrl = "http://localhost:8080/api/notes";
Run the MAUI application:

Select your target platform:
Windows, Mac, or Android Emulator.
Click the Run button (green play button) in Visual Studio.
4. Verify Functionality
Open the MAUI application and test the following actions:

Add a Note.
Edit a Note.
Delete a Note.
Verify the changes in the database:

Connect to MySQL in the running container:

bash
Copy code
docker exec -it simplenotesapi-mysql-1 mysql -u root -p
Password: 7227 (set in docker-compose.yml).
Run SQL queries to check the database:

sql
Copy code
USE simplenotesdb;
SELECT * FROM Notes;
Key Features
->Backend: ASP.NET Core Web API with CRUD operations.
->Frontend: MAUI app with MVVM for clean data binding.
->Database: MySQL database managed via Docker.
->Docker: Containers for API and MySQL with docker-compose.
->MVVM Design Pattern Overview
->Models: Defines data structure (Note.cs in SimpleNotesApi and SimpleNotesApp).
->ViewModels: Manages data and business logic (NotesViewModel.cs).
->Views: UI components (MainPage.xaml, DetailPage.xaml).
Key Code Locations:
Models:
->SimpleNotesApi/Models/Note.cs
->SimpleNotesApp/Models/Note.cs
->ViewModel:
->SimpleNotesApp/ViewModels/NotesViewModel.cs
Views:
->SimpleNotesApp/MainPage.xaml
->SimpleNotesApp/DetailPage.xaml

Troubleshooting
If the API Doesn’t Start:
Ensure Docker is running.
Check for port conflicts (8080 for API, 3307 for MySQL).
If the MAUI App Doesn’t Fetch Data:
Verify the API is running at http://localhost:8080/api/notes.
Test the API using a browser or Postman.
Author
Mustafa Taha Karadayi

