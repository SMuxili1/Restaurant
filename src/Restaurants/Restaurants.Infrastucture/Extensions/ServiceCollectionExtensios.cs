﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Authorization;
using Restaurants.Infrastucture.Authorization.Requirements;
using Restaurants.Infrastucture.Persistence;
using Restaurants.Infrastucture.Repositories;
using Restaurants.Infrastucture.Seeders;

namespace Restaurants.Infrastucture.Extensions
{
    public static class ServiceCollectionExtensios
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "Angola"))
                .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements( new MinimumAgeRequirement(20)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        }
    }
}
