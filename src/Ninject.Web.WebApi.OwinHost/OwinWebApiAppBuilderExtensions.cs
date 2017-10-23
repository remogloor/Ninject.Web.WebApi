// -------------------------------------------------------------------------------------------------
// <copyright file="OwinWebApiAppBuilderExtensions.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2010 Enkari, Ltd. All rights reserved.
//   Copyright (c) 2010-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
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
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.WebApi.OwinHost
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Ninject.Web.Common;
    using Ninject.Web.Common.OwinHost;

    using Owin;

    /// <summary>
    /// The OWIN web API app builder extensions.
    /// </summary>
    public static class OwinWebApiAppBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/> and Adds Web API component to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> used to configure the endpoint.</param>
        /// <returns>The updated application builder.</returns>
        public static IAppBuilder UseNinjectWebApi(this IAppBuilder app, HttpConfiguration configuration)
        {
            AddOwinModuleToBootstrapper(app, configuration);

            return app.UseWebApi(configuration);
        }

        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/> and Adds Web API component to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="httpServer">The http server.</param>
        /// <returns>The updated application builder.</returns>
        public static IAppBuilder UseNinjectWebApi(this IAppBuilder app, HttpServer httpServer)
        {
            if (httpServer == null)
            {
                throw new ArgumentNullException("httpServer");
            }

            AddOwinModuleToBootstrapper(app, httpServer.Configuration);

            return app.UseWebApi(httpServer);
        }

        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/>
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> used to configure the endpoint.</param>
        private static void AddOwinModuleToBootstrapper(IAppBuilder app, HttpConfiguration configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var bootstrapper =
                app.Properties.Where(element => element.Key.Equals(OwinAppBuilderExtensions.NinjectOwinBootstrapperKey))
                    .Select(x => x.Value)
                    .OfType<OwinBootstrapper>()
                    .Single();

            bootstrapper.AddModule(new OwinWebApiModule(configuration));
        }
    }
}