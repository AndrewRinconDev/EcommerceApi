using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderHistoryController : BaseController<OrderHistory, OrderHistoryDto>
    {
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IMapper _mapper;

        public OrderHistoryController(IMapper mapper, IOrderHistoryService baseService) : base(mapper, baseService) {
            _orderHistoryService = baseService;
            _mapper = mapper;
        }

        [HttpGet("Order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderHistoryDto>>> GetByOrder(Guid orderId)
        {
            try
            {
                var orderHistorys = await _orderHistoryService.GetByOrder(orderId);
                return Ok(_mapper.Map<IEnumerable<OrderHistoryDto>>(orderHistorys));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

    }
}
