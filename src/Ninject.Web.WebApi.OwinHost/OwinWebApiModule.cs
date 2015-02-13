//-------------------------------------------------------------------------------
// <copyright file="OwinWebApiModule.cs" company="bbv Software Services AG">
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

using System;

namespace Ninject.Web.WebApi.OwinHost
{
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    using Ninject.Modules;

    /// <summary>
    /// The OWIN web API module.
    /// </summary>
    internal class OwinWebApiModule : NinjectModule
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly HttpConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinWebApiModule"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public OwinWebApiModule(HttpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Loads the module.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Bind<HttpConfiguration>().ToConstant(this.configuration);
        }

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public override void VerifyRequiredModulesAreLoaded()
        {
            if (!this.Kernel.HasModule(typeof(WebApiModule).FullName))
            {
                throw new InvalidOperationException("This module requires Ninject.Web.WebAPI extension");
            }

            this.Rebind<IDependencyResolver>().To<OwinNinjectDependencyResolver>();

            this.Kernel.Components.RemoveAll<IWebApiRequestScopeProvider>();
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, OwinWebApiRequestScopeProvider>();
        }
    }
}