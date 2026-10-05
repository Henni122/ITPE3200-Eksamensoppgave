# Study Room Booking : Basic Project – ITPE3200 Web Applications, 2026H

## About the Project

Study Room Booking is an ASP.NET Core MVC web application developed as an MVP (Minimum Viable Product). The current implementation focuses on managing study rooms through a database-backed web interface.

Users can view rooms, create new rooms, update existing rooms and delete rooms. The database structure also includes models for room bookings, users and booking participants, which support further development of the booking functionality.

## Technologies Used

- .NET 10.0
- ASP.NET Core MVC
- C#
- Entity Framework Core 10.0.12
- SQLite
- Razor Views
- Bootstrap 5.3.3
- jQuery 3.7.1
- ASP.NET Core Identity
- Serilog
- Library Manager (LibMan)
- Node.js

Node.js version: Not applicable. Node.js is not required to build or run this application.

The frontend is implemented using Razor Views and Bootstrap. Bootstrap's JavaScript bundle and jQuery are included through LibMan, but the current application does not rely on custom JavaScript for its main functionality.

## Main Features

### Rooms

Users can:

- View rooms in a table
- Add new rooms
- Update existing rooms
- Delete rooms

Each room contains:

- Room name
- Capacity
- Building

## Database Models

The database currently contains models for:

- Room
- RoomBooking
- BookingParticipant
- ApplicationUser

The current user interface implements CRUD functionality for rooms. The booking-related models are included in the database structure for further development.

## Server-Side Validation

The application includes server-side validation for room data.

Current validation includes:

- Room names are limited to 2–50 valid characters.
- Room capacity must be between 1 and 100.
- Building names must be between 2 and 50 characters.
- Invalid form input is returned to the form with validation messages.

## Database

The application uses SQLite together with Entity Framework Core.

ApplicationDbContext manages the application's database entities and relationships. The database includes relationships between rooms, bookings, users and booking participants.

Examples include:

- A Room can have multiple RoomBookings.
- A user can organize multiple RoomBookings.
- A RoomBooking can have multiple BookingParticipants.
- A user can participate in multiple bookings.

Entity Framework Core migrations are used to create and update the database. Initial room data is also added through database seeding.

## CRUD Functionality

CRUD functionality is implemented for rooms:

- Create – add a new room
- Read – display rooms from the database
- Update – edit an existing room
- Delete – remove a room

## Error Handling

The application includes basic server-side error handling.

For example:

- Requests to update or delete a room that does not exist return a NotFound response.
- Invalid form input is handled through ModelState validation.
- Validation messages are displayed when submitted room data is invalid.
- ASP.NET Core's developer exception page is enabled in the Development environment.

## Logging

The application uses ASP.NET Core logging together with Serilog.

Serilog is configured to write log information to:

- The console
- Log files in the Logs folder

The current RoomController logs selected events, including displaying room data and creating new rooms.

## User Experience

The application has a simple and user-friendly design with clear navigation and consistent use of Bootstrap components. Images, icons, buttons and structured tables are used to improve visual appeal and make important information easy to identify.

Forms display validation messages to help users correct invalid input, while the overall interface is kept simple and easy to use.

## Navigation

The application currently contains the following main pages:

- Home – the main page, including a carousel with study room images.
- Rooms – displays available rooms and provides access to create, update and delete functionality.

A navigation bar is available throughout the application for quick access to the main pages.

## Project Structure

