using System;
using System.Collections.Generic;

namespace SBT.Database.Entities
{
    public class Sport
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<SportData> SportData { get; set; }
    }
}
