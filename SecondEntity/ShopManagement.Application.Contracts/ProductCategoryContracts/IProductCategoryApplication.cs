using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategoryContracts
{
    public interface IProductCategoryApplication
    {
        public OperationResult Create(CreateProductCategory command);
        public OperationResult Edit(EditProductCategory command);
        public EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        List<ProductCategoryViewModel> Search(ArticleCategoriesSearchmodel searchModel);
    }
}
