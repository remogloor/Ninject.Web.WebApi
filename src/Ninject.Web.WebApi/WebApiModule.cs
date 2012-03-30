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

namespace Ninject.Web.WebApi
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using System.Web.Http.Services;
    using System.Web.Http.Validation;
    using System.Web.Routing;

    using Ninject.Web.Common;
    using Ninject.Web.WebApi.Filter;
    using Ninject.Web.WebApi.Validation;

    /// <summary>
    /// Defines the bindings and plugins of the MVC web extension.
    /// </summary>
    public class WebApiModule : GlobalKernelRegistrationModule<OnePerRequestHttpModule>
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            base.Load();
            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectWebApiHttpApplicationPlugin>();
            this.Kernel.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
            this.Kernel.Bind<IFilterProvider>().ToConstant(new DefaultFilterProvider(this.Kernel, GlobalConfiguration.Configuration.ServiceResolver.GetFilterProviders()));
            this.Kernel.Bind<IFilterProvider>().To<NinjectFilterProvider>();
            this.Kernel.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
            this.Kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            this.Kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            this.Kernel.Bind<ModelValidatorProvider>().ToConstant(new NinjectDefaultModelValidatorProvider(this.Kernel, GlobalConfiguration.Configuration.ServiceResolver.GetModelValidatorProviders()));
        }
    }
}