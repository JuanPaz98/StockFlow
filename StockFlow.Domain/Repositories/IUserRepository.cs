using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        void Remove(UserEntity user);
        void Update(UserEntity user);
    }
}
