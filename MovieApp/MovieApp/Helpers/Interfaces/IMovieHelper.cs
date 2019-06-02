using System;
using System.Collections.Generic;

namespace MovieApp.Helpers.Interfaces
{
    public interface IMovieHelper
    {
        string AddMovie(Model.RequestModel.AddMovie movie);

        string DeleteMovie(Guid movieId);

        string EditMovie(Model.RequestModel.Movie movie);

        List<Model.ClientModel.Movie> GetAllMovies();

        Model.ClientModel.Movie GetMovie(Guid movieId);
    }
}