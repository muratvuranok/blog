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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>()
                .HasKey(bc => new { bc.PostId, bc.CategoryId });


            modelBuilder.Entity<PostCategory>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.PostCategories)
                .HasForeignKey(bc => bc.PostId);

                
            modelBuilder.Entity<PostCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.PostCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
 
