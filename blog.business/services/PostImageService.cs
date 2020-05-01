namespace blog.business.services
{
    using repositories;
    using data.models;
    using data.context;
    using System.Collections.Generic;

    public class PostImageService : Repository<PostImage>, IPostImageRepository
    {
        public PostImageService(blogcontext context) : base(context) { }

        public void SetFalse(IEnumerable<PostImage> images)
        {
            foreach (PostImage image in images)
            {
                image.Active = false; 
            }
            Save();
        }
    }
}