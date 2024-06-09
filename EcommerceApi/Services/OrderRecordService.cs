using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderRecordService : BaseService<OrderRecord>, IOrderRecordService
    {
        IOrderRecordRepository _OrderRecordRepository;
        public OrderRecordService(IOrderRecordRepository repository) : base(repository) {
            _OrderRecordRepository = repository;
        }

        public async Task<IEnumerable<OrderRecord>> GetByOrder(Guid orderId)
        {
            return await _OrderRecordRepository.GetByOrder(orderId);
        }
    }
}
