using _0_Framework.Application;
using _01_Query.Contract.Article;
using _01_Query.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext context;
        public ArticleCategoryQuery(BlogContext context)
        {
            this.context = context;
        }
        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return context.ArticleCategories.Include
                (x => x.Articles).Select(
                x=>new ArticleCategoryQueryModel
                {
                    Name=x.Name,
                    Picture=x.Picture,
                    PictureAlt=x.PictureAlt,
                    PictureTitle=x.PictureTitle,
                    Slug=x.Slug,
                    ArticlesCount=x.Articles.Count
                }
                ).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var articleCategory= context.ArticleCategories
                .Include(x=>x.Articles)
                .Select
                (x=>new ArticleCategoryQueryModel 
                {
                    Slug=x.Slug,
                    Name=x.Name,
                    Description=x.Description,
                    Picture=x.Picture,
                    PictureAlt=x.PictureAlt,
                    PictureTitle=x.PictureTitle,
                    Keywords=x.Keywords,
                    MetaDescription=x.MetaDescription,
                    CanonicalAddress=x.CanonicalAddress,
                    ArticlesCount=x.Articles.Count,
                    Articles=MapArticles(x.Articles)
                }).FirstOrDefault(x=>x.Slug==slug);
            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                articleCategory.KeywordList = articleCategory.Keywords.Split(",").ToList();
            return articleCategory;
        }

        public static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x=>new
            ArticleQueryModel
            {
                Slug=x.Slug,
                ShortDescription=x.ShortDescription,
                Title=x.Title,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                PublishDate=x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
