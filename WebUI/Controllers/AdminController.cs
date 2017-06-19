using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IMovieRepository repository;

        public AdminController(IMovieRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index(string sortOrder)
        {
            var movies = repository.Movies;

            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) || sortOrder == "id_desc" ? "id" : "id_desc";
            ViewBag.TitleSortParam = sortOrder == "title" ? "title_desc" : "title";
            ViewBag.LengthSortParam = sortOrder == "length" ? "length_desc" : "length";
            ViewBag.AgeLimitSortParam = sortOrder == "agelimit" ? "agelimit_desc" : "agelimit";
            ViewBag.BudgetSortParam = sortOrder == "budget" ? "budget_desc" : "budget";
            ViewBag.KPSortParam = sortOrder == "kp" ? "kp_desc" : "kp";
            ViewBag.IMDBSortParam = sortOrder == "imdb" ? "imdb_desc" : "imdb";
            ViewBag.PremiereDateSortParam = sortOrder == "premiere" ? "premiere_desc" : "premiere";

            switch(sortOrder)
            {
                case "title": movies = movies.OrderBy(s => s.Title); break;
                case "title_desc": movies = movies.OrderByDescending(s => s.Title); break;
                case "id": movies = movies.OrderBy(s => s.MovieID); break;
                case "id_desc": movies = movies.OrderByDescending(s => s.MovieID); break;
                case "length": movies = movies.OrderBy(s => s.Length); break;
                case "length_desc": movies = movies.OrderByDescending(s => s.Length); break;
                case "agelimit": movies = movies.OrderBy(s => s.RatingAgeLimit); break;
                case "agelimit_desc": movies = movies.OrderByDescending(s => s.RatingAgeLimit); break;
                case "budget": movies = movies.OrderBy(s => s.Budget); break;
                case "budget_desc": movies = movies.OrderByDescending(s => s.Budget); break;
                case "kp": movies = movies.OrderBy(s => s.RatingKP); break;
                case "kp_desc": movies = movies.OrderByDescending(s => s.RatingKP); break;
                case "imdb": movies = movies.OrderBy(s => s.RatingIMDB); break;
                case "imdb_desc": movies = movies.OrderByDescending(s => s.RatingIMDB); break;
                case "premiere": movies = movies.OrderBy(s => s.PremiereDate); break;
                case "premiere_desc": movies = movies.OrderByDescending(s => s.PremiereDate); break;
            }

            return View(movies);
        }

        public ViewResult Edit(int movieId)
        {
            Movie movie = repository.Movies
                .FirstOrDefault(p => p.MovieID == movieId);
            return View (movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    movie.ImageMimeType = image.ContentType;
                    movie.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(movie.ImageData, 0, image.ContentLength);
                }
                repository.SaveMovie(movie);
                TempData["message"] = string.Format("{0} has been saved", movie.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(movie);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Movie());
        }

        [HttpPost]
        public ActionResult Delete(int movieId)
        {
            Movie deletedMovie = repository.DeleteMovie(movieId);
            if (deletedMovie != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedMovie.Title);
            }
            return RedirectToAction("Index");
        }
    }
}