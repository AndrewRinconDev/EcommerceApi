using AutoMapper;
using EcommerceApi.Controllers.Contracts;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers;

[ApiController]
[Authorize]
public class BaseController<U, T> : ControllerBase, IBaseController<T> where U : class where T : class
{
    protected readonly IMapper _mapper;
    protected readonly IBaseService<U> _baseService;

    public BaseController(IMapper mapper, IBaseService<U> baseService)
    {
        _mapper = mapper;
        _baseService = baseService;
    }

    [HttpGet()]
    [Authorize("BasicRead")]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        IEnumerable<U> list = await _baseService.GetAll();
        return Ok(_mapper.Map<IEnumerable<U>, IEnumerable<T>>(list));
    }

    [HttpGet("{id}")]
    [Authorize("BasicRead")]
    public async Task<ActionResult<T>> GetById(string id)
    {
        U entity = await _baseService.GetById(new Guid(id));
        return Ok(_mapper.Map<U, T>(entity));
    }

    [HttpPost()]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<T>> SaveEntity([FromBody] T entity)
    {
        U entitySaved = await _baseService.Save(_mapper.Map<T, U>(entity));
        return Ok(_mapper.Map<U, T>(entitySaved));
    }

    [HttpPut()]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<T>> UpdateEntity([FromBody] T entity)
    {
        U entitySaved = await _baseService.Update(_mapper.Map<T, U>(entity));
        return Ok(_mapper.Map<U, T>(entitySaved));
    }

    [HttpDelete("{id}")]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<bool>> DeleteEntity(string id)
    {
        return Ok(await _baseService.DeleteById(new Guid(id)));
    }
}
