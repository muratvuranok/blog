namespace blog.api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq; 
    using business.repositories; 

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categortRepository)
        {
            this._categoryRepository = categortRepository;
        }
 
        public object Get()
        {
            return _categoryRepository.GetAll().Select(x => new
            {
                x.Id,
                x.Name
            });
        }

    }
}