Here's the updated **README.md** file with the correct syntax for bold letters and links:

---

# Contract Monthly Claim System (CMCS)

The **Contract Monthly Claim System (CMCS)** is a web-based application developed using **ASP.NET Core MVC**. The system allows Independent Contractor (IC) lecturers to submit their monthly claims, track their claim history, and manage their accounts. It also includes features for administrators to review and approve submitted claims.

## Table of Contents
- [Prerequisites](#prerequisites)
- [Installation and Setup](#installation-and-setup)
- [Running the Application](#running-the-application)
- [Usage](#usage)
- [Controllers and Views](#controllers-and-views)
- [Classes and Models](#classes-and-models)
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

3. **Database Setup** (Optional):
   - If the application requires a database, configure the **connection string** in the `appsettings.json` file.
   - Run **Entity Framework** migrations (if applicable) to set up your database schema:
     ```bash
     dotnet ef database update
     ```

4. **Build the Project**:
   Compile and build the project:
   ```bash
   dotnet build
   ```

## Running the Application

After setting up the application, run it with the following command:

```bash
dotnet run
```

Alternatively, you can use Visual Studio to run the application by pressing `F5` (Start Debugging) or `Ctrl+F5` (Start Without Debugging).

Once the application is running, navigate to the following URL in your browser:

```
https://localhost:5001
```

## Usage

- **Home Page**: Upon launching the application, you will be taken to the home page, which allows navigation to different parts of the system.
- **Submit a Claim**: You can submit new claims by navigating to the "Submit a Claim" section.
- **Claim History**: View the history of all claims submitted by the logged-in lecturer.
- **Login**: Use the login page to authenticate as an Independent Contractor (IC) lecturer or an administrator.

### Navigation Buttons:
- **Submit a Claim**: Directs you to the form where you can submit a new claim.
- **View Claim History**: Displays the list of all previously submitted claims.
- **Login**: Takes you to the login page where ICs or administrators can log into their accounts.

## Controllers and Views

### Controllers:
The application contains the following controllers, each handling different parts of the system:

1. **HomeController**:
   - Manages the home page.
   - Default landing page with navigation links.
  
2. **ClaimController**:
   - `Submit`: Handles claim submission form and processes the form data.
   - `History`: Displays a list of previously submitted claims by the user.
  
3. **AccountController**:
   - `Login`: Manages the login process for Independent Contractors and administrators.

### Views:
- **Views/Home/Index.cshtml**: The home page of the application.
- **Views/Claim/Submit.cshtml**: The form to submit new claims.
- **Views/Claim/History.cshtml**: Displays a list of submitted claims.
- **Views/Account/Login.cshtml**: Login page for users to authenticate.

## Classes and Models

### `Claim` Class:
Represents a claim submitted by a lecturer. It contains the following properties:
- `ClaimId`: The unique identifier for each claim.
- `TotalHours`: The total number of hours worked.
- `HourlyRate`: The rate per hour.
- `Comments`: Additional comments provided by the user.
- `TotalAmount`: Calculated value based on `TotalHours * HourlyRate`.
- `DateSubmitted`: The date when the claim was submitted.

### `User` Class:
Represents a user in the system, either an Independent Contractor or an administrator. This class handles basic user information and authentication:
- `UserId`: Unique identifier for the user.
- `Username`: Login name for the user.
- `Password`: User password for login authentication.
- `Role`: Specifies whether the user is an IC or administrator.

### `LoginViewModel` Class:
Handles the login form submission and validation.
- `Username`: Stores the username entered during login.
- `Password`: Stores the password entered during login.

## Troubleshooting

1. **View Not Found Error**:
   - If you encounter an error like "The view 'Submit' was not found", ensure that the corresponding view exists in the correct folder (e.g., `Views/Claim/Submit.cshtml`).

2. **Database Connection Issues**:
   - If the database isn't connecting, verify the connection string in the `appsettings.json` file.
   - Make sure your SQL Server is running and accessible.

3. **Login Issues**:
   - Ensure that valid user credentials are being used.
   - If you encounter authentication issues, check if the correct role-based access is configured.
/// <summary>
    /// Caleb Voskuil
    /// ST10397320
    /// Prog6212
    /// group 1
    /// References:https://youtu.be/GhQdlIFylQ8?si=cKIYSWunHDC1csWj
    /// https://youtu.be/gfkTfcpWqAY?si=3BredEtmLRK-IVXr
    /// https://youtu.be/wxznTygnRfQ?si=YdSPdyKekHStCUFz
    /// </summary>
