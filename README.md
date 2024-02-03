<div align="center">
  <h3 align="center">Hotel Management System</h3>

  <p align="center">
    A comprehensive hotel management system built with ASP MVC .NET 7
    <br />
    <a href="#">Explore the documentation »</a>
    <br />
    <br />
    <a href="#">View Demo</a>
    ·
    <a href="#">Report Bug</a>
    ·
    <a href="#">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#project-structure">Project Structure</a></li>
    <li><a href="#technologies-used">Technologies Used</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Describe your hotel management system project, its features, and its purpose.

### Built With

* [ASP MVC .NET 7](https://dotnet.microsoft.com/apps/aspnet)

<!-- GETTING STARTED -->
## Getting Started

This section will guide users on how to set up and run your hotel management system locally.

### Prerequisites
* Visual Studio
* PostreSql

### Packages
* Microsoft.AspNetCore.Identity.EntityFramework.core 7.0.0
* Microsoft.EntityFrameworkCore 7.0.0
* Microsoft.EntityFrameworkCore.Design 7.0.0
* Microsoft.VisualStudio.Web.CodeGeneration.Design 7.0.0
* Npgsql.EntityFrameworkCore.PostgreSQL 7.0.0
* Npgsql.NodaTime 7.0.0
* Swashbuckle.AspNetCore 6.5.0
#### This packages are already installed with downloading the project.

### Installation

* Download the project
* Open it with Visual Studio
* Change the Connection in appsetting.json based on your Username and Password
* Open terminal
* dotnet
  ```sh
  dotnet ef database update
  ```
  #### This installation means the database is empty so there will be no Users and Rooms.
  
<!-- USAGE -->
## Usage

Welcome to the Hotel Management System! This section will guide you through the available functionalities and how to interact with the system.

### Admin Panel

#### Creating a New Room
1. In the Navigation bar, click on the "Add room" option.
2. Fill in the required details such as room number, type, Description, Capacity, etc. 
3. Click the "Create" button to create a new room.

#### Editing/Updating a Room
1. In the Navigation bar, click on the "All Rooms" option to view the existing rooms.
2. Locate the room you want to edit and click on the "Edit" option.
3. Update the necessary information and click "Update" to apply the changes.

#### Deleting a Room
1. In the Navigation bar, click on the "All Rooms" option to view the existing rooms.
2. Find the room you wish to delete and click on the "Delete" option.

#### View All Reservations
1. In the Navigation bar, click on the "View All Reservations" option.
2. It will Display all reservations made by the users.

#### Users
1. In the Navigation bar, click on the "User List" option.
2. It will Display all Users that have made an Account.
* Find the User you wish to delete and click on the "Delete" option.
* Find the User you wish to change role and click on the "Make User / Make Admin" option.

### User Panel

#### Viewing Rooms
1. In the Navigation bar, click on the "Rooms" option.
2. It will show all available rooms.

#### Viewing Details
1. Follow the previous steps.
2. Locate the room you want to view and click on the "Learn more" option.

#### Booking a room
1. Follow the previous steps.
2. Click on the "Reserve Room" option.
3. Fill in the required details such as Name, Email, Number, Arrival and Depaarture date, Billing Infromation, Special Request.
4. Check the box to agree to the Terms and Conditions.
* Clicking on the Terms and Conditions will pop up a box displaying all the Terms and Conditions
5. Click the "Reserve Room" button to reserve the room and all infromation will be saved.

## Additional Notes

- Ensure proper authentication and authorization when accessing the Admin Panel.
- For security reasons, regular users are not granted access to administrative functionalities.

Feel free to explore and use the various features available in the Hotel Management System. If you encounter any issues, please don't hesitate to report the bug.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- PROJECT STRUCTURE -->
## Project Structure

The Hotel Management System project follows a modular structure to organize its components effectively. Here's an overview of the main directories and their purposes:

### Controllers

- **AccountController.cs**: Handles user authentication and registration.
- **AdminRoomController.cs**: Manages administrative functions related to rooms, such as creation, editing, and deletion.
- **RoomController.cs**: Manages user-related room functionalities, including displaying all rooms, details, bookings, and room reservations.

### Models

- **Reservations**:
  - **BillingInfo.cs**: Contains billing information for reservations.
  - **Guest.cs**: Represents guest details for reservations.
  - **Reservation.cs**: Captures reservation details, linking guests and billing information.

### ViewModels

- **AddRoomRequest.cs**: Represents the model for adding a new room.
- **EditRoomRequest.cs**: Represents the model for editing room details.
- **LoginViewModel.cs**: Handles the data for user login.
- **RegisterViewModel.cs**: Manages the data for user registration.
- **UsersViewModel.cs**: Represents user-related data.

### Other Models

- **ApplicationUser.cs**: Represents the application's users.
- **ErrorViewModel.cs**: Manages error-related information.
- **Room.cs**: Represents room details.
- **User.cs**: Represents user details.

### Repository

- **Implementations**
  - **Repository.cs**: Contains the implementation of data repository operations.

- **Interfaces**
  - **IRepository.cs**: Defines the interface for data repository operations.

### Services

- **Implementations**
  - **RoomService.cs**: Implements room-related services.

- **Interfaces**
  - **IRoomService.cs**: Defines the interface for room-related services.

### Views

- **Account**
  - Login.cshtml, Register.cshtml, ViewUsers.cshtml

- **Dashboard**
  - AllReservations.cshtml

- **AdminRoom**
  - AllRooms.cshtml, Create.cshtml, Update.cshtml

- **Home**
  - Index.cshtml, Privacy.cshtml

- **Shared**
  - Layout.cshtml

### Configuration

- **AppSettings.json**: Configuration file for application settings.
- **Program.cs**: Entry point for the application.

Feel free to explore each directory for more details on specific functionalities and implementations.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.
