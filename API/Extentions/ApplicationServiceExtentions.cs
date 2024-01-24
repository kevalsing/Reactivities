using System.Reflection;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class ApplicationExtentions
    {
        public static IServiceCollection AddApplicationservices(this  IServiceCollection services,
                                                             IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<Persistence.DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(Application.ListQueryHandler).Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(Application.Activities.Edit).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            
            return services; 

        }


    }
}