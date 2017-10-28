using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebLinkList.EF;
using Swashbuckle.AspNetCore.Swagger;

namespace WebLinkList.WebApi
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "WebLinkList API",
                    Description = "API for managing the web links",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "irambuk", Email = "", Url = "https://twitter.com/irambuk" },
                    License = new License { Name = "not finalized", Url = "https://example.com/license" }
                });
            });

            //services.AddEntityFrameworkSqlServer();

            services.AddDbContext<WebLinkContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebLinkContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebLinkList API V1");
            });




        }
    }
}
