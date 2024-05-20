using EcommerceApi.Services.Contracts;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;

namespace EcommerceApi.Services
{
    public class BaseService<T> : IBaseService<T> where T : BDEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public Task<T> Save(T entity)
        {
            return _repository.Save(entity);
        }

        public Task<T> SaveUpdate(T entity)
        {
            if (entity.id == null || entity.id == Guid.Empty)
            {
                return Save(entity);
            }
            else
            {
                return Update(entity);
            }
        }

        public async Task<T> Update(T entity)
        {
            await _repository.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            return await _repository.DeleteById(id);
        }

        public void DetachEntities()
        {
            _repository.DetachAllEntities();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _repository.Exist(id);
        }
    }
}
