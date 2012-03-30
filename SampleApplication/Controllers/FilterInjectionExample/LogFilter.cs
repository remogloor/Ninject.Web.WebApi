//-------------------------------------------------------------------------------
// <copyright file="LogFilter.cs" company="bbv Software Services AG">
//   Copyright (c) 2010 bbv Software Services AG
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

namespace SampleApplication.Controllers.FilterInjectionExample
{
    using System.Web.Http.Controllers;

    using log4net;

    using Ninject.Web.WebApi.Filter;

    /// <summary>
    /// A filter that loggs an actions.
    /// </summary>
    public class LogFilter : AbstractActionFilter
    {
        /// <summary>
        /// The logger used to log.
        /// </summary>
        private readonly ILog log;

        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilter"/> class.
        /// </summary>
        /// <param name="log">The logger used to log.</param>
        /// <param name="prefix">The prefix.</param>
        public LogFilter(ILog log, string prefix)
        {
            this.log = log;
            this.prefix = prefix;
        }

        public override bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Called before an action method executes.
        /// Logs that an action is beeing executed.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.log.DebugFormat(
                "{2}Executing action {0}.{1}",
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                actionContext.ActionDescriptor.ActionName,
                this.prefix);
        }

        /// <summary>
        /// Called after the action method executes.
        /// Logs that an action was executed.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            this.log.DebugFormat(
                "Executed action {0}.{1}",
                actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
        }
    }
}