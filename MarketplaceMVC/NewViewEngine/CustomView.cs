using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceMVC.NewViewEngine
{
    public class CustomView : IView
    {
        public CustomView(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public Task RenderAsync(ViewContext context)
        {
            //\(-_-)/?
            throw new System.NotImplementedException();
        }
    }
}
