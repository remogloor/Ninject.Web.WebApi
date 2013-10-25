//-------------------------------------------------------------------------------
// <copyright file="HttpActionDescriptorExtensionMethods.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.WebApi.FilterBindingSyntax
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Controllers;

    /// <summary>
    /// Extension methods for the http action descriptor
    /// </summary>
    public static class HttpActionDescriptorExtensionMethods
    {
        /// <summary>
        /// Gets the custom attributes of the specified type.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <param name="type">The type of the attribute.</param>
        /// <returns>The custom attributes of the specified type.</returns>
        public static IEnumerable<object> GetCustomAttributes(this HttpActionDescriptor actionDescriptor, Type type)
        {
            return ((IEnumerable)typeof(HttpActionDescriptor)
                                     .GetMethod("GetCustomAttributes", new Type[0]).MakeGenericMethod(type)
                                     .Invoke(actionDescriptor, new object[0])).Cast<object>();
        }

        /// <summary>
        /// Gets the custom attributes of the specified type.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <param name="type">The type of the attribute.</param>
        /// <returns>The custom attributes of the specified type.</returns>
        public static IEnumerable<object> GetCustomAttributes(this HttpControllerDescriptor actionDescriptor, Type type)
        {
            return ((IEnumerable)typeof(HttpControllerDescriptor)
                                     .GetMethod("GetCustomAttributes").MakeGenericMethod(type)
                                     .Invoke(actionDescriptor, new object[0])).Cast<object>();
        }
    }
}