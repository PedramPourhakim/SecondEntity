using _0_Framework.Application;
using _01_Query.Contract.Article;
using _01_Query.Contract.Comment;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore;
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
        private readonly CommentContext commentContext;

        public ArticleQuery(BlogContext context,CommentContext commentContext)
        {
            this.context = context;
            this.commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article= context.Articles.Include(x => x.Category)
                   .Where(x => x.PublishDate <= DateTime.Now)
                   .Select
               (x => new ArticleQueryModel
               {
                   Id=x.Id,
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

            var comments =
              commentContext.Comments.Where(x =>
        x.Type == CommentType.Article).Where
        (x => x.OwnerRecordID == article.Id)
        .Where(x => !x.IsCanceled)
        .Where(x => x.IsConfirmed)
        .Select(x => new CommentQueryModel
        {
            Id = x.Id,
            Message = x.Message,
            Name = x.Name,
            CreationDate = x.CreationDate.ToFarsi(),
            ParentID=x.ParentID,
        }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentID > 0)
                    comment.ParentName = comments
                        .FirstOrDefault(x=>x.Id==comment.ParentID)
                        ?.Name;
            }
            article.Comments = comments;

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
