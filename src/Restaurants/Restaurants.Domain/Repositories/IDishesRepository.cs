using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        //Task<IEnumerable<Dish>> GetAllAsync();
        //Task<Dish?> GetByIdAsync(int id);
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
        //Task Delete(Dish entity);
        //Task SaveChanges();
    }
}
