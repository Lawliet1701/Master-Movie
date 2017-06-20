using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class MoviePredict
    {
        public Movie Movie { get; set; }
        public double Prediction { get; set; }
    }
}