using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.SlideContracts;
using ShopManagement.Domain.SliderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext context;

        public SlideRepository(ShopContext context):base(context)
        {
            this.context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return context.Slides.Select(x=>new EditSlide
            {
                Id=x.Id,
                BtnText=x.BtnText,
                Heading=x.Heading,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Text=x.Text,
                Title=x.Title
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<SlideViewModel> GetList()
        {
            return context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Heading = x.Heading,
                Title = x.Title,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToString()
            }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
