using CpaWebApp.Models.Request;
using Microsoft.AspNetCore.Mvc;
using ShikiApiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using CpaWebApp.Providers;
using Microsoft.Extensions.Caching.Memory;
using CpaWebApp.Interfaces;
using CpaWebApp.Models.AnimeDAO;

namespace ShikimoriRandomizer.Controllers
{
    [Route("/api/Search/")]
    public class SearchController : Controller
    {
        private IAnimeDAO _animeDAO;

        public SearchController(IAnimeDAO animeDAO)
        {
            this._animeDAO = animeDAO;
        }

        [HttpGet]
        //public IEnumerable<Anime> Get([FromQuery] ParametersAnime request)
        public IEnumerable<AnimeShortInfo> Get([FromQuery] SearchRequest request)
        {

            //Anime anime = _animeDAO.Random(request);

            // Временная костылизация
            Anime anime = _animeDAO.Random(ConvertToParametersAnime(request));

            //return new List<Anime> { anime };
            return new List<AnimeShortInfo>
            {
                new AnimeShortInfo
                {
                    Name = anime.names.First().text,
                    Url = anime.links.First().link
                }
            };
        }

        // После обновления View метод должен уйти туда, откуда пришел
        private ParametersAnime ConvertToParametersAnime(SearchRequest source)
        {
            ParametersAnime parameters = new ParametersAnime()
            {
                phrase = source.Text,
                genres = source.Genres,
                studios = source.Studios
            };

            return parameters;
        }
    }
}
