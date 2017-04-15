using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFMovieRepository : IMovieRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Movie> Movies
        {
            get { return context.Movies; }
        }
    }
}
