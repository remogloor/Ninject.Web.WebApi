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
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, DefaultWebApiRequestScopeProvider>();
            
            this.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();

            var defaultFilterProviders = GlobalConfiguration.Configuration.Services.GetServices(typeof(IFilterProvider)).Cast<IFilterProvider>();
            this.Bind<IFilterProvider>().ToConstant(new DefaultFilterProvider(this.Kernel, defaultFilterProviders));
            this.Bind<IFilterProvider>().To<NinjectFilterProvider>();
            
            this.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
            this.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            var modelValidatorProviders = GlobalConfiguration.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>();
            this.Kernel.Bind<ModelValidatorProvider>().ToConstant(new NinjectDefaultModelValidatorProvider(this.Kernel, modelValidatorProviders));
            this.Kernel.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();

            this.Kernel.Bind<HttpConfiguration>().ToMethod(ctx => GlobalConfiguration.Configuration);
        }
    }
}