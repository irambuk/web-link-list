using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLinkList.EF.Model;

namespace WebLinkList.WebMvc.Models
{
    public class WebLinkHomeViewModel : BaseChartTimelineViewModel
    {
        public WebLink WebLink { get; set; }

        public IList<Usage> Usages { get; set; }

        public WebLinkHomeViewModel(UsageDropDownTypes type) : base(type)
        {
        }

        public IList<UsageDataPerUnitViewModel> GetUsageData()
        {
            var usageData = new List<UsageDataPerUnitViewModel>();

            if (Usages == null)
            {
                return usageData;
            }

            foreach (var date in UsageTimelineDates)
            {
                var usageCount = Usages.Where(u => u.CreatedDateTime.Date == date.Date).Count();
                var usageDataPerUnitViewModel = new UsageDataPerUnitViewModel { NoOfVisits = usageCount, UnitName = date.ToShortTimeString() };
                usageData.Add(usageDataPerUnitViewModel);
            }

            return usageData;
        }

        public IList<string> GetColours()
        {
            var list = new List<string>();

            var randomColorString = BaseRandomColorViewModel.GetRandomColor();

            foreach (var date in UsageTimelineDates)
            {
                list.Add(randomColorString);
            }

            return list;
        }

        public string GetChart()
        {
            var stringBuilder = new StringBuilder();

            //stringBuilder.Append("[");

            foreach (var usage in Usages)
            {
                stringBuilder.Append("{");

                stringBuilder.Append($"label:'{WebLink.Name}',");

                stringBuilder.Append($"data:[{String.Join(",", GetUsageData().Select(c => "" + c.NoOfVisits + "").ToList())}],");

                stringBuilder.Append($"backgroundColor:[{String.Join(",", GetColours().Select(c => "'" + c + "'").ToList())}],");

                stringBuilder.Append($"borderColor:[{String.Join(",", GetColours().Select(c => "'" + c + "'").ToList())}],");

                stringBuilder.Append($"borderWidth:1");


                stringBuilder.Append("},");
            }


            //stringBuilder.Append("]");


            return stringBuilder.ToString();
        }
    }
}
