namespace Cannabis.Models
{
    public class ErrorViewModel : CannabisValueModel
    {
        public CannabisValue RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId?.Value as string);
    }
}