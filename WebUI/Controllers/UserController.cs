using Domain.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private IMovieRepository repository;

        public UserController(IMovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize]
        public ViewResult RatingList()
        {
            var movies = repository.Movies;

            var currentUser = UserManager.FindById(User.Identity.GetUserId());

            Dictionary<int, byte> currentUsersRatings = repository.UsersMovies.Where(u => u.UserID == currentUser.Id).ToDictionary(x => x.MovieID, x => x.Rating);

            List<RatingListViewModel> ratingList = new List<RatingListViewModel>();

            foreach (KeyValuePair<int, byte> pair in currentUsersRatings)
            {
                RatingListViewModel item = new RatingListViewModel()
                {
                    Movie = movies.FirstOrDefault(p => p.MovieID == pair.Key),
                    Rating = pair.Value
                };

                ratingList.Add(item);
            }

            return View(ratingList);
        }
    }
}