using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCS3.BLL.MappingProfiles;
using MVCS3.BLL.Services.AttachementService;
using MVCS3.BLL.Services.Classes;
using MVCS3.BLL.Services.Interfaces;
using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models.IdentityModels;
using MVCS3.DAL.Repositories.Classes;
using MVCS3.DAL.Repositories.Interfaces;

namespace MVCS3PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services Add services to the DI container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //var conString = builder.Configuration["ConnectionStrings: DefaultConnection"];
                //var conString = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
                var conString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(conString).UseLazyLoadingProxies();
            });

            //builder.Services.AddScoped<IDeprtmentRepository, DeprtmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachementService, AttachementService>();

            //builder.Services.AddAutoMapper(typeof(ProjectReference).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
				//options.User. RequireUnique Email = true;
				//options.Password. RequireLowercase = true;
				//options.Password. RequireUppercase = true
			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
				//pattern: "{controller=Home}/{action=Index}/{id?}");
				pattern: "{controller=Account}/{action=Register}/{id?}");

			app.Run();
        }
    }
}
