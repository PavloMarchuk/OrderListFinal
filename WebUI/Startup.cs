using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Data;
using WebUI.Models;
using WebUI.Services;
using DateLayer.Models;
using EFCoreGenericRepository.Common;
using DateLayer.Repositories;
using WebUI.Models.BusinessViewModels;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace WebUI
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
			services.Configure<WebEncoderOptions>(options => {options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);	});
			//підключення авторизації
			services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("KyivgazIdentity")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

			services.AddDistributedMemoryCache();
			services.AddSession();

			//додаємо посилання на провайдера сервера баз даних і додаємо строку підключення
			services.AddDbContext<KyivgazDBContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("KyivgazDB")));

			//використовуємо встроєний контейнер створення залежностей
			services.AddScoped<DbContext, KyivgazDBContext>();
			services.AddTransient<IGenericRepository<Invoice>, InvoiceRepository>();
			services.AddTransient<IGenericRepository<Manager>, ManagerRepository>();


			//ManagerViewModel

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
			app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=About}/{id?}");
				//template: "{controller=Invoices}/{action=Filter}/{id?}");
		});
        }
    }
}


