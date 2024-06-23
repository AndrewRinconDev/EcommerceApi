using EcommerceApi.Models.Database;
using EcommerceApi.Models.Reference;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductService _orderProductService;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        public OrderService(IOrderRepository repository, IOrderProductService orderProductService, IOrderHistoryRepository orderHistoryRepository) : base(repository) {
            _orderRepository = repository;
            _orderProductService = orderProductService;
            _orderHistoryRepository = orderHistoryRepository;
        }

        public async Task<IEnumerable<Order>> GetAllActive()
        {
            return await _orderRepository.GetAllActive();
        }

        public async Task<Order> GetByIdActive(Guid id)
        {
            return await _orderRepository.GetByIdActive(id);
        }

        public async Task<IEnumerable<Order>> GetByCustomer(Guid customerId)
        {
            return await _orderRepository.GetByCustomer(customerId);
        }

        public async Task<Order> SaveOrder(Order order)
        {
            var currentDate = DateTime.Now;
            order.creationOn = order.updateOn = currentDate;
            order.isActive = true;

            var orderSaved = await _orderRepository.Save(order);

            if (orderSaved != null)
            {
                await _orderProductService.SaveAllProducts(orderSaved);

                await _orderHistoryRepository.Save(new OrderHistory
                {
                    orderId = orderSaved.id,
                    date = currentDate,
                    orderState = OrderStatusType.Createted,
                    detail = "Order created"
                });
            }
            return orderSaved;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            order.updateOn = DateTime.Now;
            var orderUpdated = await _orderRepository.Update(order);
            // TODO Save order records
            return orderUpdated;
        }

        public async Task<Order> DeleteOrder(Guid Id)
        {
            var order = await _orderRepository.GetById(Id);
            if (order == null) throw new Exception("Order not found");

            order.isActive = false;
            order.updateOn = DateTime.Now;
            return await _orderRepository.Update(order);
        }
    }
}
