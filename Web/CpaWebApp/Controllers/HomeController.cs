using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CpaWebApp.Models;
using ShikiApiLib;
using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;

namespace CpaWebApp.Controllers
{



    public class HomeController : Controller
    {

        private IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            this._config = config;

            ShikiApiStatic.Domen = _config.GetValue<string>("ShikiConfig:Domen");
            ShikiApiStatic.DomenApi = _config.GetValue<string>("ShikiConfig:DomenApi");
        }

        public static string ToEnumString<T>(T type)
        {

            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public Dictionary<string, string> GenerateYears(int from, int to)
        {
            var result = new Dictionary<string, string>();
            for (int i = to; i >= from; i--)
            {
                for (int s = 0; s < 4; s++)
                {
                    result.Add($"{((Season)s).ToString()}_{i.ToString()}", $"{ToEnumString((Season)s)} {i.ToString()}");
                }
                result.Add(i.ToString(), i.ToString());
            }
            return result;
        }

        public ActionResult Index(string year)
        {
            var model = new SearchViewModel();

            model.Genres = ShikiApiStatic.GetGenres().Where(x => x.kind == "anime").ToList();
            model.Studios = ShikiApiStatic.GetStudios();
            model.Season = GenerateYears(1980, DateTime.Now.Year);
            model.Kinds = Enum.GetValues(typeof(AKind)).Cast<AKind>().ToList().ConvertAll(x => x.ToString());
            model.TitleStatus = Enum.GetValues(typeof(TitleStatus)).Cast<TitleStatus>().ToList().ConvertAll(x => x.ToString());
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
