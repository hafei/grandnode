using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Grand.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Grand.Web
{
    /// <summary>
    /// Represents startup class of application
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// Get configuration root of the application
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Ctor

        public Startup(IHostingEnvironment environment)
        {
            //初始化配置文件
            //create configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                //加载json配置文件
                .AddJsonFile("App_Data/appsettings.json", optional: false, reloadOnChange: true)
                //加载环境变量  数据库连接 数据库驱动
                .AddEnvironmentVariables()
                .Build();
        }

        #endregion

        /// <summary>
        /// Add services to the application and configure service provider
        /// 
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //configuring https
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                //port can be configured in launchsetting.json
                //options.HttpsPort = 5001;
            });

            //
            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// http 请求管道
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //enable https
            application.UseHsts();
            //开启https重定向
            application.UseHttpsRedirection();
            //
            application.ConfigureRequestPipeline();
        }
    }
}
