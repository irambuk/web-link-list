using System;
using System.Collections.Generic;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class WebLink
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsFaviourite { get; set; }

        public string Url { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<WebLinkCategory> WebLinkCategories { get; set; }

        public List<Usage> Usages { get; set; }

    }
}
