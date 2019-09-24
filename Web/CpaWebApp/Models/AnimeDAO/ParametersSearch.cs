using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class ParametersSearch
    {
        public string phrase { get; set; }
        public int page  { get; set; }
        public int limit  { get; set; }
        public int order  { get; set; }
        public int[] kinds  { get; set; }
        public int[] statuses  { get; set; }
        public int[] ratings  { get; set; }
        public int[] ids  { get; set; }
        public int[] genres  { get; set; }
        public int[] studios  { get; set; }
        public ICollection<ParameterExtendInt> score  { get; set; }
        public ICollection<ParameterExtendInt> date  { get; set; }
    }
}
