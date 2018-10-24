using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Route
{
    public class InfoSystemView : IView
    {
        public InfoSystemView(string path)
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