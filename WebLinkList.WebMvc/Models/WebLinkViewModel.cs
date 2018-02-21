using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.EF.Model;

namespace WebLinkList.WebMvc.Models
{
    public class WebLinkModifyViewModel
    {
        public WebLink WebLink { get; set; }

        public List<CategoryItem> CategoryItems { get; set; }        
    }

    public class CategoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
