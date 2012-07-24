namespace Ninject.Web.WebApi
{
    using Ninject.Activation;
    using Ninject.Components;

    public interface IWebApiRequestScopeProvider : INinjectComponent
    {
        object GetRequestScope(IContext context);
    }
}