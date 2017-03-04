namespace FluentGenericApiDemo.src.Models
{
    using Interfaces;
    public class Message : IMessage
    {
        public string Cc { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }
    }
}
