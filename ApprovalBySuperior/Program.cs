using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApprovalBySuperior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //.UseUrls("http://*:5001/")は追加
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:5001/");
    }
}