```
ITPE3200-Eksamensoppgave
├─ README.md
└─ StudyRoomBooking
   ├─ StudyRoomBooking.slnx
   ├─ StudyRoomBooking.Web
   │  ├─ appsettings.Development.json
   │  ├─ appsettings.json
   │  ├─ Controllers
   │  │  ├─ HomeController.cs
   │  │  └─ RoomController.cs
   │  ├─ Data
   │  │  ├─ ApplicationDbContext.cs
   │  │  ├─ IRoomRepository.cs
   │  │  └─ RoomRepository.cs
   │  ├─ libman.json
   │  ├─ Migrations
   │  │  ├─ 20261003121143_InitialCreate.cs
   │  │  ├─ 20261003121143_InitialCreate.Designer.cs
   │  │  └─ ApplicationDbContextModelSnapshot.cs
   │  ├─ Models
   │  │  ├─ ApplicationUser.cs
   │  │  ├─ BookingParticipant.cs
   │  │  ├─ Room.cs
   │  │  └─ RoomBooking.cs
   │  ├─ Program.cs
   │  ├─ Properties
   │  │  └─ launchSettings.json
   │  ├─ StudyRoomBooking.Web.csproj
   │  ├─ Views
   │  │  ├─ Home
   │  │  │  └─ Index.cshtml
   │  │  ├─ Room
   │  │  │  ├─ Create.cshtml
   │  │  │  ├─ Delete.cshtml
   │  │  │  ├─ Table.cshtml
   │  │  │  └─ Update.cshtml
   │  │  ├─ Shared
   │  │  │  ├─ _Layout.cshtml
   │  │  │  └─ _Layout.cshtml.css
   │  │  ├─ _ViewImports.cshtml
   │  │  └─ _ViewStart.cshtml
   │  └─ wwwroot
   │     └─ images
   │        ├─ K106.jpg
   │        ├─ PE242.jpg
   │        └─ PH356 Tone.jpg
   └─ tests
      └─ StudyRoomBooking.Tests
         ├─ StudyRoomBooking.Tests.csproj
         └─ UnitTest1.cs
```
Some files and folders are intentionally excluded from the repository through `.gitignore`.

These include:

- Build output such as `bin/` and `obj/`
- User-specific Visual Studio files such as `.vs/`, `*.suo` and `*.user`
- Editor and operating system files such as `.vscode/`, `.DS_Store` and `Thumbs.db`
- Downloaded client-side libraries under `wwwroot/lib`
- Serilog log files and the `Logs/` folder
- Local SQLite database files such as `.db`, `.db-shm` and `.db-wal`

These files are generated locally during development or when the application is running, and are therefore not included in the repository.

## How to Run the Application

### Requirements

- .NET 10.0 SDK
- Entity Framework Core CLI tools if dotnet ef is not already installed
- Node.js is not required.

### 1. Open the Project Folder

Open a terminal in the folder containing:

```text
StudyRoomBooking.Web.csproj
```

### 2. Restore Dependencies

Run:

```bash
dotnet restore
```

### 3. Create or Update the Database

Run:

```bash
dotnet ef database update
```

If the Entity Framework CLI tool is not installed, it can be installed with:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Build the Application

Run:

```bash
dotnet build
```

The build should complete successfully before starting the application.

### 5. Start the Application

Run:

```bash
dotnet run
```

The development launch settings currently use addresses such as:

```text
http://localhost:5247

https://localhost:7010
```

The exact address displayed in the terminal should be used when opening the application in a web browser.

## Testing

The current implementation supports manual testing of:

- Viewing rooms from the database
- Creating rooms
- Updating rooms
- Deleting rooms
- Room name validation
- Capacity validation
- Building validation
- Handling invalid room IDs when updating or deleting
- Navigation between the Home and Rooms pages

## MVP Scope

The current MVP focuses on:

- Study room management
- Database operations
- CRUD functionality for rooms
- Forms and server-side validation
- Basic error handling
- Logging
- Basic navigation and user interface
- Database models and relationships for future room booking functionality

## External Sources and Assistance

External sources and assistance used during development should be documented to make the origin of code and implementation ideas clear.

Examples used during development include:

- Microsoft ASP.NET Core documentation
- Microsoft Entity Framework Core documentation
- Bootstrap documentation
- Serilog documentation
- Course examples and lecture material from ITPE3200
- AI assistance for explanations, troubleshooting and code suggestions.
- AI assistance to compare the project implementation with the assignment requirements and to assist in writing and improving the README documentation

## Authors

ITPE3200 Web Applications – 2026H

Group members:

- [Amatarahman]
- [Riana]
- [Henni]
- [Maryan]
- [Christian]


