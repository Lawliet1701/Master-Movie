using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IMovieRepository repository;

        public AdminController(IMovieRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Movies);
        }

        public ViewResult Edit(int movieId)
        {
            Movie movie = repository.Movies
                .FirstOrDefault(p => p.MovieID == movieId);
            return View (movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
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