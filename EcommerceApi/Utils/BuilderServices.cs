using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Repositories;
using EcommerceApi.Services.Contracts;
using EcommerceApi.Services;
using EcommerceApi.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EcommerceApi.Utils
{
    public class BuilderServices
    {
        public static void AddInitialConfig(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            AddSwaggerConfiguration(builder.Services);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<EcommerceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });

            AddAuthorizationRoles(builder.Services);
            AddEntityFrameworkRepositories(builder.Services);
            AddServices(builder.Services);
        }

        private static void AddAuthorizationRoles(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicRead",
                    policy => policy.RequireClaim("permissions", "super:admin", "admin:read", "basic:read", "visualize:read"));

                options.AddPolicy("BasicWrite",
                    policy => policy.RequireClaim("permissions", "super:admin", "admin:write", "basic:write", "visualize:write"));

                options.AddPolicy("AdminWrite",
                    policy => policy.RequireClaim("permissions", "super:admin", "admin:write"));

                options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("permissions", "super:admin"));
            });
        }

        private static void AddEntityFrameworkRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            services.AddScoped<IFeatureCategoryRepository, FeatureCategoryRepository>();
            services.AddScoped<IFeatureProductRepository, FeatureProductRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IOrderRecordRepository, OrderRecordRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFavoriteProductService, FavoriteProductService>();
            services.AddScoped<IFeatureCategoryService, FeatureCategoryService>();
            services.AddScoped<IFeatureProductService, FeatureProductService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderProductService, OrderProductService>();
            services.AddScoped<IOrderRecordService, OrderRecordService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });
        }
    }
}
