using ShikiApiLib;
using System.Collections.Generic;

namespace CpaWebApp.Models
{
    public class SearchViewModel
    {
        public List<Genre> Genres { get; set; }
        public Dictionary<string, string> Season { get; set; }
        public List<Studio> Studios { get; set; }

        public List<string> Kinds { get; set; }
        public List<string> TitleStatus { get; internal set; }
    }


}