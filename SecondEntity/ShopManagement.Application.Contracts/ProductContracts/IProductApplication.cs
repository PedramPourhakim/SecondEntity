using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult InStock(long id);
        OperationResult NotInStock(long id);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        List<ProductViewModel> SearchModel(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();

    }
}
