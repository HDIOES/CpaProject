using ShikiApiLib;

namespace CpaWebApp.Models.Request
{
    public class SearchRequest
    {
        public string Text { get; set; }
        public int[] Genres { get; set; }
        public int[] Studios { get; set; }
        public string[] Seasons { get; set; }
        public AKind[] Kinds { get; set; }
        public TitleStatus[] TitleStatuses { get; set; }
        public bool Censored { get; internal set; }
    }
}
