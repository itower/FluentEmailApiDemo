namespace GenericsEmailDemo.src.Models
{
    using System;
    using FluentEmailApiDemo.src.Interfaces;

    public partial class EmailSendBulk : IEmailSendBulk
    {
        private static readonly Guid _guid = Guid.NewGuid();
        public string Cc { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public Guid guid { get { return _guid;} }

        public string Name { get; set; }
    }
}
