namespace Cannabis.ActionResults
{
    public class RawResult<T> : DataResult<T>
    {
        public RawResult(T data) : base(data) { }

        protected override string DataAsString => Data.ToString();
        protected override string ContentType => "text/plain";
    }

    public static class RawResult
    {
        public static RawResult<T> Create<T>(T data) => new RawResult<T>(data);
    }
}
