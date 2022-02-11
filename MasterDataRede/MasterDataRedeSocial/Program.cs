﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DDDSample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseUrls("https://localhost:5000","https://localhost:5001");
    }
}
