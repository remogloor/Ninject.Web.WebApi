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

    /// <summary>
    /// An abstract action filter implementation to simplify action filter implementations.
    /// </summary>
    public abstract class AbstractActionFilter : IActionFilter
    {
        /// <summary>
        /// The internal filter.
        /// </summary>
        private readonly IActionFilter internalFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractActionFilter"/> class.
        /// </summary>
        protected AbstractActionFilter()
        {
            this.internalFilter = new InternalActionFilter(this);
        }

        /// <summary>
        /// Gets a value indicating whether this filter can occur multiple times.
        /// </summary>
        /// <value>True if the filter can occur multiple times, False otherwise.</value>
        public abstract bool AllowMultiple { get; }

        /// <summary>
        /// Executes the action filter asyncronously.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="continuation">The continuation.</param>
        /// <returns>The task</returns>
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            return this.internalFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation);
        }

        /// <summary>
        /// Called when the action is executed.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public virtual void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }

        /// <summary>
        /// Called before the action is executing.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
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