using Microsoft.EntityFrameworkCore;
using StudentManagement.Web.Models.Entities;

namespace StudentManagement.Web.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
