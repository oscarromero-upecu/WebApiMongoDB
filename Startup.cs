using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebAppMongoDBUClinica.Configuraciones;
using WebAppMongoDBUClinica.Datos;
using WebAppMongoDBUClinica.Datos.Interfaces;
using WebAppMongoDBUClinica.Repositorios;

namespace WebAppMongoDBUClinica
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
            services.AddControllers();

            //conexion MONGODB
            #region MONGODB
            services.Configure<ClinicaDBSettings>
                (
                Configuration.GetSection
                (
                    nameof(ClinicaDBSettings)
                )
                );
            services.AddSingleton<ClinicaDBSettings>
                (
                cs => cs.GetRequiredService<IOptions<ClinicaDBSettings>>().Value
                );
            services.AddTransient<IClinicaContext,ClinicaContext >();
            #endregion

            #region SWAGGER
           services.AddSwaggerGen(
               SW=>SW.SwaggerDoc
               (
                   "v1", new OpenApiInfo { Title = "CLINICA MATASANOS", Version = "v1"}
                ));
            #endregion

            //services.Configure<ClinicaDBSettings>(Configuration.GetSection
            //   (nameof(ClinicaDBSettings)));

            //services.AddSingleton<ClinicaDBSettings>(sp =>
            //    sp.GetRequiredService<IOptions<ClinicaDBSettings>>().Value);

            //services.AddTransient<IClinicaContext, ClinicaContext>();

            //SERVICIOS DE REPOSITORIOS
            #region REPOSITORIOS
            services.AddTransient<IPacienteRepositorio, PacienteRepositorio>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region SWAGGER
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLINICA MATASANOS"));
            #endregion
        }
    }
}
