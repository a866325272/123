using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.PlatformNews.Results
{
    public class PlatformNewsListResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
