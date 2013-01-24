//-------------------------------------------------------------------------------
// <copyright file="NinjectDependencyScope.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Ninject.Infrastructure.Disposal;
    using Ninject.Parameters;
    using Ninject.Syntax;

    /// <summary>
    /// Dependency Scope implementation for ninject
    /// </summary>
    public class NinjectDependencyScope : DisposableObject, IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyScope"/> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            this.ResolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// Gets the resolution root.
        /// </summary>
        /// <value>The resolution root.</value>
        protected IResolutionRoot ResolutionRoot
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets the service of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The service instance or <see langword="null"/> if none is configured.</returns>
        public object GetService(Type serviceType)
        {
            var request = this.ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        /// <summary>
        /// Gets the services of the specifies type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>All service instances or an empty enumerable if none is configured.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.ResolutionRoot.GetAll(serviceType).ToList();
        }
    }
}