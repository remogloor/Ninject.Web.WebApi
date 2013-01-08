//-------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2012 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace SelfHosted_SampleApplication
{
    using System;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.SelfHost;

    using Ninject;
    using Ninject.Web.Common.SelfHost;

    /// <summary>
    /// The program main.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The application main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var webApiConfiguration = new HttpSelfHostConfiguration("http://localhost:8080");
            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "values" });

            using (var selfHost = new NinjectSelfHostBootstrapper(CreateKernel, webApiConfiguration))
            {
                selfHost.Start();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <returns>the newly created kernel.</returns>
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
