using _0_Framework.Domain;
using ShopManagement.Application.Contracts.SlideContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.SliderAgg
{
    public interface ISlideRepository :IRepository<long,Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();

    }
}
