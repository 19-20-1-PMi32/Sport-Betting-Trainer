using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBT.Database.Entities
{
    public class Sport
    {
        [Column(TypeName = "varchar(10)")]
        public string Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        public ICollection<SportData> SportData { get; set; }
    }
}
