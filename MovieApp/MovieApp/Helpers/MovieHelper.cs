using MovieApp.Helpers.Interfaces;
using MovieApp.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieApp.Helpers
{
    public class MovieHelper : IMovieHelper
    {

        private readonly IMovieRepository _movieRepository;

        private readonly IPersonRepository _personRepository;

        public MovieHelper(IMovieRepository movieRepository, IPersonRepository personRepository)
        {
            _movieRepository = movieRepository;
            _personRepository = personRepository;
        }

        public string AddMovie(Model.RequestModel.AddMovie movie)
        {
            var checkExists = _movieRepository.GetMovie(movie.Name, movie.YearOfRelease);
            if (checkExists != null)
            {
                return ErrorMessages.MOVIE_ALREADY_EXISTS;
            }
            else
            {
                if (!Utils.IsStringValid(movie.Name))
                {
                    return ErrorMessages.MOVIE_NAME_EMPTY;
                }

                if (movie.YearOfRelease < 1900 || movie.YearOfRelease > 2050)
                {
                    return ErrorMessages.MOVIE_YEAR_OF_RELEASE_SHOULD_BE_FROM_1900_2050;
                }

                if (!Utils.IsStringValid(movie.Plot))
                {
                    return ErrorMessages.MOVIE_PLOT_EMPTY;
                }

                if (!Utils.IsURLValid(movie.PosterURL))
                {
                    return ErrorMessages.MOVIE_POSTER_INVALID_URL;
                }

                if (movie.ProducerId == null)
                {
                    return ErrorMessages.PRODUCER_NOT_FOUND;
                }

                var producer = _personRepository.GetPerson(movie.ProducerId);
                if (producer == null || producer.PersonType != PersonType.Producer)
                {
                    return ErrorMessages.PRODUCER_NOT_FOUND;
                }

                if (movie.Actors == null || movie.Actors.Count == 0)
                {
                    return ErrorMessages.ACTOR_NOT_FOUND;
                }

                foreach (Guid actorId in movie.Actors)
                {
                    var actor = _personRepository.GetPerson(actorId);
                    if (actor == null || actor.PersonType != PersonType.Actor)
                    {
                        return ErrorMessages.ACTOR_NOT_FOUND;
                    }
                }

                var newMovie = _movieRepository.AddMovie(movie);
                if (newMovie != null)
                {
                    return newMovie.Id.ToString();
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
        }

        public string DeleteMovie(Guid movieId)
        {
            var checkExists = _movieRepository.GetMovie(movieId);
            if (checkExists == null)
            {
                return ErrorMessages.MOVIE_DOES_NOT_EXISTS;
            }
            else
            {
                if (_movieRepository.DeleteMovie(movieId))
                {
                    return string.Empty;
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
        }

        public string EditMovie(Model.RequestModel.Movie movie)
        {
            var checkExists = _movieRepository.GetMovie(movie.Id);
            if (checkExists == null)
            {
                return ErrorMessages.MOVIE_DOES_NOT_EXISTS;
            }
            else
            {
                if (movie.Name != null)
                {
                    if (Utils.IsStringEmpty(movie.Name))
                        return ErrorMessages.MOVIE_NAME_EMPTY;
                }

                if (movie.YearOfRelease > 0)
                {
                    if (movie.YearOfRelease < 1900 || movie.YearOfRelease > 2050)
                        return ErrorMessages.MOVIE_YEAR_OF_RELEASE_SHOULD_BE_FROM_1900_2050;
                }

                if (movie.Plot != null)
                {
                    if (Utils.IsStringEmpty(movie.Plot))
                        return ErrorMessages.MOVIE_PLOT_EMPTY;
                }

                if (movie.PosterURL != null)
                {
                    if (!Utils.IsURLValid(movie.PosterURL))
                        return ErrorMessages.MOVIE_POSTER_INVALID_URL;
                }

                if (movie.ProducerId != null)
                {
                    var producer = _personRepository.GetPerson(movie.ProducerId);
                    if (producer == null || producer.PersonType != PersonType.Producer)
                    {
                        return ErrorMessages.PRODUCER_NOT_FOUND;
                    }
                }

                if (movie.Actors != null && movie.Actors.Count != 0)
                {
                    foreach (Guid actorId in movie.Actors)
                    {
                        var actor = _personRepository.GetPerson(actorId);
                        if (actor == null || actor.PersonType != PersonType.Actor)
                        {
                            return ErrorMessages.ACTOR_NOT_FOUND;
                        }
                    }
                }
                var editMovie = _movieRepository.EditMovie(movie);
                if (editMovie != null)
                {
                    return editMovie.Id.ToString();
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
        }

        List<Model.ClientModel.Movie> IMovieHelper.GetAllMovies()
        {
            var allMovies = _movieRepository.GetAllMovies();

            List<Model.ClientModel.Movie> completeMovies = new List<Model.ClientModel.Movie>();

            foreach (Entity.Movie movie in allMovies)
            {
                var eachMovie = AutoMapper.Mapper.Map<Model.ClientModel.Movie>(movie);
                var actors = _movieRepository.GetActorsInMovie(movie.Id);
                List<Guid> actorIds = new List<Guid>();
                foreach (Entity.Person actor in actors)
                {
                    actorIds.Add(actor.Id);
                }
                eachMovie.Actors = actorIds;
                completeMovies.Add(eachMovie);
            }

            return completeMovies;
        }

        Model.ClientModel.Movie IMovieHelper.GetMovie(Guid movieId)
        {
            var movie = _movieRepository.GetMovie(movieId);
            var completeMovie = AutoMapper.Mapper.Map<Model.ClientModel.Movie>(movie);
            var actors = _movieRepository.GetActorsInMovie(movieId);
            List<Guid> actorIds = new List<Guid>();
            foreach (Entity.Person actor in actors)
            {
                actorIds.Add(actor.Id);
            }
            completeMovie.Actors = actorIds;

            return completeMovie;

        }
    }
}
