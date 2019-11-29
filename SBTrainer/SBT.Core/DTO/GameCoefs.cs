using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Core.DTO
{
    public class GameCoefs
    {
        public string Team1 { get; set; }

        public string Team2 { get; set; }

		public int CommenceTime { get; set; }

        public List<SiteInfo> SitesInfo { get; set; }
    }
}
