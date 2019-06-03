using Microsoft.AspNetCore.Mvc;
using MovieApp.Helpers;
using MovieApp.Helpers.Interfaces;
using System;

namespace MovieApp.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private IMovieHelper _movieHelper;

        public MovieController(IMovieHelper movieHelper)
        {
            _movieHelper = movieHelper;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(_movieHelper.GetAllMovies());
        }

        [HttpGet("{movieId}")]
        public IActionResult GetMovie(Guid movieId)
        {
            var movie = _movieHelper.GetMovie(movieId);

            if (movie != null)
            {
                return Ok(movie);
            }
            else
            {
                return NotFound(new { message = ErrorMessages.MOVIE_DOES_NOT_EXISTS });
            }
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody]Model.RequestModel.AddMovie movie)
        {
            if (movie == null)
            {
                return BadRequest(new { message = "Please check input." });
            }
            var result = _movieHelper.AddMovie(movie);

            if (Guid.TryParse(result, out Guid Id))
            {
                return Ok(_movieHelper.GetMovie(Id));
            }
            else if (result.Equals(ErrorMessages.MOVIE_ALREADY_EXISTS) ||
               result.Equals(ErrorMessages.MOVIE_NAME_EMPTY) ||
               result.Equals(ErrorMessages.MOVIE_YEAR_OF_RELEASE_SHOULD_BE_FROM_1900_2050) ||
               result.Equals(ErrorMessages.MOVIE_POSTER_INVALID_URL) ||
               result.Equals(ErrorMessages.PRODUCER_NOT_FOUND) ||
               result.Equals(ErrorMessages.ACTOR_NOT_FOUND))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }

        [HttpPatch]
        public IActionResult EditMovie([FromBody]Model.RequestModel.Movie movie)
        {
            if (movie == null)
            {
                return BadRequest(new { message = "Please check input." });
            }
            var result = _movieHelper.EditMovie(movie);

            if (Guid.TryParse(result, out Guid Id))
            {
                return Ok(_movieHelper.GetMovie(Id));
            }

            else if (result.Equals(ErrorMessages.MOVIE_DOES_NOT_EXISTS) ||
               result.Equals(ErrorMessages.MOVIE_NAME_EMPTY) ||
               result.Equals(ErrorMessages.MOVIE_YEAR_OF_RELEASE_SHOULD_BE_FROM_1900_2050) ||
               result.Equals(ErrorMessages.MOVIE_POSTER_INVALID_URL) ||
               result.Equals(ErrorMessages.PRODUCER_NOT_FOUND) ||
               result.Equals(ErrorMessages.ACTOR_NOT_FOUND))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }

        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovie(Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return BadRequest(new { message = "Please check input." });
            }
            var result = _movieHelper.DeleteMovie(movieId);

            if (result.Equals(String.Empty))
            {
                return Ok();
            }
            else if (result.Equals(ErrorMessages.MOVIE_DOES_NOT_EXISTS))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }
    }
}
