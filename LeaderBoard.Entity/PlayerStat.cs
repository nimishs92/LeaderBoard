using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaderBoard.Entity
{
    public class PlayerStat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Match { get; set; }

        public int Kills { get; set; }

        public long Score { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
