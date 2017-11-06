using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WebLinkList.EF.Model
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public Color GraphColor { get; set; } = Color.White;

        public int GraphColorInt
        {
            get { return GraphColor.ToArgb(); }
            set { GraphColor = Color.FromArgb(value); }
        }

        public List<WebLinkCategory> WebLinkCategories { get; set; }
    }
}
