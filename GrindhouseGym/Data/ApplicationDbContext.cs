using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GrindhouseGym.Models; // <--- 1. ADD THIS LINE so it can find your classes

namespace GrindhouseGym.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 2. ADD THESE TWO LINES
        // These tell the database: "Create tables named 'Plans' and 'Members'"
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}