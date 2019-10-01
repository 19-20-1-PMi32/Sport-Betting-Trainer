using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBT.Database.Entities
{
    public class Account
    {
        [Column(TypeName = "varchar(50)"), Key]
        public string Email { get; set; }

        [Column(TypeName = "char(32)")]
        public string PasswordHash { get; set; }

        public float Ballance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
