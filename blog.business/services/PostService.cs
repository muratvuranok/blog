namespace blog.business.services
{
    using repositories;
    using data.models;
    using data.context;
    public class PostService : Repository<Post>, IPostRepository
    {
        public PostService(blogcontext context) : base(context) { } 
    }
}