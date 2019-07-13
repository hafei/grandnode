using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Grand.Web
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            //self host
            //启动主机 主机负责应用程序启动和生存周期管理
            //配置服务器和请求处理管道  可用于托管Web应用
            var host = WebHost.CreateDefaultBuilder(args)
                
                .UseKestrel(options => options.AddServerHeader = false) //不显示 Server : Kestrel
                .CaptureStartupErrors(true)  //捕获启动错误
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")

                .UseStartup<Startup>()  //指定启动类  为服务注册及中间件注册提供入口
                //.UseStartup(typeof(Startup))  // it can also be used in this way
                .Build();

            host.Run();
        }
    }
}