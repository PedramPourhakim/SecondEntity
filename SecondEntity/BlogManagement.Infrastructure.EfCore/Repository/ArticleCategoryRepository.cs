using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext context;
        public ArticleCategoryRepository(BlogContext context):base(context)
        {
            this.context = context;
        }
        public EditArticleCategory GetDetails(long id)
        {
            return context.ArticleCategories.Select(
                x=>new EditArticleCategory
                { 
                    Id=x.Id,
                    Name=x.Name,
                    CanonicalAddress=x.CanonicalAddress,
                    Description=x.Description,
                    PictureAlt=x.PictureAlt,
                    PictureTitle=x.PictureTitle,
                    Keywords=x.Keywords,
                    MetaDescription=x.MetaDescription,
                    ShowOrder=x.ShowOrder,
                    Slug=x.Slug
                }).FirstOrDefault
                (x=>x.Id==id);
        }

        public string GetSlugBy(long id)
        {
            return context.ArticleCategories.Select
                (x=>new {x.Id,x.Slug })
                .FirstOrDefault(x=>x.Id==id).Slug;
        }

        public List<ArticleCategoryViewModel> Search
            (ArticleCategorySearchModel searchModel)
        {
            var query = context.ArticleCategories.Select
                (x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Picture = x.Picture,
                    ShowOrder = x.ShowOrder,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains
                  (searchModel.Name));
           return query.OrderByDescending(x=>x.ShowOrder).ToList();
        }
    }
}
