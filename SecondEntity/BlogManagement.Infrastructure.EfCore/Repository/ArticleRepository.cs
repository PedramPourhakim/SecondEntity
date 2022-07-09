using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext context;
        public ArticleRepository(BlogContext context):base(context)
        {
            this.context = context;
        }
        public EditArticle GetDetails(long id)
        {
            return context.Articles.Select(x=>new EditArticle
            {
                Id=x.Id,
                CanonicalAddress=x.CanonicalAddress,
                CategoryId=x.CategoryId,
                Description=x.Description,
                Keywords=x.Keywords,
                MetaDescription=x.MetaDescription,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                PublishDate=x.PublishDate.ToFarsi(),
                ShortDescription=x.ShortDescription,
                Slug=x.Slug,
                Title=x.Title
            }).FirstOrDefault(x=>x.Id==id);
        }

        public Article GetWithCategory(long id)
        {
            return context.Articles.Include(x=>
            x.Category).FirstOrDefault(x=>x.Id==id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel
            searchModel)
        {
            var query = context.Articles.Select(x =>
              new ArticleViewModel
              {
                  Id = x.Id,
                  Category = x.Category.Name,
                  CategoryId=x.Category.Id,
                  Picture = x.Picture,
                  PublishDate = x.PublishDate.ToFarsi(),
                  ShortDescription=x.ShortDescription,
                  Title=x.Title,
              });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains
                (searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x=>x.CategoryId
                ==searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
