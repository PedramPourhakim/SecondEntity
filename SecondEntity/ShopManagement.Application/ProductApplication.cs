using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository productRepository;
        private readonly IFileUploader fileUploader;
        private readonly IProductCategoryRepository productCategoryRepository;
        public ProductApplication(IProductRepository productRepository,IFileUploader fileUploader,IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.productRepository = productRepository;
            this.fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var Operation = new OperationResult();
            if (productRepository.Exists(x => x.Name == command.Name))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var categorySlug = productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}//{slug}";
            var picturePath = fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name, command.Code, 
                 command.ShortDescription,
                command.Description,picturePath,
                command.PictureAlt, command.PictureTitle,
                command.CategoryId,slug,
                command.MetaDescription,
                command.Keywords);
            productRepository.Create(product);
            productRepository.SaveChanges();
            return Operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var Operation = new OperationResult();
            var product = productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            if (productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return Operation.Failed(" .یک رکورد دیگر با همین اسم ولی با یک آیدی دیگر وجود دارد لطفا از درج رکورد تکراری خودداری فرمایید");
            var slug = command.Slug.Slugify();
            var categorySlug = productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{product.Category.Slug}/{slug}";
            var picturePath = fileUploader.Upload(command.Picture, path);
            product.Edit(command.Name, command.Code,
                command.ShortDescription,
                command.Description, picturePath,
                command.PictureAlt, command.PictureTitle,
                command.CategoryId, slug,
                command.MetaDescription,
                command.Keywords);
            productRepository.SaveChanges();
            return Operation.Succeeded();

        }

        public EditProduct GetDetails(long id)
        {
            return productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return productRepository.GetProducts();
        }
        public List<ProductViewModel> SearchModel(ProductSearchModel searchModel)
        {
           return productRepository.Search(searchModel);
        }
    }
}
