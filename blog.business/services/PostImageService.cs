namespace blog.business.services
{
    using repositories;
    using data.models;
    using data.context;
    using System;
    public class PostImageService : Repository<PostImage>, IPostImageRepository
    {
        public PostImageService(blogcontext context) : base(context) { }

        public void SetFalse(Guid id)
        {

        }
    }
}