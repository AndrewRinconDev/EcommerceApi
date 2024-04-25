﻿namespace EcommerceApi.Repositories.Contracts
{
    public interface IBaseRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task DeleteById(Guid id);

        public Task<T> Save(T entity);

        public Task<T> Update(T entity);
    }
}
