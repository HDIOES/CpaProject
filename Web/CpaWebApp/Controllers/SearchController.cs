using CpaWebApp.Models.Request;
using Microsoft.AspNetCore.Mvc;
using ShikiApiLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShikimoriRandomizer.Controllers
{
    [Route("/api/Search/")]
    public class SearchController : Controller
    {
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

        [HttpGet]
        public IEnumerable<AnimeShortInfo> Get([FromQuery] SearchRequest request)
        {
            int maxPage = 1024;
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

            var count = GetCountOfPages(searcher, 0, maxPage);
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
            return new List<AnimeShortInfo> { result[position] };
        }
    }
}
