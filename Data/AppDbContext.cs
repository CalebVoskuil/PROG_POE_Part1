using Microsoft.EntityFrameworkCore;
using PROG_POE1.Models;


namespace PROG_POE1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Claim> Claims { get; set; }
    }
}
