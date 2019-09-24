using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class Anime
    {
        public int id { get; set; }
        public int kind { get; set; }
        public int status { get; set; }
        public int episodies { get; set; }
        public int episodiesAired { get; set; }
        public int score { get; set; }
        public int epidoseDuration { get; set; }
        public ICollection<MultyName> names { get; set; }
        public ICollection<ExternalServiceAnime> links { get; set; }
        public MultyImage image  { get; set; }
        public DateTime aired  { get; set; }
        public DateTime released  { get; set; }
        public ICollection<Studio> studios  { get; set; }
    }
}
