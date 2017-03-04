namespace FluentGenericApiDemo.src.Models
{
    using Interfaces;

    public class EmailConfig : IEmailConfig
    {
        public string Host { get; set; }

        public bool IsSsl { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }
    }
}
