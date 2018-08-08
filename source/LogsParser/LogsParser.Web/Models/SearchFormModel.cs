using System.Collections.Generic;

namespace LogsParser.Web.Models
{
    public class SearchFormModel
    {
        public string SearchPattern { get; set; }

        public IEnumerable<string> Directories { get; set; }
    }
}