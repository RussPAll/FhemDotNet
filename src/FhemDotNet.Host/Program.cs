using System;
using FhemDotNet.Host.Nancy;
using Nancy.Hosting.Self;


namespace FhemDotNet.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostConfiguration = new HostConfiguration
                {
                    UnhandledExceptionCallback = e => Console.WriteLine(e.ToString())
                };
            var nancyHost = new NancyHost(new NHamlBootstrapper(), hostConfiguration, new Uri("http://localhost:8081"));
            nancyHost.Start();

            Console.WriteLine("Nancy now listening. Press enter to stop");

            Console.ReadLine();
            nancyHost.Stop();
        }
    }
}
