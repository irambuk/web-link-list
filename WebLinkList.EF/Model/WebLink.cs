using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class WebLink
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsFaviourite { get; set; }

        public bool IsSingleReadLink { get; set; }

        public string Url { get; set; }

        public byte[] FaviconImageBytes { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<WebLinkCategory> WebLinkCategories { get; set; }

        public List<Usage> Usages { get; set; }

        public string FaviconImageBase64
        {
            get
            {
                if (FaviconImageBytes != null && FaviconImageBytes.Length > 0)
                {
                    var base64 = Convert.ToBase64String(FaviconImageBytes);

                    return base64;
                }

                return string.Empty; 
                
            }
        }

    }
}
