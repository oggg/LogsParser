using System.ComponentModel.DataAnnotations;

namespace LogsParser.Web.Models
{
    public class SearchFormPostModel
    {
        [Required]
        public string SearchPattern { get; set; }

        [Required]
        public string FolderSelector { get; set; }
    }
}