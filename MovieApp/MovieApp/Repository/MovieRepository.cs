using MovieApp.Entity;
using MovieApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Repository
{
    public class MovieRepository : IMovieRepository
    {

        private MovieAppDbContext _context;

        public MovieRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public Entity.Movie AddMovie(Model.RequestModel.AddMovie movie)
        {
            var newMovie = AutoMapper.Mapper.Map<Entity.Movie>(movie);
            _context.Movies.Add(newMovie);

            foreach (Guid actorId in movie.Actors)
            {
                var actorMovieMap = new ActorMovieMap()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                _context.ActorMovieMap.Add(actorMovieMap);
            }

            if (_context.SaveChanges() > 0)
            {
                return newMovie;
            }
            return null;
        }

        public bool DeleteMovie(Guid movieId)
        {
            var movie = GetMovie(movieId);
            if (movie != null)
            {
                List<ActorMovieMap> actorMovieMaps = _context.ActorMovieMap.Where(am => am.MovieId == movieId).ToList();
                _context.ActorMovieMap.RemoveRange(actorMovieMaps);

                _context.Movies.Remove(movie);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public Entity.Movie EditMovie(Model.RequestModel.Movie movie)
        {
            var movieObj = GetMovie(movie.Id);
            AutoMapper.Mapper.Map(movie, movieObj);

            if (movie.Actors != null && movie.Actors.Count != 0)
            {
                var actorsList = _context.ActorMovieMap.Where(am => am.MovieId == movie.Id).ToList();
                _context.ActorMovieMap.RemoveRange(actorsList);

                if (_context.SaveChanges() > 0)
                {
                    foreach (Guid actorId in movie.Actors)
                    {
                        _context.ActorMovieMap.Add(new ActorMovieMap()
                        {
                            ActorId = actorId,
                            MovieId = movie.Id
                        });
                    }
                    if (_context.SaveChanges() > 0)
                        return movieObj;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public List<Entity.Person> GetActorsInMovie(Guid movieId)
        {
            List<Entity.Person> actors = new List<Entity.Person>();
            foreach (var map in _context.ActorMovieMap.Where(am => am.MovieId == movieId))
            {
                actors.Add(_context.People.FirstOrDefault(p => p.Id == map.ActorId));
            }
            return actors;
        }

        public List<Entity.Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public Entity.Movie GetMovie(Guid movieId)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == movieId);
        }

        public Movie GetMovie(string movieName, int yearOfRelease)
        {
            return _context.Movies.FirstOrDefault(m => m.Name == movieName && m.YearOfRelease == yearOfRelease);
        }

        public Entity.Person GetMovieProducer(Guid movieId)
        {
            return GetMovie(movieId).Producer;
        }

    }
}
