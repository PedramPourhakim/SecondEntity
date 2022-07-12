using _0_Framework.Application;
using _01_Query.Contract.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext context;

        public ArticleQuery(BlogContext context)
        {
            this.context = context;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article= context.Articles.Include(x => x.Category)
                   .Where(x => x.PublishDate <= DateTime.Now)
                   .Select
               (x => new ArticleQueryModel
               {
                   CategoryName = x.Category.Name,
                   CategorySlug = x.Category.Slug,
                   Slug = x.Slug,
                   CanonicalAddress = x.CanonicalAddress,
                   Description = x.Description,
                   Keywords = x.Keywords,
                   MetaDescription = x.MetaDescription,
                   Picture = x.Picture,
                   PictureAlt = x.PictureAlt,
                   PictureTitle = x.PictureTitle,
                   PublishDate = x.PublishDate.ToFarsi(),
                   ShortDescription = x.ShortDescription,
                   Title = x.Title
               }).FirstOrDefault(x => x.Slug == slug);
            if(!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();
            return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return context.Articles.Include(x=>x.Category)
                .Where(x =>x.PublishDate <= DateTime.Now)
                .Select
            (x=>new ArticleQueryModel 
            { 
                Slug=x.Slug,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                PublishDate=x.PublishDate.ToFarsi(),
                ShortDescription=x.ShortDescription,
                Title=x.Title
            }).ToList();
        }
    }
}
