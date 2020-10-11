using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderBoard.Entity.DbSettings
{
    /// <summary>
    /// Entity to read from appsettings
    /// </summary>
    public class PlayerStatsDatabaseSettings : IPlayerStatsDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IPlayerStatsDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

}
