namespace blog.business.services
{
    using repositories;
    using data.models;
    using data.context;

    public class CategoryService : Repository<Category>, ICategoryRepository
    {
        public CategoryService(blogcontext context) : base(context) { }

    }
}