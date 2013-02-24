using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Hosting.Self;


namespace FhemDotNet.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostConfiguration = new HostConfiguration();
            hostConfiguration.UnhandledExceptionCallback = e =>
            {
                throw e;
            };
            var nancyHost = new NancyHost(new NHamlBootstrapper(), hostConfiguration, new Uri("http://localhost:8081"));
            nancyHost.Start();

            Console.WriteLine("Nancy now listening. Press enter to stop");

            Console.ReadLine();
            nancyHost.Stop();
        }
    }
}
