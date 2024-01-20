using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }   
}
