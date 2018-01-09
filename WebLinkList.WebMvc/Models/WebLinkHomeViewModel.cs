using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.EF.Model;

namespace WebLinkList.WebMvc.Models
{
    public class WebLinkHomeViewModel
    {
        public WebLink WebLink { get; set; }

        public IList<Usage> Usages { get; set; }
    }
}
