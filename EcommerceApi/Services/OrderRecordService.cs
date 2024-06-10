using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderRecordService : BaseService<OrderRecord>, IOrderRecordService
    {
        IOrderRecordRepository _orderRecordRepository;

        public OrderRecordService(IOrderRecordRepository repository) : base(repository) {
            _orderRecordRepository = repository;
        }

        public async Task<IEnumerable<OrderRecord>> GetByOrder(Guid orderId)
        {
            return await _orderRecordRepository.GetByOrder(orderId);
        }
    }
}
