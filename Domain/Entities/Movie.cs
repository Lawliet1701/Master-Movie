using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Movie
    {
        [HiddenInput(DisplayValue = false) ]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "Please enter a movie title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a movie origin title")]
        public string TitleEN { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a movie description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a movie premiere date")]
        public DateTime PremiereDate { get; set; }

        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Please enter a positive value")]
        public Int16 Length { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a movie slogan")]
        public string Slogan { get; set; }

        [Required(ErrorMessage = "Please enter a movie age limit")]
        public string RatingAgeLimit { get; set; }

        [Required(ErrorMessage = "Please enter a movie budget")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive value")]
        public int Budget { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive id")]
        public int KPID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive rating")]
        public decimal RatingKP { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter a positive rating")]
        public decimal RatingIMDB { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive rating")]
        public int RatingMetascore { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal Rating { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalViews { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
