using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers.Contracts;
public interface IBaseController<T> where T : class
{
    public Task<ActionResult<IEnumerable<T>>> GetAll();

    public Task<ActionResult<T>> GetById(string id);

    public Task<ActionResult<T>> SaveEntity(T entity);

    public Task<ActionResult<T>> UpdateEntity(string id, T entity);
}

