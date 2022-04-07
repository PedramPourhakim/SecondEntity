using _01_Query.Contract.Slide;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent :ViewComponent
    {
        private readonly ISlideQuery slideQuery;
        public SlideViewComponent(ISlideQuery slideQuery)
        {
            this.slideQuery = slideQuery;
        }
        public IViewComponentResult Invoke()
        {
            var slides = slideQuery.GetSlides();
            return View(slides);
        }
    }
}
