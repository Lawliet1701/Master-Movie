using Domain.Abstract;
using Domain.Entities;
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

            //List<Dictionary<int, byte>> allUsersRatings = new List<Dictionary<int, byte>>();

            List<User> allUsers = Users.Where(u => u.Id != currentUser.Id).Select(u => new User { UserID = u.Id, UserName = u.UserName}).ToList();

            foreach (User user in allUsers)
            {
                user.Ratings = UsersMovies.Where(u => u.UserID == user.UserID).ToDictionary(x => x.MovieID, x => x.Rating);
            }


            double totalSumA = 0;

            foreach (KeyValuePair<int, byte> pair in currentUsersRatings)
            {
                totalSumA += Math.Pow(pair.Value, 2);
            }

            totalSumA = Math.Sqrt(totalSumA);


            double totalSim = 0;

            Dictionary<int, double> prediction = new Dictionary<int, double>();

            foreach (User user in allUsers)
            {

                double totalSumB = 0;

                foreach (KeyValuePair<int, byte> innerPair in user.Ratings)
                {
                    totalSumB += Math.Pow(innerPair.Value, 2);
                }

                totalSumB = Math.Sqrt(totalSumB);

                var sum = 0;

                foreach (KeyValuePair<int, byte> outerPair in currentUsersRatings)     // ratings of current user
                {

                    if (user.Ratings.ContainsKey(outerPair.Key))
                    {
                        sum += outerPair.Value * user.Ratings[outerPair.Key];
                    }

                }

                double similarity = sum / (totalSumA * totalSumB);

                Debug.WriteLine(user.UserName + " : " + similarity);


                if (similarity > 0.1)                                             // filtering users by similarity threshold
                {

                    totalSim += similarity;


                    foreach (KeyValuePair<int, byte> pair in user.Ratings)
                    {
                        double predValue = pair.Value * similarity;

                        if (prediction.ContainsKey(pair.Key))                    // if this movie already exists, add value  
                        {
                            prediction[pair.Key] += predValue;
                        }
                        else if (!currentUsersRatings.ContainsKey(pair.Key))     // if current user didn't see this movie
                        {
                            prediction.Add(pair.Key, predValue);
                        }

                    }

                }

            }

            var keys = new List<int>(prediction.Keys);
            foreach (int key in keys)
            {
                prediction[key] /= totalSim;
            }
                                                                                    // sort predictions by value
            var items = from pair in prediction                
                        orderby pair.Value descending
                        select pair;

            //foreach (KeyValuePair<int, double> pair in items)
            //{
            //    if (pair.Value > 6)
            //    {
            //        Debug.WriteLine("{0} : {1}", pair.Key, pair.Value);
            //    }
            //}

            List<MoviePredict> result = new List<MoviePredict>();

            foreach (KeyValuePair<int, double> pair in items)
            {
                if (pair.Value > 5)
                {

                    MoviePredict mp = new MoviePredict()
                    {
                        Movie = (Movie)repository.Movies.Where(s => s.MovieID == pair.Key).FirstOrDefault(),
                        Prediction = pair.Value
                    };

                    result.Add(mp);
                }
                
            }

            return View(result);
        }

    }
}