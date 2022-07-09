using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository articleCategoryRepository;
        private readonly IFileUploader fileUploader;
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository,IFileUploader fileUploader)
        {
            this.articleCategoryRepository = articleCategoryRepository;
            this.fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var Operation = new OperationResult();
            if (articleCategoryRepository.Exists(x => x.Name == command.Name))
                return Operation.Failed
                    (ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var PictureName=fileUploader.Upload(command.Picture, slug);

            var ArticleCategory = new ArticleCategory(command.Name,
                PictureName,command.PictureAlt
                ,command.PictureTitle,command.Description,
                command.ShowOrder,slug,
                command.Keywords,command.MetaDescription,
                command.CanonicalAddress);
            articleCategoryRepository.Create(ArticleCategory);
            articleCategoryRepository.SaveChanges();
            return Operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var Operation = new OperationResult();
            var articleCategory = 
                articleCategoryRepository
                .Get(command.Id);

            if (articleCategory == null)
                return Operation.Failed
                    (ApplicationMessages.RecordNotFound);

            if (articleCategoryRepository.Exists
                (x => x.Name == command.Name &&
                x.Id !=command.Id))
                return Operation.Failed
                    (ApplicationMessages.
                    DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var PictureName = fileUploader
                .Upload(command.Picture, slug);

            articleCategory.Edit(command.Name,
                PictureName,command.PictureAlt,
                command.PictureTitle
                ,command.Description,
                command.ShowOrder, slug,
                command.Keywords, command.MetaDescription,
                command.CanonicalAddress);

            articleCategoryRepository.SaveChanges();
            return Operation.Succeeded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return articleCategoryRepository.GetDetails(id);
        }

      

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return articleCategoryRepository.Search(
                searchModel);
        }
    }
}
