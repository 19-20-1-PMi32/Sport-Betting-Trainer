using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBT.Database.Entities
{
    public class SportData
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Group { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Details { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SportId { get; set; }

        public Sport Sport { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
