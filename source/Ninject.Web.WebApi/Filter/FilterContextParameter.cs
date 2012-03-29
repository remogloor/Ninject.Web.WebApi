//-------------------------------------------------------------------------------
// <copyright file="FilterContextParameter.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.WebApi.Filter
{
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Ninject.Parameters;

    /// <summary>
    /// A parameter that contains the controller context and action descriptor for the filter.
    /// </summary>
    public class FilterContextParameter : Parameter
    {
        /// <summary>
        /// The name of the parameter.
        /// </summary>
        public const string ParameterName = "FilterContext";

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterContextParameter"/> class.
        /// </summary>
        /// <param name="configuration">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        public FilterContextParameter(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
            : base(ParameterName, ctx => null, false)
        {
            this.HttpConfiguration = configuration;
            this.ActionDescriptor = actionDescriptor;
        }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public HttpConfiguration HttpConfiguration { get; private set; }

        /// <summary>
        /// Gets the action descriptor.
        /// </summary>
        /// <value>The action descriptor.</value>
        public HttpActionDescriptor ActionDescriptor { get; private set; }
    }
}