using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Domain.ProductAgg;
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

        public ProductApplication(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var Operation = new OperationResult();
            if (productRepository.Exists(x => x.Name == command.Name))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.Code, 
                command.UnitPrice, command.ShortDescription,
                command.Description,command.Picture,
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
            var product = productRepository.Get(command.Id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            if (productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return Operation.Failed(" .یک رکورد دیگر با همین اسم ولی با یک آیدی دیگر وجود دارد لطفا از درج رکورد تکراری خودداری فرمایید");
            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code,
                command.UnitPrice, command.ShortDescription,
                command.Description, command.Picture,
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

        public OperationResult InStock(long id)
        {
            var Operation = new OperationResult();
            var product = productRepository.Get(id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            product.InStock();
            productRepository.SaveChanges();
            return Operation.Succeeded();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            product.NotInStock();
            productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductViewModel> SearchModel(ProductSearchModel searchModel)
        {
           return productRepository.Search(searchModel);
        }
    }
}
