using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader fileUploader;
        private readonly IProductPictureRepository productPictureRepository;
        private readonly IProductRepository productRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository,IProductRepository productRepository,IFileUploader fileUploader)
        {
            this.fileUploader = fileUploader;
            this.productRepository = productRepository;
            this.productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            //    return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var product = productRepository.GetProductWithCategory(command.ProductId);

            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath=fileUploader.Upload(command.Picture,path);

            var productpicture = new ProductPicture(command.ProductId, picturePath
                , command.PictureAlt, command.PictureTitle);
            productPictureRepository.Create(productpicture);
            productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            

            var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
            var picturePath = fileUploader.Upload(command.Picture, path);
            productPicture.Edit(command.ProductId, picturePath,
                command.PictureAlt, command.PictureTitle);
            productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            productPicture.Remove();
            productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            productPicture.Restore();
            productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return productPictureRepository.Search(searchModel);
        }
    }
}
