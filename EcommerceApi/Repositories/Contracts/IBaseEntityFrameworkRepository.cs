using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IBaseEntityFrameworkRepository<T> : IBaseRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task DeleteById(Guid id);

        public Task<T> Save(T entity);

        public Task<T> Update(T entity);

        public void DetachAllEntities();
    }

}
