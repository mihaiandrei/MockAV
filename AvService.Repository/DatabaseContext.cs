using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AvService.Repository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<NotificationDocument> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=.\notifications.db");
    }
}
