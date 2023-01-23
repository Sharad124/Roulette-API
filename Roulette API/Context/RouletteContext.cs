using Microsoft.EntityFrameworkCore;
using Roulette_API.Models;

namespace Roulette_API.Context
{
    public class RouletteContext : DbContext
    {
        public RouletteContext(DbContextOptions<RouletteContext> options) : base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BetType> BetTypes { get; set; }
        public DbSet<Spin> spins { get; set; }
    }
}
