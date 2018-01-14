using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLinkList.EF.Model;

namespace WebLinkList.WebMvc.Models
{
    public class CategoryHomeViewModel : BaseChartTimelineViewModel
    {
        public Category Category { get; set; }

        public IList<WebLinkViewModel> WebLinks { get; set; }

        public IList<Usage> Usages { get; set; }        

        public CategoryHomeViewModel(UsageDropDownTypes type) : base(type)
        {
        }

        public IList<UsageDataPerUnitViewModel> GetUsageDataPerWebLinkId(Guid weblinkId)
        {
            var usageData = new List<UsageDataPerUnitViewModel>();

            if (Usages == null)
            {
                return usageData;
            }

            foreach (var date in UsageTimelineDates)
            {
                var usageCount = Usages.Where(u => u.WebLinkId == weblinkId && u.CreatedDateTime.Date == date.Date).Count();
                var usageDataPerUnitViewModel = new UsageDataPerUnitViewModel { NoOfVisits = usageCount, UnitName = date.ToShortTimeString() };
                usageData.Add(usageDataPerUnitViewModel);
            }

            return usageData;
        }

        public IList<string> GetColours(Guid weblinkId)
        {
            var list = new List<string>();

            var weblink = WebLinks.FirstOrDefault(w => w.WebLinkId == weblinkId);

            if (weblink == null)
            {
                return list;
            }

            foreach (var date in UsageTimelineDates)
            {
                list.Add(weblink.RandomColor);
            }
            
            return list;
        }

        public string GetChart()
        {
            var stringBuilder = new StringBuilder();

            //stringBuilder.Append("[");

            foreach (var weblink in WebLinks)
            {
                stringBuilder.Append("{");

                stringBuilder.Append($"label:'{weblink.Name}',");

                stringBuilder.Append($"data:[{String.Join(",", GetUsageDataPerWebLinkId(weblink.WebLinkId).Select(c => "" + c.NoOfVisits + "").ToList())}],");

                stringBuilder.Append($"backgroundColor:[{String.Join(",", GetColours(weblink.WebLinkId).Select(c => "'" + c + "'").ToList())}],");

                stringBuilder.Append($"borderColor:[{String.Join(",", GetColours(weblink.WebLinkId).Select(c => "'" + c + "'").ToList())}],");

                stringBuilder.Append($"borderWidth:1");


                stringBuilder.Append("},");
            }


            //stringBuilder.Append("]");


            return stringBuilder.ToString();
        }
    }
}
