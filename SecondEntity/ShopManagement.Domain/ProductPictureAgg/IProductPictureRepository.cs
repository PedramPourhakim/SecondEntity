using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository
        <long,ProductPicture>
    {
        ProductPicture GetWithProductAndCategory(long id);
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
