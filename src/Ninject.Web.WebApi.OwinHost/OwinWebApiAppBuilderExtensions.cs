//-------------------------------------------------------------------------------
// <copyright file="OwinWebApiAppBuilderExtensions.cs" company="bbv Software Services AG">
//   Copyright (c) 2012 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Web.WebApi.OwinHost
{
    using System;
    using System.Linq;
    using System.Web.Http;

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
        /// <returns>The application builder.</returns>
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
        /// <returns>The application builder.</returns>
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
