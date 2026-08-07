# VideGameListAPI

## Project Overview

VideGameListAPI is a .NET capstone project demonstrating a complete solution with:

- A C# Web API (`TaskTracker.Api`) that exposes CRUD endpoints for video games stored in memory.
- A C# Console client (`TaskTracker.Client`) that consumes the web API and allows users to create, read, update, and delete video game entries.
- A C# test project (`TaskTracker.Tests`) that verifies the API controller behaviors using xUnit.



- `TaskTrackerCapstome/TaskTracker.Api` - Web API project
- `TaskTrackerCapstome/TaskTracker.Client` - Console client project
- `TaskTrackerCapstome/TaskTracker.Tests` - xUnit test project


## How to Build

From the repository root:

```powershell
cd C:\Users\Tj\Documents\GitHub\VideGameListAPI
dotnet build TaskTrackerCapstone.slnx
```

## How to Run

### Option 1: Use the batch files (Windows)

From the repository root:

```powershell
.\run-api.bat
.\run-client.bat
.\run-tests.bat
```

This is the simplest way to run the project locally.

### Option 2: Run manually

Start the API:

```powershell
dotnet run --project TaskTrackerCapstome/TaskTracker.Api/TaskTracker.Api.csproj --urls "http://localhost:5000"
```

Open a second terminal and run the client:

```powershell
cd TaskTrackerCapstome/TaskTracker.Client
dotnet run
```

If the API is running on a different port, set the `GAME_API_URL` environment variable before starting the client:

```powershell
$env:GAME_API_URL = "http://localhost:5000/api/games"
cd TaskTrackerCapstome/TaskTracker.Client
dotnet run
```

### Run Tests

From the repository root:

```powershell
dotnet test TaskTrackerCapstone.slnx
```

## API Endpoints

- `GET /api/games` - Retrieve all games
- `GET /api/games/{id}` - Retrieve a game by ID
- `POST /api/games` - Add a new game
- `PUT /api/games/{id}` - Update an existing game
- `DELETE /api/games/{id}` - Delete a game


## Notes

- Data is stored in memory and resets when the API restarts.
- The client uses a console menu to interact with the API.
