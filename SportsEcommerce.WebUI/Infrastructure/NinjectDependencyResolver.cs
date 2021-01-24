﻿using Moq;
using Ninject;
using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Concrete;
using SportsEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Represents the Core Kernel of the IOC Container
        /// </summary>
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRespository>();
        }
    }
}