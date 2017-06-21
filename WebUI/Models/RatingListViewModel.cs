using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class RatingListViewModel
    {
        public Movie Movie { get; set; }
        public byte Rating { get; set; }
    }
}