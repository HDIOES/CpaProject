using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class Response
    {
        public int rows  { get; set; }
        public int pages  { get; set; }
        public int rowsPerPage  { get; set; }
        public int rowsLastPage  { get; set; }
    }
}
