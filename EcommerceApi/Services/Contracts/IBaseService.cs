namespace EcommerceApi.Services.Contracts
{
    public interface IBaseService<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task<T> Save(T entity);

        public Task<T> SaveUpdate(T entity);

        public Task<T> Update(T entity);

        public void DetachEntities();

        public Task<bool> Exist(Guid id);
    }
}
