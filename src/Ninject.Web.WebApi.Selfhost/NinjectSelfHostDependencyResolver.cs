namespace SelfHosted_SampleApplication
{
    using System.Web.Http.Dependencies;

    using Ninject.Extensions.NamedScope;
    using Ninject.Syntax;
    using Ninject.Web.WebApi;

    public class NinjectSelfHostDependencyResolver : NinjectDependencyResolver
    {
        public NinjectSelfHostDependencyResolver(IResolutionRoot resolutionRoot)
            : base(resolutionRoot)
        {
        }

        public override IDependencyScope BeginScope()
        {
            return new SelfHostNinjectDependencyScope(
                this.ResolutionRoot.CreateNamedScope(SelfHostWebApiRequestScopeProvider.WebAPIScopeName));
        }
    }
}