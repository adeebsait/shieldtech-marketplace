using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shield_Tech.Models
{
    public class AppDataContext : IdentityDbContext<AppUser>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) :
        base(options)
        { }
        public DbSet<Product> Products { get; set; }

        

    }
}
