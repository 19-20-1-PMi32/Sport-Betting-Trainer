using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBT.Database.Entities
{
    public class Site
    {
        [Column(TypeName = "varchar(10)")]
        public string Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        public int LastUpdate { get; set; }

        public float FirstWin { get; set; }

        public float SecondWin { get; set; }

        public float? Draw { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
