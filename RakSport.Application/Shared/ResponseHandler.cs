namespace RakSport.Application.Shared
{
    public class ResponseHandler
    {
        public string Message { get; set; }
        public object? Content { get; set; }

        public ResponseHandler(string Message)
        {
            this.Message = Message;
        }

        public ResponseHandler(string Message, object? Content)
        {
            this.Message = Message;
            this.Content = Content;
        }
    }
}
