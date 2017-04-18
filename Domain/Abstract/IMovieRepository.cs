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
        void SaveMovie(Movie movie);
        Movie DeleteMovie(int movieID);
    }

}
