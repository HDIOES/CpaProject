using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class ResponseAnime : Response
    {
        public ICollection<Anime> animes { get; set; }
    }
}
