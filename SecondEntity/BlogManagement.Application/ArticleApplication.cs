using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader fileUploader;
        private readonly IArticleRepository articleRepository;
        private readonly IArticleCategoryRepository articleCategoryRepository;
        public ArticleApplication(IArticleRepository 
            articleRepository,IFileUploader fileUploader
            ,IArticleCategoryRepository 
            articleCategoryRepository)
        {
            this.fileUploader = fileUploader;
            this.articleRepository = articleRepository;
            this.articleCategoryRepository = articleCategoryRepository;
        }
        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var CategorySlug =
                articleCategoryRepository.GetSlugBy
                (command.CategoryId);
           var slug=command.Slug.Slugify();
            var path = $"{CategorySlug}/{slug}";
            var PictureName=fileUploader.Upload(command.Picture,path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title,
                command.ShortDescription,command.Description,
                PictureName,command.PictureTitle,
                command.PictureAlt,publishDate,
                slug,command.Keywords,command.MetaDescription,
                command.CanonicalAddress,command.CategoryId);

            articleRepository.Create(article);
            articleRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = articleRepository.GetWithCategory
                (command.Id);
            if (article == null)
                return operation.Failed(ApplicationMessages
                    .RecordNotFound);

            if (articleRepository.Exists(x => x.Title ==
            command.Title && x.Id !=command.Id))
                return operation.Failed
                    (ApplicationMessages.
                    DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";
            var PictureName = fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title,
                command.ShortDescription, command.Description,
                PictureName, command.PictureTitle,
                command.PictureAlt, publishDate,
                slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.CategoryId);

            articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            return articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return articleRepository.Search(searchModel);
        }
    }
}
