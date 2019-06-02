using System;
using System.Collections.Generic;

namespace MovieApp.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Entity.Movie AddMovie(Model.RequestModel.AddMovie movie);
        
        bool DeleteMovie(Guid movieId);

        Entity.Movie EditMovie(Model.RequestModel.Movie movie);

        List<Entity.Person> GetActorsInMovie(Guid movieId);

        List<Entity.Movie> GetAllMovies();

        Entity.Movie GetMovie(Guid movieId);

        Entity.Movie GetMovie(string movieName, int yearOfRelease);

        Entity.Person GetMovieProducer(Guid movieId);
    }
}