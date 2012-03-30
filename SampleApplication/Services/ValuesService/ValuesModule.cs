namespace SampleApplication.Services.ValuesService
{
    using Ninject.Modules;

    public class ValuesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IValuesProvider>().To<ValuesProvider>();
        }
    }
}