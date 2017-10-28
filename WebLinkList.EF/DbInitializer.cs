using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebLinkList.EF.Model;

namespace WebLinkList.EF
{
    public static class DbInitializer
    {
        public static void Initialize(WebLinkContext context)
        {
            context.Database.EnsureCreated();



            // Look for any students.
            if (context.WebLinks.Any())
            {
                return;   // DB has been seeded
            }

            var category1 = new Category { Id = Guid.NewGuid(), Name = "Tech", CreatedDateTime = DateTime.Now };
            var category2 = new Category { Id = Guid.NewGuid(), Name = "Life", CreatedDateTime = DateTime.Now };
            var category3 = new Category { Id = Guid.NewGuid(), Name = "Hack", CreatedDateTime = DateTime.Now };
            var category4 = new Category { Id = Guid.NewGuid(), Name = "News", CreatedDateTime = DateTime.Now };
            var category5 = new Category { Id = Guid.NewGuid(), Name = "Kids", CreatedDateTime = DateTime.Now };

            var categories = new Category[] {
                category1, category2, category3, category4, category5
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();


            var weblink1 = new WebLink { Id = Guid.NewGuid(), Name = "Google",  Url = "https://google.com", CreatedDateTime = DateTime.Now, IsFaviourite=true };
            var weblink2 = new WebLink { Id = Guid.NewGuid(), Name = "BBC", Url = "https://bbc.com", CreatedDateTime = DateTime.Now, IsFaviourite = true };
            var weblink3 = new WebLink { Id = Guid.NewGuid(), Name = "Yahoo", Url = "https://yahoo.com", CreatedDateTime = DateTime.Now };
            var weblink4 = new WebLink { Id = Guid.NewGuid(), Name = "CodeProject", Url = "https://codeproject.com", CreatedDateTime = DateTime.Now };
            var weblink5 = new WebLink { Id = Guid.NewGuid(), Name = "Baby Center", Url = "https://babycenter.com", CreatedDateTime = DateTime.Now };

            var weblinks = new WebLink[]
            {
                weblink1, weblink2, weblink3, weblink4, weblink5
            };
            foreach (WebLink w in weblinks)
            {
                context.WebLinks.Add(w);
            }
            context.SaveChanges();


            var weblinkCategory1 = new WebLinkCategory { WebLinkId = weblink1.Id, CategoryId = category1.Id};
            var weblinkCategory2 = new WebLinkCategory { WebLinkId = weblink2.Id, CategoryId = category2.Id };
            var weblinkCategory3 = new WebLinkCategory { WebLinkId = weblink3.Id, CategoryId = category3.Id };
            var weblinkCategory4 = new WebLinkCategory { WebLinkId = weblink4.Id, CategoryId = category4.Id };
            var weblinkCategory5 = new WebLinkCategory { WebLinkId = weblink5.Id, CategoryId = category5.Id };
            var weblinkCategory6 = new WebLinkCategory { WebLinkId = weblink5.Id, CategoryId = category2.Id };

            var weblinkCategories = new WebLinkCategory[]
            {
                weblinkCategory1, weblinkCategory2, weblinkCategory3, weblinkCategory4, weblinkCategory5, weblinkCategory6
            };
            foreach (WebLinkCategory w in weblinkCategories)
            {
                context.WebLinkCategories.Add(w);
            }
            context.SaveChanges();
            

            var usage1 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink1.Id, CreatedDateTime = DateTime.Now };
            var usage2 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink2.Id, CreatedDateTime = DateTime.Now };
            var usage3 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink3.Id, CreatedDateTime = DateTime.Now };
            var usage4 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink4.Id, CreatedDateTime = DateTime.Now };
            var usage5 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink1.Id, CreatedDateTime = DateTime.Now };
            var usage6 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink2.Id, CreatedDateTime = DateTime.Now };
            var usage7 = new Usage { Id = Guid.NewGuid(), WebLinkId = weblink3.Id, CreatedDateTime = DateTime.Now };

            var usages = new Usage[]
            {
                usage1, usage2, usage3, usage4, usage5
            };
            foreach (Usage u in usages)
            {
                context.Usages.Add(u);
            }
            context.SaveChanges();

        }
    }
}
