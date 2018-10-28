namespace Route.Models
{
    public class MyModel
    {
        [InfoSystemValidValue(typeof(int))]
        public InfoSystemValue intValue { get; set; }
    }
}