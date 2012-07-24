namespace Ninject.Web.WebApi
{
    using System.Web;

    using Ninject.Activation;
    using Ninject.Components;

    public class DefaultWebApiRequestScopeProvider : NinjectComponent, IWebApiRequestScopeProvider
    {
        public object GetRequestScope(IContext context)
        {
            return HttpContext.Current;
        }
    }
}