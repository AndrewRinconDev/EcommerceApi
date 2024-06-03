using AutoMapper;
using EcommerceApi.Controllers.Contracts;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            U? entity = await _baseService.GetById(new Guid(id));

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<U, T>(entity));
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost()]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<T>> SaveEntity([FromBody] T entity)
    {
        try
        {
            U entitySaved = await _baseService.Save(_mapper.Map<T, U>(entity));
            return Ok(_mapper.Map<U, T>(entitySaved));
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<T>> UpdateEntity(string id, [FromBody] T entity)
    {
        try
        {
            U entitySaved = await _baseService.Update(_mapper.Map<T, U>(entity));

            return Ok(_mapper.Map<U, T>(entitySaved));
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!await _baseService.Exist(new Guid(id))) return NotFound();

            Console.Error.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    [Authorize("BasicWrite")]
    public async Task<ActionResult<bool>> DeleteEntity(string id)
    {
        try
        {
        return Ok(await _baseService.DeleteById(new Guid(id)));
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return BadRequest();
        }
    }
}
