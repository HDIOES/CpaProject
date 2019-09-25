using CpaWebApp.Interfaces;
using CpaWebApp.Models.AnimeDAO;
using CpaWebApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CpaWebApp.Services.AnimeDAO
{
    public class AnimeDAOVanilla : IAnimeDAO
    {

        private IMemoryCache _cache;
        private IConfiguration _config;

        public AnimeDAOVanilla(IMemoryCache cache, IConfiguration config)
        {
            this._cache = cache;
            this._config = config;
        }

        public Anime Anime(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseAnime Animes(ParametersAnime parameters)
        {
            throw new NotImplementedException();
        }

        public Anime Random()
        {
            return Random(null);
        }

        public Anime Random(ParametersAnime parameters)
        {

            ShikimoriProvider shikimoriProvider = new ShikimoriProvider(_cache, _config );

            ShikiApiLib.AnimeShortInfo vanillaAnime = shikimoriProvider.GetRandomTitle(new Models.Request.SearchRequest());

            return ConvertAnimeShortInfoToAnime(vanillaAnime);
        }

        private Anime ConvertAnimeShortInfoToAnime(ShikiApiLib.AnimeShortInfo vanillaAnime)
        {
            string shikiURL = _config.GetValue<string>("ShikiConfig:Domen");

            int kind = 0; // 0 — неизвестный (дефолтный) тип

            switch (vanillaAnime.Kind.ToLower())
            {
                case "tv":
                case "tv_13":
                case "tv_24":
                case "tv_48":
                    kind = 1;
                    break;
                case "movie":
                    kind = 2;
                    break;
                case "ova":
                    kind = 3;
                    break;
                case "ona":
                    kind = 4;
                    break;
                case "special":
                    kind = 5;
                    break;
                case "music":
                    kind = 6;
                    break;
            }

            int status = 0; // 0 — неизвестный (дефолтный) cnfnec

            switch (vanillaAnime.TitleStatus.ToLower())
            {
                case "anons":
                    status = 1;
                    break;
                case "ongoing":
                    status = 2;
                    break;
                case "released":
                    status = 3;
                    break;
            }


            Anime anime = new Anime()
            {
                id = vanillaAnime.TitleId,
                kind = kind,
                status = status,
                episodies = vanillaAnime.TotalEpisodes,
                episodiesAired = vanillaAnime.AiredEpisodes,
                score = 0,
                epidoseDuration = 0,
                names = new List<MultyName>
                {
                    new MultyName()
                    {
                        text = vanillaAnime.Name,
                        lang = "en_us",
                        relevance = 1
                    }
                },
                image = new MultyImage()
                {
                    serverURL = shikiURL,
                    x48 = vanillaAnime.Poster.x48,
                    x96 = vanillaAnime.Poster.x96,
                    x160 = vanillaAnime.Poster.preview,
                    x225 = vanillaAnime.Poster.original,
                    original = vanillaAnime.Poster.original,

                },
                links = new List<ExternalServiceAnime>
                {
                    new ExternalServiceAnime()
                    {
                        name = "Shikimori",
                        siteURL = shikiURL,
                        link = vanillaAnime.Url,
                        relevance = 1,
                        icon = "",
                        hasVideo = false,
                    }
                },
                aired = (vanillaAnime.AiredOn == null) ? new DateTime(0) : DateTime.Parse(vanillaAnime.AiredOn).Date,
                released = (vanillaAnime.ReleasedOn == null) ? new DateTime(0) : DateTime.Parse(vanillaAnime.ReleasedOn).Date,
                studios = null

            };

            // Русского названия может не быть в системе
            if (vanillaAnime.RusName != null) anime.names.Add(new MultyName()
            {
                text = vanillaAnime.RusName,
                lang = "ru_ru",
                relevance = 1
            });

            return anime;
        }
    }
}
