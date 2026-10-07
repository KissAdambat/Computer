using Microsoft.EntityFrameworkCore;
namespace Computer.Models
{
    public class ComputerShopDBContext : DbContext
    {
        public ComputerShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public ComputerShopDBContext()
        {
        }

        public DbSet<Osystem> Osystems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user=root;password=;database=computer");
        }
    }
}
