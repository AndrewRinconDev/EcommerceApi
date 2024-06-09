using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IOrderRecordService : IBaseService<OrderRecord>
    {
        public Task<IEnumerable<OrderRecord>> GetByOrder(Guid orderId);
    }
}
