using Cannabis.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cannabis.Routing
{
    public class ActionInvoker : IActionInvoker
    {
        private readonly ControllerContext _controllerContext;

        public ActionInvoker(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext ?? throw new ArgumentNullException(nameof(controllerContext));
        }

        public Task InvokeAsync()
        {
            var method = _controllerContext.ActionDescriptor.MethodInfo;
            var controller = (object)null;
            var parameters = (object)null;
            throw new NotImplementedException();
        }
    }
}
