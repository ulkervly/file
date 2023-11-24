using Microsoft.EntityFrameworkCore;
using PustokPractice.Models;

namespace PustokPractice.DAL
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<Service> Services { get; set; }

    }
}
