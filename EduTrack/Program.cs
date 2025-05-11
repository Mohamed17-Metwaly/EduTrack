using EduTrack.DataAccess;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EduTrackContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("EduTrack.DataAccess")));
            builder.Services.AddScoped<IStudentRepository,StudentRepository>();
            builder.Services.AddScoped<ISemesterRepository,SemesterRepository>();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<ICourseRepository,CourseRepository>();
            builder.Services.AddScoped<IEnrollmentRepository,EnrollmentRepository>();
            builder.Services.AddControllers()
                              .AddJsonOptions(options =>
                              {
                                  options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                              });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
