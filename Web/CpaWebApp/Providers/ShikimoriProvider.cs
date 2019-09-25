using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CpaWebApp.Models.Request;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShikiApiLib;

namespace CpaWebApp.Providers
{
    public class ShikimoriProvider
    {
        private DateTime _lastUpdate;
        private int _count;
        private IConfiguration _config;

        private readonly IMemoryCache _cache;

        public ShikimoriProvider(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            if (_cache.TryGetValue("last_update", out var value))
            {
                _lastUpdate = (DateTime)value;
            }
            _count = _cache.Get<int>("count");

            _config = config;
        }

        private int GetCountOfPages(AnimeSearch search, int from, int to, int counter = 0)
        {
            counter++;
            if (to - from == 1)
            {
                search.Page = to;
                var result = search.GetSearch();
                if (result != null && result.Count > 0)
                {

                    return to;
                }
                else
                {
                    return from;
                }
            }
            else
            {
                var delemiter = (from + to) / 2;
                search.Page = delemiter;

                var result = search.GetSearch();
                if (result != null && result.Count > 0)
                {
                    return GetCountOfPages(search, delemiter, to);
                }
                else
                {
                    return GetCountOfPages(search, from, delemiter);
                }
            }
        }

        public AnimeShortInfo GetRandomTitle(SearchRequest request)
        {
            ShikiApiStatic.Domen = _config.GetValue<string>("ShikiConfig:Domen");
            ShikiApiStatic.DomenApi = _config.GetValue<string>("ShikiConfig:DomenApi");

            var maxPage = 1024;
            var searcher = new AnimeSearch();
            if (request.Genres != null)
                foreach (var g in request.Genres)
                {
                    searcher.Genre.Add(g, true);
                }
            if (request.Studios != null)
                foreach (var s in request.Studios)
                {
                    searcher.Studio.Add(s, true);
                }
            if (request.Seasons != null)
            {
                maxPage = request.Seasons.Count() * 20;
                if (maxPage > 1024) maxPage = 1024;
                foreach (var s in request.Seasons)
                {
                    searcher.Season.Add(s, true);
                }
            }
            if (request.Text != null)
                searcher.SearchText = request.Text;
            if (request.Kinds != null)
                foreach (var k in request.Kinds)
                {
                    searcher.Kind.Add(k, true);
                }
            if (request.TitleStatuses != null)
            {
                foreach (var k in request.TitleStatuses)
                {
                    searcher.TitleStatus.Add(k, true);
                }
            }

            var result = new List<AnimeShortInfo>();
            if (_lastUpdate.Date != DateTime.Today || _count == 0)
            {
                _count = GetCountOfPages(searcher, 0, maxPage);
                _lastUpdate = DateTime.Now;
                _cache.Set("count", _count);
                _cache.Set("last_update", _lastUpdate);
            }

            var count = _count;
            searcher.Page = count;
            searcher.Censored = request.Censored;
            var lastPageCount = searcher.GetSearch().Count();
            count++;
            var totalCount = (count - 1) * 50 + lastPageCount;
            var titleNum = new Random().Next(0, totalCount);
            var page = titleNum / 50;
            var position = titleNum - 50 * page;
            searcher.Page = page;
            var local = searcher.GetSearch();
            result.AddRange(local);
            return result[position];
        }
    }
}
