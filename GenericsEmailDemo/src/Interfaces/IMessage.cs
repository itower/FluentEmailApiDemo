namespace FluentGenericApiDemo.src.Interfaces
{
    public interface IMessage
    {
        string To { get; }

        string From { get; }

        string Cc { get; }

        string Subject { get; }
    }
}
