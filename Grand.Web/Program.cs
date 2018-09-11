using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Grand.Web
{
    public class Program
    {
        /// <summary>
        /// 对于ASP.Net Core 应用程序来说, 本质上是一个独立的控制台程序.
        /// 不必须在IIS内部托管也不需要IIS来启动运行  这就是 Asp.Net Core 跨平台的基础
        /// ASP.Net Core 有一个内置的自托管(Self-Host)的Web Server 用来处理外部请求
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //self host
            //启动主机 主机负责应用程序启动和生存周期管理
            //配置服务器和请求处理管道  可用于托管Web应用
            var host = WebHost.CreateDefaultBuilder(args)    //1. 创建IWebHostBuilder
                
                .UseKestrel(options => options.AddServerHeader = false) 
                .CaptureStartupErrors(true)  //捕获启动错误
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
                .UseStartup<Startup>()
                //.UseStartup(typeof(Startup))  // it can also be used in this way
                .Build();  //2. 创建IWebHost

            host.Run();//3. 启动IWebHost
        }
    }
}