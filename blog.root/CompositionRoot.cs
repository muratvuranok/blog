namespace blog.root
{
    using Microsoft.Extensions.DependencyInjection;
    using business.repositories;
    using business.services;
    using data.context;
    using Microsoft.EntityFrameworkCore;
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<blogcontext>();
            services.AddScoped(typeof(IPostRepository), typeof(PostService));
            services.AddScoped(typeof(IPostImageRepository), typeof(PostImageService));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryService));
            services.AddDbContext<blogcontext>(options =>
            options.UseSqlServer("server=94.73.170.5;uid=u8924044_blog;database=u8924044_blog; pwd=EGyz66W3RBcx36U", x => x.MigrationsAssembly("blog.data")));
        }
    }
}