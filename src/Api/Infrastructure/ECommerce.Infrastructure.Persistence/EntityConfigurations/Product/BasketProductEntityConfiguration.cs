using Azure;
using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.EntityConfigurations.Product;

public class BasketProductEntityConfiguration : BaseEntityConfiguration<BasketProduct>
{
    public override void Configure(EntityTypeBuilder<BasketProduct> builder)
    {
        base.Configure(builder);

        builder.ToTable("basketproduct", EntityContext.DEFAULT_SCHEMA);

        builder.HasOne(BasketProduct => BasketProduct.Product)
              .WithMany(Product => Product.BasketProducts)
              .HasForeignKey(BasketProduct => BasketProduct.ProductID);

        builder.HasOne(BasketProduct => BasketProduct.Basket)
                .WithMany(Basket => Basket.BasketProducts)
                .HasForeignKey(BasketProduct => BasketProduct.BasketID);
    }
  
}
