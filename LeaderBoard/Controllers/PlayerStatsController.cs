using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderBoard.Data;
using LeaderBoard.Entity;
using LeaderBoard.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatController : ControllerBase
    {
        private PlayerStatsService _playerStatsService;
        public PlayerStatController(PlayerStatsService playerStatsService)
        {
            this._playerStatsService = playerStatsService;
        }
        [HttpGet("{id}")]
        //[Route("api/[controller]/id")]
        public IEnumerable<PlayerStat> GetPlayerStats(string id)
        {
            return _playerStatsService.GetPlayerStats(id);
        }

        [HttpGet("{id}/{duration}")]
        public IEnumerable<PlayerStat> GetPlayerStatsDuration(string id, int duration)
        {
            return _playerStatsService.GetPlayerStats(id, duration);
        }

        [HttpPost]
        public ActionResult AddPlayerStat([FromBody]PlayerStat playerStat)
        {
            try
            {
                _playerStatsService.Create(playerStat);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
    }
}