using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.EntityConfigurations.Product;

public class CartItemEntityConfiguration : BaseEntityConfiguration<CartItem>
{

    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        
        builder.ToTable("cartitem", EntityContext.DEFAULT_SCHEMA);

        builder
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductID);

        builder
            .HasOne(ci => ci.ShoppingCart)
            .WithMany(sc => sc.CartItems)
            .HasForeignKey(ci => ci.ShoppingCartID)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
