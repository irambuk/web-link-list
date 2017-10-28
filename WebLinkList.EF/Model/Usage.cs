using System;
using System.Collections.Generic;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class Usage
    {
        public Guid Id { get; set; }

        public Guid WebLinkId { get; set; }

        public WebLink WebLink { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}
