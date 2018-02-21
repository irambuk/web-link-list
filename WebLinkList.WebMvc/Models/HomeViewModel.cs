using System;
using System.Collections.Generic;
using System.Drawing;
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
        public List<WebLinkViewModel> SingleReadLinks { get; set; }
        public List<WebLinkViewModel> WebLinks { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public string CurrentSearchString { get; set; }
    }

    public class BaseRandomColorViewModel
    {
        private string _randomColor;

        public string RandomColor
        {
            get
            {
                if (!string.IsNullOrEmpty(_randomColor))
                {
                    return _randomColor;
                }

                _randomColor = GetRandomColor();

                return _randomColor;            }
        }


        public static string GetRandomColor()
        {
            var random = new Random();
            var color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            var colorString = $"rgba({color.R},{color.G},{color.B},1)";

            return colorString;
        }
    }

    public class WebLinkViewModel : BaseRandomColorViewModel
    {
        public Guid WebLinkId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int CurrentCount { get; set; }
        public DateTime? LastVisitedDateTime { get; set; }    
        
        public string ImageBase64 { get; set; }

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

        public string FavIconUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Url))
                    return string.Empty;

                return $"http://www.google.com/s2/favicons?domain={Url}";
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
