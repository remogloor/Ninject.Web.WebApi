namespace Ninject.Web.WebApi.FilterBindingSyntax
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Controllers;

    public static class HttpActionDescriptorExtensionMethods
    {
        public static IEnumerable<object> GetCustomAttributes(this HttpActionDescriptor actionDescriptor, Type type)
        {
            return Enumerable.Cast<object>(
                (IEnumerable)typeof(HttpActionDescriptor)
                .GetMethod("GetCustomAttributes").MakeGenericMethod(type)
                .Invoke(actionDescriptor, new object[0]));
        }
    
        public static IEnumerable<object> GetCustomAttributes(this HttpControllerDescriptor actionDescriptor, Type type)
        {
            return Enumerable.Cast<object>(
                (IEnumerable)typeof(HttpControllerDescriptor)
                .GetMethod("GetCustomAttributes").MakeGenericMethod(type)
                .Invoke(actionDescriptor, new object[0]));
        }
    }
}