using System.Collections.Generic;

namespace SBT.WebApp.ViewModels
{
    public class SportModel
    {
        public string Name { get; set; }

        public IList<SportDataModel> SportDataModels { get; set; }
    }
}
