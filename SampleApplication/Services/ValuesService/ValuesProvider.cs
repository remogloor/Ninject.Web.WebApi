namespace SampleApplication.Services.ValuesService
{
    public class ValuesProvider : IValuesProvider
    {
        public string[] GetValues()
        {
            return new[] { "value1", "value2" };
        }
    }
}