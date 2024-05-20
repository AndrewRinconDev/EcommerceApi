using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Repositories;
using EcommerceApi.Services.Contracts;
using EcommerceApi.Services;
using EcommerceApi.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EcommerceApi.Utils
{
    public class BuilderServices
    {
        public static void AddInitialConfig(WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EcommerceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            AddEntityFrameworkRepositories(builder.Services);
            AddServices(builder.Services);
        }

        private static void AddEntityFrameworkRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBaseRepository<Address>, AddressRepository>();
            services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
            services.AddScoped<IBaseRepository<Customer>, CustomerRepository>();
            services.AddScoped<IBaseRepository<FavoriteProduct>, FavoriteProductRepository>();
            services.AddScoped<IBaseRepository<Feature>, FeatureRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFavoriteProductService, FavoriteProductService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
