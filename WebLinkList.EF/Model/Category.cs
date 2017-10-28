using System;
using System.Collections.Generic;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<WebLinkCategory> WebLinkCategories { get; set; }
    }
}
