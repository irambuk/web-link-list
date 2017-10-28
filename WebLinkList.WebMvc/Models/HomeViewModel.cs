using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLinkList.WebMvc.Models
{
    public class HomeViewModel
    {
        public List<WebLinkViewModel> FaviouriteWebLinks { get; set; }
        public List<WebLinkViewModel> TopWebLinks { get; set; }
        public List<WebLinkViewModel> MostRecentlyViewedLinks { get; set; }
        public List<WebLinkViewModel> LeastRecentlyViewedLinks { get; set; }
        public List<WebLinkViewModel> WebLinks { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class WebLinkViewModel
    {
        public Guid WebLinkId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int CurrentCount { get; set; }
        public DateTime? LastVisitedDateTime { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public string LastVisitedDateTimeString {
            get
            {
                if (LastVisitedDateTime.HasValue && LastVisitedDateTime.Value != DateTime.MinValue)
                {
                    var diff = DateTime.Now.Subtract(LastVisitedDateTime.Value);

                    if (diff.Days < 7) {
                        return $"{diff.Days} day(s) {diff.Hours} hour(s) {diff.Minutes} minute(s)";
                    }
                    return LastVisitedDateTime.Value.ToString("G");
                }
                return string.Empty;
            }
        }
    }
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int CurrentCount { get; set; }
    }

    public enum PanelColors {

    }
}
