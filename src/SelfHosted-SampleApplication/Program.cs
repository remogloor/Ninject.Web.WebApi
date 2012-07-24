namespace SelfHosted_SampleApplication
{
    using System;
    using System.Reflection;
    using System.Web.Http;

    using Ninject;

    class Program
    {
        static void Main(string[] args)
        {
            using (var selfHost = new NinjectWebApiSelfHost("http://localhost:8080", CreateKernel()))
            {
                selfHost.HttpConfiguration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional, controller = "values" });
                selfHost.Start();

                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
