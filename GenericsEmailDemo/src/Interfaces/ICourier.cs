namespace FluentGenericApiDemo.src.Interfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface ICourier
    {
        EmailCourier WithTemplate(string templatePath);

        Task<EmailCourier> Send<T>(IMessage message, T body);
    }
}
