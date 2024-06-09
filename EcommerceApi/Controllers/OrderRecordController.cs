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
    public class OrderRecordController : BaseController<OrderRecord, OrderRecordDto>
    {
        private readonly IOrderRecordService _orderRecordService;
        private readonly IMapper _mapper;

        public OrderRecordController(IMapper mapper, IOrderRecordService baseService) : base(mapper, baseService) {
            _orderRecordService = baseService;
            _mapper = mapper;
        }

        [HttpGet("Order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderRecordDto>>> GetByOrder(Guid orderId)
        {
            try
            {
                var orderRecords = await _orderRecordService.GetByOrder(orderId);
                return Ok(_mapper.Map<IEnumerable<OrderRecordDto>>(orderRecords));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

    }
}
