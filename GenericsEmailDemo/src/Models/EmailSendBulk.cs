namespace GenericsEmailDemo.src.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentEmailApiDemo.src.Interfaces;
    using GenericsEmailDemo.src.Interfaces;
    public class EmailSendBulk : IEmailSendBulk
    {
        private static readonly Guid _guid = Guid.NewGuid();
        public string Cc { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public string Name { get; set; }

        public Guid guid { get { return _guid;} }
    }
}
