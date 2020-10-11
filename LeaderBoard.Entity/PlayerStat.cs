using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeaderBoard.Entity
{
    public class PlayerStat
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Match { get; set; }

        public int Kills { get; set; }

        public long Score { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
