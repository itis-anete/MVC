using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MarketplaceMVC.MarketplaceViewEngine
{
    public class MarketplaceView : IView
    {
        public MarketplaceView(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public Task RenderAsync(ViewContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
