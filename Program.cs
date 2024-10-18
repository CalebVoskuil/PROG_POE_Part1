using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG_POE1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext and Identity services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  // Ensure that authentication is added
app.UseAuthorization();

// Add the role seeding logic here
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await SeedRoles(roleManager, userManager);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Method to seed roles
async Task SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
    string[] roleNames = { "Lecturer", "Coordinator" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Optionally, seed some users with these roles
    // Example: Seeding an admin user for testing
    var user = new IdentityUser { UserName = "lecturer@domain.com", Email = "lecturer@domain.com" };
    var result = await userManager.CreateAsync(user, "Password123!");

    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(user, "Lecturer");
    }
}


// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~^^~~^^~~^^~[ End of File ]~^^~~^^~~^^~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~