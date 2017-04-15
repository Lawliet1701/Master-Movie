using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repository;

        public MovieController(IMovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        public ViewResult List()
        {
            return View(repository.Movies);
        }

    }
}