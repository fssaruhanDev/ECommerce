﻿using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.EntityConfigurations.Product;

public class ProductEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Product>
{

    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Product> builder)
    {
        base.Configure(builder);

        builder.ToTable("product", EntityContext.DEFAULT_SCHEMA);
    }

}
