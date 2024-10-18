using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG_POE1.Models;


namespace PROG_POE1.Data
{
   
    
        public class AppDbContext : IdentityDbContext<IdentityUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Claim> Claims { get; set; }
        }
    
}
