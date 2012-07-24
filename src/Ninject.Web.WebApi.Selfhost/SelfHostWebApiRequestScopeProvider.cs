namespace SelfHosted_SampleApplication
{
    using Ninject.Activation;
    using Ninject.Components;
    using Ninject.Extensions.NamedScope;
    using Ninject.Web.WebApi;

    public class SelfHostWebApiRequestScopeProvider : NinjectComponent, IWebApiRequestScopeProvider
    {
        internal const string WebAPIScopeName = "Ninject_WebAPIScope";

        public object GetRequestScope(IContext context)
        {
            try
            {
                return NamedScopeExtensionMethods.GetScope(context, WebAPIScopeName);
            }
            catch (UnknownScopeException)
            {
                return null;
            }
        }
    }
}