using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Core.DTO
{
    public class SiteInfo
    {
        public string Name { get; set; }

        public int LastUpdate { get; set; }

        public float FirstWinCoef { get; set; }

        public float SecondWinCoef { get; set; }
    }
}
