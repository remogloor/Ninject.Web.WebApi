
namespace SampleApplication.Controllers.ControllerInjectionExample
{
    using System.Collections.Generic;
    using System.Web.Http;

    using SampleApplication.Services.ValuesService;

    public class ValuesController : ApiController
    {
        private readonly IValuesProvider valuesProvider;

        public ValuesController(IValuesProvider valuesProvider)
        {
            this.valuesProvider = valuesProvider;
        }

        // GET /api/values
        public IEnumerable<string> Get()
        {
            return this.valuesProvider.GetValues();
        }

        // GET /api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/values
        public void Post(string value)
        {
        }

        // PUT /api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/values/5
        public void Delete(int id)
        {
        }
    }
}
