using CadastroProdutos.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CadastroProdutos.API
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
            var connectionStringMySQL = Configuration.GetConnectionString("MySQLConnection");

            services.AddDbContext<DataContext>(options =>
                options.UseMySql(connectionStringMySQL, ServerVersion.AutoDetect(connectionStringMySQL))
            );
            services.AddControllers()
                    .AddNewtonsoftJson(options => 
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Adiciona o middleware para redirecionar para https
            app.UseHttpsRedirection();

            // Adiciona o middleware de roteamento
            app.UseRouting();

            // Adiciona o middleware que habilita a autoriazação
            app.UseAuthorization();

            // Adiciona o middleware que executa o endpoint do request atual
            app.UseEndpoints(endpoints =>
            {
                // Adiciona os endpoints para as Actions dos controladores sem especificar rotas
                endpoints.MapControllers();
            });
        }
    }
}
