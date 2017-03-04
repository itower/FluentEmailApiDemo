namespace FluentGenericEmailApiDemo.src
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using FluentGenericApiDemo.src.Interfaces;
    using FluentGenericApiDemo.src.Models;
    using SimpleInjector;
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        static async Task AsyncMain()
        {
            var container = new Container();
            container.Register<IEmailConfig>(() => CreateEmailConfig());
            container.Register<ICourier, EmailCourier>();

            var emailCourier = container.GetInstance<ICourier>();

            Console.WriteLine("Sending email with template");

            var isSuccess = await emailCourier.WithTemplate("path").Send(new Message(), new EmailBody());

            await Task.Delay(2000);

            Console.WriteLine("Email Sent...");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static EmailConfig CreateEmailConfig()
        {
            return new EmailConfig()
            {
                Host = ConfigurationManager.AppSettings.Get("host"),
                IsSsl = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isSsl")),
                Password = ConfigurationManager.AppSettings.Get("password"),
                Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("port")),
                Username = ConfigurationManager.AppSettings.Get("username")
            };
        }
    }
}
