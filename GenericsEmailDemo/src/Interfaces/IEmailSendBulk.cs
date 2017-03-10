namespace FluentEmailApiDemo.src.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailSendBulk
    {
        string To { get; }

        string From { get; }

        string Subject { get; }

        string Cc { get; }

        string Name { get; }

        Guid guid { get; }
    }
}
