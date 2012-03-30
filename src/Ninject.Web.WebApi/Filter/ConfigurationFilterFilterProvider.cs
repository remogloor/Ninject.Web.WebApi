//-------------------------------------------------------------------------------
// <copyright file="ConfigurationFilterFilterProvider.cs" company="bbv Software Services AG">
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
    using System.Collections.Generic;
    using System.Linq;

    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class ConfigurationFilterFilterProvider : IFilterProvider
    {
        private readonly IKernel kernel;
        private readonly IEnumerable<IFilterProvider> filterProviders;

        public ConfigurationFilterFilterProvider(IKernel kernel, IEnumerable<IFilterProvider> filterProviders)
        {
            this.kernel = kernel;
            this.filterProviders = filterProviders;
        }

        public IEnumerable<Filter> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = this.filterProviders.SelectMany(fp => fp.GetFilters(configuration, actionDescriptor)).ToList();
            foreach (var filter in filters)
            {
                this.kernel.Inject(filter.Instance);
            }

            return filters;
        }
    }
}
