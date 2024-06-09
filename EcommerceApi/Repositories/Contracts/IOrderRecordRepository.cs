using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IOrderRecordRepository : IBaseRepository<OrderRecord>
    {
        public Task<IEnumerable<OrderRecord>> GetByOrder(Guid orderId);
    }
}
