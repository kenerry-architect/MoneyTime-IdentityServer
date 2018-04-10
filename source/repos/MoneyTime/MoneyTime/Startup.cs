using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTime.IdentityConfig;
using MoneyTime.IdentityModel;

namespace MoneyTime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentitySettings>(Configuration);
            var settings = Configuration.Get<IdentitySettings>();
            services.AddSingleton(settings);

            services.AddIdentityServer
            (
                options =>
                {
                    options.IssuerUri = settings.IdentityEndPoint;
                    options.Endpoints.EnableDiscoveryEndpoint = true;
                    options.Endpoints.EnableTokenEndpoint = true;
                    options.Endpoints.EnableUserInfoEndpoint = true;

                    options.Endpoints.EnableAuthorizeEndpoint = false;
                    options.Endpoints.EnableCheckSessionEndpoint = false;
                    options.Endpoints.EnableEndSessionEndpoint = false;
                    options.Endpoints.EnableIntrospectionEndpoint = false;
                    options.Endpoints.EnableTokenRevocationEndpoint = false;
                }
            )
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(IdentityConfiguration.GetApiResources(settings))
            .AddInMemoryClients(IdentityConfiguration.GetClients(settings));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
