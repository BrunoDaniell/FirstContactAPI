using Microsoft.EntityFrameworkCore;

namespace FirstContactAPI.Model
{
    public class FirstContactContext : DbContext
    {
        public FirstContactContext(DbContextOptions<FirstContactContext> options) : base(options)
        { 
            Database.EnsureCreated(); 
        }

        public DbSet<FirstContact> FirstContacts { get; set; }
    }
}
