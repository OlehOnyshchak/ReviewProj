using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using Ninject;
using Moq;
using System.Linq;

namespace ReviewProj.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //Mock<IEnterpriseRepository> mock = new Mock<IEnterpriseRepository>();
            //mock.Setup(m => m.Enterprises).Returns(new List<Enterprise>
            //{
            //    new Enterprise { Name = "Enterprise", Description = "Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   Description...   " },
            //    new Enterprise {Name = "Enterprise 2", Description = "Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2...Description 2..." }

            //}.AsQueryable());
            //ninjectKernel.Bind<IEnterpriseRepository>().ToConstant(mock.Object);

            ninjectKernel.Bind<IEnterpriseRepository>().To<EnterpriseRepository>();
            ninjectKernel.Bind<IReviewerRepository>().To<ReviewerRepository>();
            ninjectKernel.Bind<IReviewRepository>().To<ReviewRepository>();
        }
    }
}