// -------------------------------------------------------------------------------------------------
// <copyright file="WebApiModule.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.WebApi
{
    using System.Web.Http.Dependencies;
    using System.Web.Http.Filters;
    using System.Web.Http.Validation;

    using Ninject.Modules;
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