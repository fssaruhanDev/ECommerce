using System;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Infrastructure.Persistence.Context;
using ECommerce.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Persistence.Extensions;

public static class Registration
{
	public static IServiceCollection addInfastructureRegistration(this IServiceCollection services,IConfiguration configuration)
	{

		services.AddDbContext<EntityContext>(conf =>
		{

			var connectionString = configuration["ConnectionStrings"];

			conf.UseSqlServer(connectionString, x =>
			{
				x.EnableRetryOnFailure();
			});

		});

        //var SeedData = new SeedData();

        //SeedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
	}
}

