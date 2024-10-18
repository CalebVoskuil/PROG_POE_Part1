using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PROG_POE1.Data;

/// <summary>
/// Caleb Voskuil
/// ST10397320
/// Prog6221
/// group 1
/// References:https://youtu.be/GhQdlIFylQ8?si=cKIYSWunHDC1csWj
/// https://youtu.be/gfkTfcpWqAY?si=3BredEtmLRK-IVXr
/// https://youtu.be/wxznTygnRfQ?si=YdSPdyKekHStCUFz
/// Repository link : https://github.com/CalebVoskuil/PROG_POE_Part1.git
/// </summary>






var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();

// Add Entity Framework and Identity services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Part2")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configure identity options here
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CreateDefaultUsers(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Replace with your error handling page
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();

// Method to seed the database with default users
static async Task CreateDefaultUsers(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create roles if they do not exist
    string[] roles = { "Lecturer", "Coordinator" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Create and seed the Lecturer user
    var lecturer = new IdentityUser
    {
        UserName = "lecturer@example.com",
        Email = "lecturer@example.com",
        EmailConfirmed = true
    };
    var lecturerPassword = "Lecturer@123";
    var resultLecturer = await userManager.CreateAsync(lecturer, lecturerPassword);
    if (resultLecturer.Succeeded)
    {
        await userManager.AddToRoleAsync(lecturer, "Lecturer");
    }

    // Create and seed the Coordinator user
    var coordinator = new IdentityUser
    {
        UserName = "coordinator@example.com",
        Email = "coordinator@example.com",
        EmailConfirmed = true
    };
    var coordinatorPassword = "Coordinator@123";
    var resultCoordinator = await userManager.CreateAsync(coordinator, coordinatorPassword);
    if (resultCoordinator.Succeeded)
    {
        await userManager.AddToRoleAsync(coordinator, "Coordinator");
    }
}





// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~^^~~^^~~^^~[ End of File ]~^^~~^^~~^^~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~