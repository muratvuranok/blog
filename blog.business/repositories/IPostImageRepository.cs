
namespace blog.business.repositories
{ 
    using data.models;
    using System.Collections.Generic; 
    public interface IPostImageRepository : IRepository<PostImage>
    {
        void SetFalse(IEnumerable<PostImage> images);
    }
}