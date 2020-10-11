using LeaderBoard.Entity;
using System;
using System.Collections.Generic;

namespace LeaderBoard.BusinessLogic
{
    public class PlayerStatsLogic
    {

        public IEnumerable<PlayerStat> GetPlayerStats(string id)
        {
            //return _context.PlayerStats.Where<PlayerStat>(playerStat => playerStat.UserName.Equals(id,StringComparison.OrdinalIgnoreCase));
            return _playerStatsService.Get().Where<PlayerStat>(playerStat => playerStat.UserName.ToLower() == id.ToLower());
            //var allTable = from playerStat in _playerStatsService.Get()
            //               where playerStat.UserName == id
            //               select playerStat;

            //return allTable;
        }
    }
}
