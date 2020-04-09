namespace blog.data.context
{
    using models;
    using Microsoft.EntityFrameworkCore;
    public class blogcontext : DbContext
    {
        public blogcontext(DbContextOptions<blogcontext> options)
        : base(options) { }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostImage> PostImages { get; set; }
    }
}