using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
