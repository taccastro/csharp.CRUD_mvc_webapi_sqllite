using AnuncioWeb.Contracts;
using AnuncioWeb.Database;
using AnuncioWeb.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AnuncioWeb.Helpers;

namespace AnuncioWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region AutoMapper-Configuracao
            var config = new MapperConfiguration(cfg => {

                cfg.AddProfile(new DTOMapperProfile());         
            
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion  

            services.AddDbContext<AnuncioContext>(opt => {
                opt.UseSqlite("Data Source=Database\\tb_AnuncioWebmotors.db");

            });

            services.AddMvc();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
