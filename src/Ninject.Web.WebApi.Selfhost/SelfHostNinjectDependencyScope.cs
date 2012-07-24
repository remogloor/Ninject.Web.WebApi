namespace SelfHosted_SampleApplication
{
    using System;

    using Ninject.Syntax;
    using Ninject.Web.WebApi;

    public class SelfHostNinjectDependencyScope : NinjectDependencyScope
    {
        public SelfHostNinjectDependencyScope(IResolutionRoot resolutionRoot)
            : base(resolutionRoot)
        {
        }

        public override void Dispose(bool disposing)
        {
            ((IDisposable)this.ResolutionRoot).Dispose();
            base.Dispose(disposing);
        }
    }
}