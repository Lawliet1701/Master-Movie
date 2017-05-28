using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFMovieRepository : IMovieRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Movie> Movies
        {
            get { return context.Movies; }
        }

        public void SaveMovie(Movie movie)
        {
            if (movie.MovieID == 0)
            {
                context.Movies.Add(movie);
            }
            else
            {
                Movie dbEntry = context.Movies.Find(movie.MovieID);
                if (dbEntry != null)
                {
                    dbEntry.Title = movie.Title;
                    dbEntry.TitleEN = movie.TitleEN;
                    dbEntry.Description = movie.Description;
                    dbEntry.PremiereDate = movie.PremiereDate;
                    dbEntry.Length = movie.Length;
                    dbEntry.Slogan = movie.Slogan;
                    dbEntry.RatingAgeLimit = movie.RatingAgeLimit;
                    dbEntry.Budget = movie.Budget;
                    dbEntry.KPID = movie.KPID;
                    dbEntry.RatingKP = movie.RatingKP;
                    dbEntry.RatingIMDB = movie.RatingIMDB;
                    dbEntry.ImageData = movie.ImageData;
                    dbEntry.ImageMimeType = movie.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Movie DeleteMovie(int movieID)
        {
            Movie dbEntry = context.Movies.Find(movieID);
            if (dbEntry != null)
            {
                context.Movies.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
