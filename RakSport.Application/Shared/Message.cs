namespace RakSport.Application.Shared
{
    public class Message
    {
        public string message { get; set; }
        public object? Content { get; set; }

        public Message(string message)
        {
            this.message = message;
        }

        public Message(string message, object? Content)
        {
            this.message = message;
            this.Content = Content;
        }
    }
}
