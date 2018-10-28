using Newtonsoft.Json;

namespace Cannabis.ActionResults
{
    public class JsonResult<T> : DataResult<T>
    {
        public JsonResult(T data) : base(data) { }

        protected override string DataAsString => JsonConvert.SerializeObject(Data);
        protected override string ContentType => "application/json";
    }

    public static class JsonResult
    {
        public static JsonResult<T> Create<T>(T data) => new JsonResult<T>(data);
    }
}
