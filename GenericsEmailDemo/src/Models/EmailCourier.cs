namespace FluentGenericApiDemo.src.Models
{
    using System.Threading.Tasks;
    using Interfaces;
    public class EmailCourier : ICourier
    {
        private readonly IEmailConfig _emailConfig;
        public EmailCourier(IEmailConfig emailConfig)
        {
            this._emailConfig = emailConfig;
        }

        public EmailCourier WithTemplate(string templatePath)
        {
            // Compile template here
            return this;
        }

        public async Task<EmailCourier> Send<T>(IMessage message, T body)
        {
            // Send message here
            return await Task.Run(() =>
            {
                return this;
            });
        }
    }
}
