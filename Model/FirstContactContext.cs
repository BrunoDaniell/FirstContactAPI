using Microsoft.EntityFrameworkCore;

namespace FirstContactAPI.Model
{
    public class FirstContactContext : DbContext
    {
        public DbSet<FirstContact> FirstContacts { get; set; }

        public FirstContactContext(DbContextOptions<FirstContactContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=test.sqlite");
        }
    }
}
