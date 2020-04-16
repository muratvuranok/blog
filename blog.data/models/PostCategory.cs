namespace blog.data.models
{
    using System;
    public class PostCategory
    {
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}