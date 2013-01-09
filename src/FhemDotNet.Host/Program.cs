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
            var nancyHost = new NancyHost(new Uri("http://localhost:8081"), new NHamlBootstrapper());
            nancyHost.Start();

            Console.WriteLine("Nancy now listening. Press enter to stop");

            Console.ReadLine();
            nancyHost.Stop();
        }
    }
}
