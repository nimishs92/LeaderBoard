using LeaderBoard.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeaderBoard.Data
{
    public class LeaderBoardDbContext : DbContext
    {
        public LeaderBoardDbContext(DbContextOptions<LeaderBoardDbContext> context) : base(context)
        {

        }
        public DbSet<PlayerStat> PlayerStats { get; set; }
    }
}
