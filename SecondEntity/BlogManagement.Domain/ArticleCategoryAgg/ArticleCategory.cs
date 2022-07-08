using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory :EntityBase
    {
        public string Name { get;private set; }
        public string Picture { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }

        public ArticleCategory(string name, 
            string picture, string description,
            int showOrder, string slug,
            string keywords, string metaDescription,
            string canonicalAddress)
        {
            Name = name;
            Picture = picture;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
        public void Edit(string name,
            string picture, string description,
            int showOrder, string slug,
            string keywords, string metaDescription,
            string canonicalAddress)
        {
            Name = name;

            if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;

            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
    }
}
