
using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Api.Services;
using RecruitmentTask.Data.DbContexts;

namespace RecruitmentTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // EF DbContext service
            builder.Services.AddDbContext<RecruitmentTaskDbContext>(options => 
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            // AutoMapper service (default config profile for the entire app)
            builder.Services.AddAutoMapper(typeof(MappingProfiles.DefaultMappingProfile));

            // Business logic services
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddScoped<IExternalFilesService>(s =>
                new ExternalFilesService(
                    new HttpClient()
                    {
                        BaseAddress = new Uri(builder.Configuration["ExternalFilesSettings:BaseUrl"] ?? ""),
                    }));


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
