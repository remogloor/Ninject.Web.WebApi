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

using Ninject.Modules;

namespace Ninject.Web.WebApi
{
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Filters;
    using System.Web.Http.Validation;
    using System.Web.Routing;

    using Ninject.Web.Common;
    using Ninject.Web.WebApi.Filter;
    using Ninject.Web.WebApi.Validation;

    /// <summary>
    /// Defines the bindings and plugins of the WebApi extension.
    /// </summary>
    public class WebApiModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectWebApiHttpApplicationPlugin>();
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, DefaultWebApiRequestScopeProvider>();

            this.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();

            this.Bind<IFilterProvider>().To<DefaultFilterProvider>();
            this.Bind<IFilterProvider>().To<NinjectFilterProvider>();

            this.Bind<ModelValidatorProvider>().To<NinjectDefaultModelValidatorProvider>();
            this.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();
        }
    }
}