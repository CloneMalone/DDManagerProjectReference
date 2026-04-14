using DnDManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DnDManager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<SessionLog> SessionLogs { get; set; }
        public DbSet<Combatant> Combatants { get; set; }
    }
}