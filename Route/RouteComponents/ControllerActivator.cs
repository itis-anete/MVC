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
    public class ControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            throw new NotImplementedException();
        }

        public void Release(ControllerContext context, object controller)
        {
            throw new NotImplementedException();
        }
    }
}
