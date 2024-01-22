using System;
using System.Globalization;
using Bogus;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.Persistence.Context;

internal class SeedData
{
	private static List<User> GetUsers()
	{
        var result = new Faker<User>(locale:"tr")
            .RuleFor(i => i.ID, i => Guid.NewGuid())
            .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.Avatar, i => i.Internet.Avatar())
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.UserName, i => i.Internet.UserName())
			.RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt( i.Internet.Password()))
			.RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
			.Generate(100);

		return result;
    }


	public async Task SeedAsync(IConfiguration configuration)
	{
        try
        {

            var Size = new[] { "s", "m", "l", "xl" };

            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["ConnectionStrings"]);
            var context = new EntityContext(dbContextBuilder.Options);
            var users = GetUsers();
            var userIds = users.Select(i => i.ID);
            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
            var result = new Faker<Product>(locale: "tr")
               .RuleFor(i => i.ID, i => Guid.NewGuid())
               .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
               .RuleFor(i => i.Company, i => i.Company.CompanyName())
               .RuleFor(i => i.Picture, i => i.Image.Fashion())
               .RuleFor(i => i.Name, i => i.Commerce.ProductName())
               .RuleFor(i => i.Description, i => i.Commerce.ProductDescription())
               .RuleFor(i => i.Price, i => Convert.ToDouble( i.Commerce.Price(1,1000,2,"")))
               .RuleFor(i => i.Stock, i => i.Commerce.Random.Int(1,100))
               .RuleFor(i => i.Barcode, i => i.Commerce.Ean13())
               .RuleFor(i => i.Size, i => i.PickRandom(Size))
               .Generate(500);


            await context.SaveChangesAsync();
        }
        catch (CultureNotFoundException ex)
        {
            Console.WriteLine($"Hata: {ex.Message}, Invalid Culture Name: {ex.InvalidCultureName}");
        }
       
    }
}

