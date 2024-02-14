
using CSDS.Core.Framework.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SSSNP.API", Version = "v1" });
            });


            string managerName = Configuration.GetSection("DataPartition:Manager").Value;
            string connectionString = Configuration.GetSection("DataPartition:ConnectionString").Value;
            DataPartitionManagers.Instance.AddDataPartitionConfiguration(managerName, connectionString);

            string managerNameFAO = Configuration.GetSection("DataPartition:ManagerFAO").Value;
            string connectionStringFAO = Configuration.GetSection("DataPartition:ConnectionStringFAO").Value;
            DataPartitionManagers.Instance.AddDataPartitionConfiguration(managerNameFAO, connectionStringFAO);

            string managerNameSNSOP = Configuration.GetSection("DataPartition:ManagerSNSOP").Value;
            string connectionStringSNSOP = Configuration.GetSection("DataPartition:ConnectionStringSNSOP").Value;
            DataPartitionManagers.Instance.AddDataPartitionConfiguration(managerNameSNSOP, connectionStringSNSOP);

            string apiKey = Configuration.GetSection("APIAuthorization:APIpasskey").Value;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSSNP.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
