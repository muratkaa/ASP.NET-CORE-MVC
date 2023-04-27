using Microsoft.EntityFrameworkCore;
using StudentApp.Models.DBEntities;

namespace StudentApp.DAL
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
