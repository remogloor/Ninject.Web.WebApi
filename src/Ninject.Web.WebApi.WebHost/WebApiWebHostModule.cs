//-------------------------------------------------------------------------------
// <copyright file="WebApiModule.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.WebApi.WebHost
{
    using System;
    using System.Web.Http;

    using Ninject.Modules;

    /// <summary>
    /// Defines the bindings of the WebApi WebHost extension.
    /// </summary>
    public class WebApiWebHostModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<HttpConfiguration>().ToMethod(ctx => GlobalConfiguration.Configuration);
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
        }
    }
}
