using CpaWebApp.Models.Request;
using Microsoft.AspNetCore.Mvc;
using ShikiApiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using CpaWebApp.Providers;
using Microsoft.Extensions.Caching.Memory;


namespace ShikimoriRandomizer.Controllers
{
    [Route("/api/Search/")]
    public class SearchController : Controller
    {
        private IMemoryCache _cache;

        public SearchController(IMemoryCache cache)
        {
            this._cache = cache;
        }

        [HttpGet]
        public IEnumerable<AnimeShortInfo> Get([FromQuery] SearchRequest request)
        {
            var provider = new ShikimoriProvider(_cache);
            return new List<AnimeShortInfo> { provider.GetRandomTitle(request) };
        }
    }
}
