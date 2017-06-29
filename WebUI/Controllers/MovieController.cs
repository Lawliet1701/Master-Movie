using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repository;
        public int PageSize = 16;

        public MovieController(IMovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        public ViewResult List(string searchString, string category, int page = 1)
        {
            ViewBag.SearchString = searchString;

            int decade = -1;

            if (category != null)
            {
                decade = int.Parse(category);
            }

            var movies = repository.Movies;

            if (!String.IsNullOrEmpty(searchString))
            {
                 movies = movies.Where(s => s.Title.Contains(searchString) || s.TitleEN.Contains(searchString));
            }

            MoviesListViewModel model = new MoviesListViewModel
            {
                Movies = movies
                .Where(p => category == null || (p.PremiereDate.Year >= decade && p.PremiereDate.Year < decade + 10))
                .OrderBy(p => p.MovieID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        movies.Count() :
                        movies.Where(e => (e.PremiereDate.Year >= decade && e.PremiereDate.Year < decade + 10)).Count()
                },
                CurrentYearCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int movieId)
        {
            Movie movie = repository.Movies.FirstOrDefault(p => p.MovieID == movieId);
            if (movie != null)
            {
                return File(movie.ImageData, movie.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        

        public ViewResult Main(string category, int page = 1)
        {
            MoviesListViewModel model = new MoviesListViewModel
            {
                Movies = repository.Movies
                .Where(p => category == null || p.PremiereDate.Year.ToString() == category)
                .OrderBy(p => p.MovieID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Movies.Count() :
                        repository.Movies.Where(e => e.PremiereDate.Year.ToString() == category).Count()
                },
                CurrentYearCategory = category
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public EmptyResult AddRating(int movieID, byte rating)
        {
            var userId = User.Identity.GetUserId();

            var usersMovie = repository.UsersMovies.FirstOrDefault(p => p.UserID == userId && p.MovieID == movieID);

            var userMovieId = usersMovie != null ? usersMovie.UserMovieID : 0; 

            if (rating > 0 && rating <= 10)
            {
                if ( repository.Movies.FirstOrDefault(p => p.MovieID == movieID) != null)
                {
                    UsersMovie userMovie = new UsersMovie()
                    {
                        UserMovieID = userMovieId,
                        MovieID = movieID,
                        UserID = userId,
                        Rating = rating
                    };

                    repository.SaveUserMovie(userMovie);
                }
            }
            return null;
        }
        

    }
}