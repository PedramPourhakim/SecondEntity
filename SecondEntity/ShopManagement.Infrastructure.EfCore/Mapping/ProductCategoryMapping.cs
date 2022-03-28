using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Name).HasColumnName("نام گروه کالا").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1500).HasColumnName("توضیحات");
            builder.Property(x => x.Picture).HasColumnName("مسیرعکس").HasMaxLength(255);
            builder.Property(x => x.PictureAlt).HasColumnName("Alt عکس").HasMaxLength(255);
            builder.Property(x => x.PictureTitle).HasColumnName("عنوان عکس").HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(1500).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();
            builder.HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

        }
    }
}
