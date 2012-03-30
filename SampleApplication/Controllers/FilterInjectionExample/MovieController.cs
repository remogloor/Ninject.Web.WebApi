//-------------------------------------------------------------------------------
// <copyright file="MovieController.cs" company="Ninject Project Contributors">
//   Copyright (c) 2012 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace SampleApplication.Controllers.FilterInjectionExample
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using SampleApplication.Models.Movie;

    /// <summary>
    /// Controller for the movies
    /// </summary>
    public class MovieController : ApiController
    {
        private readonly MoviesEntities moviesEntities;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/> class.
        /// </summary>
        /// <param name="moviesEntities">The movies entities.</param>
        public MovieController(MoviesEntities moviesEntities)
        {
            this.moviesEntities = moviesEntities;
        }

        /// <summary>
        /// Gets the movies: GET /movie
        /// </summary>
        /// <returns>The movies in the database.</returns>
        [Cache(0, 0, 5, 0)]
        public IEnumerable<Movies> Get()
        {
            return this.moviesEntities.Movies.ToList();
        }

        /// <summary>
        /// Handles POST of new movies.
        /// </summary>
        /// <param name="value">The new movie.</param>
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
