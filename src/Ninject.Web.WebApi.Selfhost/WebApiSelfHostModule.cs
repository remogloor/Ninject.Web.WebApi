namespace SelfHosted_SampleApplication
{
    using System.Web.Http.Dependencies;

    using Ninject.Modules;
    using Ninject.Web.WebApi;

    public class WebApiSelfHostModule : NinjectModule
    {
        public override void Load()
        {
            this.Rebind<IDependencyResolver>().To<NinjectSelfHostDependencyResolver>();

            this.Kernel.Components.RemoveAll<IWebApiRequestScopeProvider>();
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, SelfHostWebApiRequestScopeProvider>();
        }
    }
}