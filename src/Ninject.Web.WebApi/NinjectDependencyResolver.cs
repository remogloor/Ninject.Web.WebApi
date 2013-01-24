//-------------------------------------------------------------------------------
// <copyright file="NinjectDependencyResolver.cs" company="bbv Software Services AG">
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
    using System.Web.Http.Dependencies;

    using Ninject.Syntax;

    /// <summary>
    /// Dependency resolver implementation for ninject.
    /// </summary>
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ninject.Web.WebApi.NinjectDependencyResolver"/> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectDependencyResolver(IResolutionRoot resolutionRoot) : base(resolutionRoot)
        {
        }

        /// <summary>
        /// Begins the scope.
        /// </summary>
        /// <returns>The new scope</returns>
        public virtual IDependencyScope BeginScope()
        {
            return this;
        }
    }
}