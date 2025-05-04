


# Contract Monthly Claim System (CMCS)

The **Contract Monthly Claim System (CMCS)** is a web-based application developed using **ASP.NET Core MVC**. The system allows Independent Contractor (IC) lecturers to submit their monthly claims, track their claim history, and manage their accounts. It also includes features for administrators to review and approve submitted claims.

## Table of Contents
- [Prerequisites](#prerequisites)
- [Installation and Setup](#installation-and-setup)
- [Acquiring the Local Database](#acquiring-the-local-database)
- [Running the Application](#running-the-application)
- [Usage](#usage)
- [Troubleshooting](#troubleshooting)

## Prerequisites

Before running this project, ensure you have the following installed:

- **.NET Core SDK** (version 5.0 or later)
- **Visual Studio** (or another IDE like Rider) with **ASP.NET Core MVC** workload
- **SQL Server** or any other supported database engine for the application (optional, based on configuration)

## Installation and Setup

Follow these steps to set up the project:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repository-url/cmcs.git
   cd cmcs
   ```

2. **Restore the packages**:
   Open the project in Visual Studio or use the terminal:
   ```bash
   dotnet restore
   ```

3. **Build the Project**:
   Compile and build the project:
   ```bash
   dotnet build
   ```

## Acquiring the Local Database

To set up the local database on your machine, follow these steps:

1. **Open the Database Project**:
   - If you have a separate database project, open it in Visual Studio. If the database is part of the main project, you can skip this step.

2. **Check the Connection String**:
   - Ensure the connection string in the `appsettings.json` file is pointing to your local SQL Server instance. The typical connection string looks like this:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CMCS;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

3. **Create the Local Database**:
   - If you haven't created the database yet, you can do this through Visual Studio:
     - Right-click on the project in **Solution Explorer** and select **Add > New Item**.
     - Choose **SQL Server Database** and give it a name (e.g., `CMCS.mdf`).
     - After creating it, right-click the database file in **Solution Explorer** and select **Open** to launch the SQL Server Object Explorer.
     - From here, you can right-click the database and select **New Query** to run any necessary SQL scripts to set up your schema.

4. **Run Entity Framework Migrations** (if applicable):
   - If you are using Entity Framework Core, ensure your migrations are up to date:
     ```bash
     dotnet ef database update
     ```
   - This command will apply any pending migrations to your local database.

5. **Seed the Database** (if needed):
   - If your application requires initial data (like hardcoded users), ensure that the seeding logic is implemented in your `DbContext` or wherever your application handles database initialization.

## Running the Application

After setting up the local database, run the application with the following command:

```bash
dotnet run
```

Alternatively, you can use Visual Studio to run the application by pressing `F5` (Start Debugging) or `Ctrl+F5` (Start Without Debugging).

Once the application is running, navigate to the following URL in your browser:

```
https://localhost:5001
```

## Usage

- Independent Contractor (IC) lecturers can log in to submit their monthly claims.
- Administrators can review and approve submitted claims.
- The application provides a user-friendly interface for managing claims and tracking history.


## Troubleshooting

- Ensure that your SQL Server instance is running and accessible.
- Check for any migration errors and verify the connection string in `appsettings.json`.
- If you encounter issues while running the application, refer to the logs for more details.

```


/// <summary>
    /// Caleb Voskuil
    /// ST10397320
    /// Prog6212
    /// group 1
    /// References:https://youtu.be/GhQdlIFylQ8?si=cKIYSWunHDC1csWj
    /// https://youtu.be/gfkTfcpWqAY?si=3BredEtmLRK-IVXr
    /// https://youtu.be/wxznTygnRfQ?si=YdSPdyKekHStCUFz
    /// </summary>
