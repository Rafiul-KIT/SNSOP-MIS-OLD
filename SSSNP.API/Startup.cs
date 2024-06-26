
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
using NLog;
using SSSNP_SNSOP.Core.DbHandler;
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

        private static Logger logger = LogManager.GetCurrentClassLogger();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //// test code for db operations: START
            //IDbOperation dbOperation = new SqlDbOperation(Configuration.GetSection("snsop_db_connStr").Value);

            //// native complex queries call goes here
            //var sql = "select * from tbl_Users where IP_Approver = 1";
            //var dataTableForSql = dbOperation.GetDataTable(sql);

            //// native stored procedure call goes here
            //var dataSet = dbOperation.ExecuteCmdForSp(new List<CmdParamModel>()
            //{
            //    new CmdParamModel() { ParamValue = "alamin", ParamName = "@User_id" },
            //}, "CheckUserid");

            //if (dataSet != null && dataSet.Tables.Count > 0)
            //{
            //    var dataTableForStoredProcedure = dataSet.Tables[0];
            //}
            //// test code for db operations: END


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SNSOP.API", Version = "v1" });
            });


            Utility.SetupLogging(Configuration.GetSection("LogFileLocation").Value, Configuration.GetSection("LogFileName").Value,
                NLog.LogLevel.FromString(Configuration.GetSection("LogLevel").Value));

            logger.Info("SNSOP API Project started...");

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
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSSNP.API v1"));
            //}

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SNSOP.API v1"));

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
