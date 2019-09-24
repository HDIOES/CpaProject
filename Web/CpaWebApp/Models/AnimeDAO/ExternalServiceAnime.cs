using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class ExternalServiceAnime : ExternalService
    {
        public string link  { get; set; }
        public int relevance  { get; set; }
        public bool hasVideo  { get; set; }
    }
}
