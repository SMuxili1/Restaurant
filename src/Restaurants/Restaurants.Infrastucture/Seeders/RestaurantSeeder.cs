using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants =
            [
                new()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Fundado em 1976",
                    ContactNumber = "+244911222333",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes =
                    [
                        new()
                        {
                            Name = "Frango grelhado fumado",
                            Description = "Galinha do mato",
                            Price = 1000.99M,
                        },

                        new()
                        {
                            Name = "Frango frito",
                            Description = "Galinha Isa Brown",
                            Price = 7000.67M,
                        },
                    ],
                    Address = new()
                    {
                        City = "Luanda",
                        Street = "Vila de Viana",
                        PostalCode = "244"
                    }
                }
            ];

            return restaurants;
        }
    }
}
