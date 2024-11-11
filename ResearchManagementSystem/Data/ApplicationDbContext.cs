using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResearchManagementSystem.Models;

namespace ResearchManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       

        public DbSet<AddAccomplishment> Production { get; set; }
        public DbSet<AddPresentation> Presentation { get; set; }
        public DbSet<AddPublication> Publication { get; set; }
        public DbSet<AddUtilization> Utilization { get; set; }
        public DbSet<AddPatent> Patent { get; set; }
        public DbSet<AddCitation> Citation { get; set; }

        public DbSet<AddFAQs> FAQs { get; set; }
        public object RankHistories { get; internal set; }
    }
}
