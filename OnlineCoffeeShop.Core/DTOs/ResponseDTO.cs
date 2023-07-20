namespace OnlineCoffeeShop.Core.DTOs
{
    public class ResponseDTO<T>
    {
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T? Data { get; set; }

        public static ResponseDTO<T> Fail(IEnumerable<string> errors)
        {
            return new ResponseDTO<T>
            {
                Errors = errors
            };
        }

        public static ResponseDTO<T> Success(T data)
        {
            return new ResponseDTO<T>
            {
                Data = data
            };
        }
    }
}
