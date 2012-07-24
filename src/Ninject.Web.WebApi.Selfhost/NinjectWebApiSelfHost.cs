namespace SelfHosted_SampleApplication
{
    using System;
    using System.Web.Http;
    using System.Web.Http.SelfHost;

    using Ninject;
    using Ninject.Web.Common;

    public class NinjectWebApiSelfHost : IDisposable
    {
        private Bootstrapper bootstrapper;
        private HttpSelfHostServer server;

        public NinjectWebApiSelfHost(string baseAddress, IKernel kernel)
        {
            this.HttpConfiguration = new HttpSelfHostConfiguration(baseAddress);
            this.bootstrapper = new Bootstrapper();

            kernel.Bind<HttpConfiguration>().ToConstant(this.HttpConfiguration);
            this.bootstrapper.Initialize(() => kernel);

            this.server = new HttpSelfHostServer(this.HttpConfiguration);
        }

        public HttpSelfHostConfiguration HttpConfiguration { get; private set; }

        public void Start()
        {
            this.server.OpenAsync().Wait();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool disposable)
        {
            this.server.Dispose();
            this.bootstrapper.ShutDown();
        }
    }
}