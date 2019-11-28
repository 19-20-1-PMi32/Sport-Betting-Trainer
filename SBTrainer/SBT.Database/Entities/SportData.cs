using System;
using System.Collections.Generic;

namespace SBT.Database.Entities
{
    public class SportData
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public string Group { get; set; }

        public string Details { get; set; }

        public string Title { get; set; }

        public string SportId { get; set; }

        public Sport Sport { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
