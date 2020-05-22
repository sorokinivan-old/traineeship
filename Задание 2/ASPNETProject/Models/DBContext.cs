using Microsoft.EntityFrameworkCore;

namespace ASPNETProject.Models
{

    public class DBContext : DbContext
    {
        public static string connString = "Host=localhost;Port=5432;Database=traineeship;Username=postgres;Password=root";


        public DBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connString);
        }
    }
}
