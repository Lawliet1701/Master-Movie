using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string TitleEN { get; set; }
        public string Description { get; set; }
        public DateTime PremiereDate { get; set; }
        public Int16 Length { get; set; }
        public string Slogan { get; set; }
        public string RatingAgeLimit { get; set; }
        public int Budget { get; set; }
        public int KPID { get; set; }
        public decimal RatingKP { get; set; }
        public decimal RatingIMDB { get; set; }
        public int RatingMetascore { get; set; }
        public decimal Rating { get; set; }
        public int TotalViews { get; set; }
    }
}
