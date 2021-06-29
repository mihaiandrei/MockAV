using Microsoft.EntityFrameworkCore;

namespace NotificationRepository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<NotificationDocument> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=.\notifications.db");
    }
}
