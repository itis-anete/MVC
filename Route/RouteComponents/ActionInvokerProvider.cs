﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route
{
    public class ActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => throw new NotImplementedException();

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }
    }
}
