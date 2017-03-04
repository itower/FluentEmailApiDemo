namespace FluentGenericApiDemo.src.Interfaces
{
    public interface IEmailConfig
    {
        int Port { get; }

        string Host { get; }

        string Username { get; }

        string Password { get; }

        bool IsSsl { get; }
    }
}
