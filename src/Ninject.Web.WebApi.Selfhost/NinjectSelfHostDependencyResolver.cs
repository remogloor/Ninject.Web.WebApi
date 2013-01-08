//-------------------------------------------------------------------------------
// <copyright file="NinjectSelfHostDependencyResolver.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.WebApi.Selfhost
{
    using System.Web.Http.Dependencies;

    using Ninject.Extensions.NamedScope;
    using Ninject.Syntax;
    using Ninject.Web.WebApi;

    /// <summary>
    /// Ninject dependency resolver for self hosting
    /// </summary>
    public class NinjectSelfHostDependencyResolver : NinjectDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectSelfHostDependencyResolver"/> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectSelfHostDependencyResolver(IResolutionRoot resolutionRoot)
            : base(resolutionRoot)
        {
        }

        /// <summary>
        /// Begins the scope.
        /// </summary>
        /// <returns>The new scope</returns>
        public override IDependencyScope BeginScope()
        {
            return new SelfHostNinjectDependencyScope(
                this.ResolutionRoot.CreateNamedScope(SelfHostWebApiRequestScopeProvider.WebAPIScopeName));
        }
    }
}