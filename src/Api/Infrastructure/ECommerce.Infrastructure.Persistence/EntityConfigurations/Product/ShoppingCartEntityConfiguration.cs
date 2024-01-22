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

public class ShoppingCartEntityConfiguration : BaseEntityConfiguration<ShoppingCart>
{

    public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        base.Configure(builder);

        builder.ToTable("shoppingcart", EntityContext.DEFAULT_SCHEMA);


        builder
            .HasOne(sc => sc.User)
            .WithOne(c => c.ShoppingCart)
            .HasForeignKey<ShoppingCart>(sc => sc.UserID);
    }

}
