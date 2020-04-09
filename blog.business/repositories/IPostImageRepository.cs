namespace blog.business.repositories
{
    using System;
    using data.models;
    public interface IPostImageRepository : IRepository<PostImage>
    { 
        void SetFalse(Guid id);
    }
}