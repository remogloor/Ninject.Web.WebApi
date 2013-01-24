//-------------------------------------------------------------------------------
// <copyright file="DistributedCacheFilter.cs" company="bbv Software Services AG">
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
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Controllers;

    using Ninject.Web.WebApi.Filter;

    using SampleApplication.Services.DistributedCacheService;

    /// <summary>
    /// A filter that caches the result of an action in a distributed cache.
    /// </summary>
    public class DistributedCacheFilter : AbstractActionFilter
    {
        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly IDistributedCacheService cache;

        /// <summary>
        /// The expiration time of the cache entry.
        /// </summary>
        private readonly TimeSpan expirationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheFilter"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="expirationTime">The expiration time.</param>
        public DistributedCacheFilter(IDistributedCacheService cache, TimeSpan expirationTime)
        {
            this.cache = cache;
            this.expirationTime = expirationTime;
        }

        /// <summary>
        /// Gets a value indicating whether this filter can occur multiple times.
        /// </summary>
        /// <value>True if the filter can occur multiple times, False otherwise.</value>
        public override bool AllowMultiple
        {
            get
            {
                return false;
            }
        }
        
        /// <summary>
        /// Called before an action method executes.
        /// Gets the cached result if available.
        /// </summary>
        /// <param name="actionContext">The action Context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var result = this.cache.GetEntry(GetKey(actionContext.ActionDescriptor)) as Response;
            if (result != null)
            {
                actionContext.Response = new HttpResponseMessage(result.StatusCode)
                    {
                        Version = result.Version,
                        RequestMessage = actionContext.Request,
                        ReasonPhrase = result.ReasonPhrase,
                        Content = new ByteArrayContent(result.Content)
                    };

                actionContext.Response.Content.Headers.ContentType = result.ContentType;
            }
        }

        /// <summary>
        /// Called after the action method executes.
        /// Saves the result to the cache.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            var resultContent = actionExecutedContext.Response.Content.ReadAsStreamAsync();
            resultContent.Wait();
            var stream = resultContent.Result;
            stream.Position = 0;
            var content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);

            this.cache.AddEntry(
                GetKey(actionExecutedContext.ActionContext.ActionDescriptor),
                new Response
                    {
                        StatusCode = actionExecutedContext.Response.StatusCode,
                        ReasonPhrase = actionExecutedContext.Response.ReasonPhrase,
                        Version = actionExecutedContext.Response.Version,
                        Content = content,
                        ContentType = actionExecutedContext.Response.Content.Headers.ContentType,
                },
                this.expirationTime);
        }

        /// <summary>
        /// Gets the key from the action descriptor.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The key for the action.</returns>
        private static string GetKey(HttpActionDescriptor actionDescriptor)
        {
            return string.Format(
                "{0}.{1}",
                actionDescriptor.ControllerDescriptor.ControllerName,
                actionDescriptor.ActionName);
        }

        private class Response
        {
            public HttpStatusCode StatusCode { get; set; }

            public string ReasonPhrase { get; set; }
            
            public Version Version { get; set; }
            
            public byte[] Content { get; set; }
            
            public MediaTypeHeaderValue ContentType { get; set; }
        }
    }
}