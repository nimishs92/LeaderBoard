using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderBoard.Data;
using LeaderBoard.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private LeaderBoardDbContext _context;
        public LeaderBoardController(LeaderBoardDbContext context)
        {
            this._context = context;
        }
        [HttpGet("{match}/{duration}")]
        public IEnumerable<LeaderBoardEntry> LeaderBoard(string match, int duration)
        {
            var allTable = from playerStat in _context.PlayerStats
                           where playerStat.TimeStamp >= DateTime.Now.AddHours(-duration) & playerStat.Match.Equals(match, StringComparison.OrdinalIgnoreCase)
                           group playerStat by playerStat.UserName into g
                           orderby g.Max(g => g.Score) descending
                           select new LeaderBoardEntry
                           {
                               UserName = g.Key,
                               Score = g.Max(g => g.Score),
                               Kills = g.Max(g => g.Kills)
                           };

            var data = from playerStat in _context.PlayerStats
                       where playerStat.TimeStamp >= DateTime.Now.AddHours(-duration) & playerStat.Match.Equals(match, StringComparison.OrdinalIgnoreCase)
                       group playerStat by playerStat.UserName into g
                       orderby g.Max(g => g.Score) descending
                       select new LeaderBoardEntry
                       {
                           UserName = g.Key,
                           Score = g.Max(g => g.Score),
                           Kills = g.Max(g => g.Kills),
                           Rank = allTable.Count(s => s.Score > g.Max(g => g.Score)) + 1
                       };

            return data;
        }
    }
}