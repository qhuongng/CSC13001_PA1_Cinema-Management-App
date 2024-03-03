using Microsoft.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=cinemaManagement;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=True;Trust Server Certificate=True");
        } catch (Exception ex)
        {
            Console.WriteLine("Failed to connect to database: " + ex.Message);
        }
    }
}