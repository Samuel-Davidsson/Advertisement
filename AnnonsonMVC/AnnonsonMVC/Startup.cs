using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data.DataContext;
using Domain.Services;
using Domain.Interfaces;
using Domain.Entites;
using Data.Repositories;


namespace AnnonsonMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
   
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IStoreArticleService, StoreArticleService>();

            services.AddScoped<IRepository<Article>, Repository<Article>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Store>, Repository<Store>>();
            services.AddScoped<IRepository<Company>, Repository<Company>>();
            services.AddScoped<IRepository<StoreArticle>, Repository<StoreArticle>>();

            //var connection = @"Server=DESKTOP-M702LBS;Database=annonsappen;Trusted_Connection=True;";
            var connection = @"Server=SAMUEL;Database=annonsappen;Trusted_Connection=True;";
            services.AddDbContext<annonsappenContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Articles}/{action=Index}/{id?}");
            });
        }
    }
}
