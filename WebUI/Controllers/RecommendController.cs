using Domain.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class RecommendController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private IMovieRepository repository;

        public RecommendController(IMovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        public RecommendController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        public ActionResult Index()
        {

           var currentUser = UserManager.FindById(User.Identity.GetUserId());

            var UsersMovies = repository.UsersMovies.ToList();
            var Users = UserManager.Users.ToList();

            Dictionary<int, byte> currentUsersRatings = UsersMovies.Where(u => u.UserID == currentUser.Id).ToDictionary(x => x.MovieID, x => x.Rating);

            List<Dictionary<int, byte>> allUsersRatings = new List<Dictionary<int, byte>>();

            var allUsers = Users.Where(u => u.Id != currentUser.Id).Select(u => u.Id);

            foreach (string id in allUsers)
            {
                allUsersRatings.Add(UsersMovies.Where(u => u.UserID == id).ToDictionary(x => x.MovieID, x => x.Rating));
            }



            double totalSumA = 0;

            foreach (KeyValuePair<int, byte> pair in currentUsersRatings)
            {
                totalSumA += Math.Pow(pair.Value, 2);
            }

            totalSumA = Math.Sqrt(totalSumA);



            foreach (Dictionary<int, byte> dict in allUsersRatings)
            {

                double totalSumB = 0;

                foreach (KeyValuePair<int, byte> innerPair in dict)
                {
                    totalSumB += Math.Pow(innerPair.Value, 2);
                }

                totalSumB = Math.Sqrt(totalSumB);

                var sum = 0;

                foreach (KeyValuePair<int, byte> outerPair in currentUsersRatings)     // ratings of current user
                {

                    if (dict.ContainsKey(outerPair.Key))
                    {
                        sum += outerPair.Value * dict[outerPair.Key];
                    }

                }

                double similarity = sum / (totalSumA + totalSumB);

                Debug.WriteLine("SIM : " + similarity );

            }

            return View();
        }

    }
}