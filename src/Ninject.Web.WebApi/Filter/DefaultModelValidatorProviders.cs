//-------------------------------------------------------------------------------
// <copyright file="DefaultFilterProviders.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2014 Ninject Project Contributors
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

namespace Ninject.Web.WebApi.Filter
{
    using System.Collections.Generic;
    using System.Web.Http.Validation;

    /// <summary>
    /// Keeps a reference to all default model validator providers from the configuration.
    /// </summary>
    public class DefaultModelValidatorProviders
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultModelValidatorProviders"/> class.
        /// </summary>
        /// <param name="defaultModelValidatorProviders">The default model validator providers.</param>
        public DefaultModelValidatorProviders(IEnumerable<ModelValidatorProvider> defaultModelValidatorProviders)
        {
            this.DefaultModelValidators = defaultModelValidatorProviders;
        }

        /// <summary>
        /// Gets the default model validator providers.
        /// </summary>
        /// <value>The default model validator providers.</value>
        public IEnumerable<ModelValidatorProvider> DefaultModelValidators { get; private set; }
    }
}