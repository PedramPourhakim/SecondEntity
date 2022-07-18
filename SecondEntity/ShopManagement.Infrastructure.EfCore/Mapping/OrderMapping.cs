using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.IssueTrackingNo)
                .HasMaxLength(8).IsRequired();
            builder.OwnsMany(x => x.Items,navigationBuilder=>
            {
                navigationBuilder.ToTable("OrderItems");
                navigationBuilder.HasKey(x=>x.Id);
                navigationBuilder.WithOwner(x => x.Order)
                .HasForeignKey(x => x.OrderId);
            });
        }
    }
}
