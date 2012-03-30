//-------------------------------------------------------------------------------
// <copyright file="AbstractActionFilter.cs" company="Ninject Project Contributors">
//   Copyright (c) 2012 Ninject Project Contributors
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
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public abstract class AbstractActionFilter : IActionFilter
    {
        private readonly IActionFilter internalFilter;

        protected AbstractActionFilter()
        {
            this.internalFilter = new InternalActionFilter(this);
        }

        public abstract bool AllowMultiple { get; }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            return this.internalFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation);
        }

        public virtual void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }

        public virtual void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        private class InternalActionFilter : ActionFilterAttribute
        {
            private readonly AbstractActionFilter parent;

            public InternalActionFilter(AbstractActionFilter parent)
            {
                this.parent = parent;
            }

            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                this.parent.OnActionExecuted(actionExecutedContext);
            }

            public override void OnActionExecuting(HttpActionContext actionContext)
            {
                this.parent.OnActionExecuting(actionContext);
            }
        }
    }
}