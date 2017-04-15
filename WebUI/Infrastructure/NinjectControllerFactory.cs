using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(new List<Movie> {
                new Movie { Title = "Аватар", Description = "Bla-Bla-Bla" },
                new Movie { Title = "Город Грехов", Description = "Bla-Bla-Bla" },
                new Movie { Title = "Матрица", Description = "Bla-Bla-Bla" }
                }.AsQueryable());
            ninjectKernel.Bind<IMovieRepository>().ToConstant(mock.Object);
        }
    }
}