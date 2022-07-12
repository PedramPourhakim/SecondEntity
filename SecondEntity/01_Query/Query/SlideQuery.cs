using _01_Query.Contract.Slide;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class SlideQuery : ISlideQuery
    {
         private readonly ShopContext context;

        public SlideQuery(ShopContext context)
        {
            this.context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return context.Slides
                .Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Picture=x.Picture,
                    PictureAlt=x.PictureAlt,
                    PictureTitle=x.PictureTitle,
                    BtnText=x.BtnText,
                    Heading=x.Heading,
                    Link=x.Link,
                    Text=x.Text,
                    Title=x.Title
                }).ToList();
        }
    }
}
