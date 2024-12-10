namespace Core
{
    public class APIException : Exception
    {
        public int StatusCode { get; init; }

        public APIException(int status, string message)
            : base(message)
        {
            this.StatusCode = status;
        }

    }
}
