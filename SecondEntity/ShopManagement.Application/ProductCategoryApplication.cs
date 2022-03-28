using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var Operation = new OperationResult();
            if (productCategoryRepository.Exists(x=>x.Name==command.Name))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var productcategory = new ProductCategory(command.Name,
                command.Description, command.Picture
                , command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription,
                slug);
            productCategoryRepository.Create(productcategory);
            productCategoryRepository.SaveChanges();
            return Operation.Succeeded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productcategory = productCategoryRepository.Get(command.Id);
            if (productcategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("یک رکورد دیگر با همین اسم ولی با آیدی دیگر وجود دارد پس لطفا مجدد تلاش کنید");
            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name,command.Description,
                command.Picture,command.PictureAlt,
                command.PictureTitle,command.Keywords,
                command.MetaDescription,slug);
            productCategoryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditProductCategory GetDetails(long id)
        {
            return productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
           return productCategoryRepository.Search(searchModel);
        }
    }
}
