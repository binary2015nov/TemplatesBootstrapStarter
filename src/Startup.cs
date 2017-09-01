using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Funq;
using ServiceStack;
using ServiceStack.Templates;

namespace App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseServiceStack(new AppHost());
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("Bootstrap Templates App", typeof(MyServices).GetAssembly()) {}

        public override void Configure(Container container)
        {
            Plugins.Add(new TemplatePagesFeature());
        }
    }

    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}