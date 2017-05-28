using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repository;
        public int PageSize = 3;

        public MovieController(IMovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        public ViewResult List(string category, int page = 1)
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
        

    }
}