using System;
using System.Collections.Generic;

namespace SBT.Database.Entities
{
    public class Account
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public float Ballance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
