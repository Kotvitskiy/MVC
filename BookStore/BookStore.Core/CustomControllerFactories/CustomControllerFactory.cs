using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Ninject;
using System.Configuration;

namespace Store.Core.CustomControllerFactories
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        public CustomControllerFactory()
        {

        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = null;

            Type controllerType = null;
            
            switch(controllerName)
            {
                case "Test":
                    controller = DependencyResolver.Current.GetService<ITestSwitcher>() as IController;
                    break;
                default:
                    controllerType = this.GetControllerType(requestContext, controllerName);
                    controller = GetControllerInstance(requestContext, controllerType);
                    break;
            }
            return (IController)controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
