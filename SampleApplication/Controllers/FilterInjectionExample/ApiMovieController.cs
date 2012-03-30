
namespace SampleApplication.Controllers.FilterInjectionExample
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using SampleApplication.Models.Movie;

    public class ApiMovieController : ApiController
    {
        private readonly MoviesEntities moviesEntities;

        public ApiMovieController(MoviesEntities moviesEntities)
        {
            this.moviesEntities = moviesEntities;
        }

        // GET /api/values
        [Cache(0, 0, 5, 0)]
        public IEnumerable<Movies> Get()
        {
            //return Enumerable.Empty<Movies>();
            return this.moviesEntities.Movies.ToList();
        }

        // POST /api/values
        [ClearCacheOnSuccess("Get")]
        public void Post(Movies value)
        {
            if (ModelState.IsValid)
            {
                this.moviesEntities.AddToMovies(value);
                this.moviesEntities.SaveChanges();
            }
        }
    }
}
