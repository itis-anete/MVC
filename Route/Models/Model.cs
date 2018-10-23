using Microsoft.Extensions.Primitives;

namespace Route.Models
{
    public class Model : InfoSystemValueModel
    {
        public string Prop { get; set; }

        public Model(object value) : base(value)
        {
        }
    }
}