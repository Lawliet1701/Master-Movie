using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IMovieRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<UsersMovie> UsersMovies { get; }
        void SaveMovie(Movie movie);
        void SaveUserMovie(UsersMovie userMovie);
        Movie DeleteMovie(int movieID);
    }

}
