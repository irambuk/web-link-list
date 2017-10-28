using System;
using System.Collections.Generic;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class WebLinkCategory
    {
        public Guid WebLinkId { get; set; }

        public WebLink WebLink { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
