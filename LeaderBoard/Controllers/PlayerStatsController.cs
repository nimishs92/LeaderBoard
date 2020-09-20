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
    public class PlayerStatController : ControllerBase
    {
        private LeaderBoardDbContext _context;
        public PlayerStatController(LeaderBoardDbContext context)
        {
            this._context = context;
        }
        [HttpGet("{id}")]
        //[Route("api/[controller]/id")]
        public IEnumerable<PlayerStat> GetPlayerStats(string id)
        {
            return _context.PlayerStats.Where<PlayerStat>(playerStat => playerStat.UserName.Equals(id,StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet("{id}/{duration}")]
        //[Route("api/[controller]/id")]
        public IEnumerable<PlayerStat> GetPlayerStatsDuration(string id, int duration)
        {
            return _context.PlayerStats.Where<PlayerStat>(playerStat => playerStat.UserName.Equals(id, StringComparison.OrdinalIgnoreCase)& playerStat.TimeStamp >= DateTime.Now.AddHours(-duration));
        }

        [HttpPost]
        public ActionResult AddPlayerStat([FromBody]PlayerStat playerStat)
        {
            try
            {
                _context.PlayerStats.Add(playerStat);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }
        
    }
}