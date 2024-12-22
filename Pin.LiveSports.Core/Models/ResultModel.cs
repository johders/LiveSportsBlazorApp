namespace Pin.LiveSports.Core.Models
{
    public class ResultModel<T> : BaseResult
    {
        public T Data { get; set; }
    }
}
