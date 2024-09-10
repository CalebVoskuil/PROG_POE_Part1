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

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
