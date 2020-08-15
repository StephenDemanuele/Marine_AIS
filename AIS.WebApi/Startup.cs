using AIS.Parser.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIS.WebApi
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
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("CorsPolicy", builder =>
                    builder
                    .WithOrigins("https://localhost:44328")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddMvc();

            services.AddSingleton(ParserConfiguration.Get(Configuration));
            new Parser.Bootstrapping.DefaultBootstrappingConfiguration().Bootstrap(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}
