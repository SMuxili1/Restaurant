using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastucture.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;
    }
}
