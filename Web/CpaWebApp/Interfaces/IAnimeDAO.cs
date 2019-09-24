using CpaWebApp.Models.AnimeDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Interfaces
{
    public interface IAnimeDAO
    {
        ResponseAnime Animes(ParametersAnime parameters);

        Anime Anime(int id);

        Anime Random();

        Anime Random(ParametersAnime parameters);
    }
}
