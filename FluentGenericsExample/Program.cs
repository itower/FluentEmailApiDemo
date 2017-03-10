namespace FluentGenericEmailApiDemo.src
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;
    using System.Security.Policy;
    using System.Threading.Tasks;
    using FluentGenericApiDemo.src.Models;
    using GenericsEmailDemo.src.Models;

    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        static async Task AsyncMain()
        {
            SetAppDomainPermissions();

            await SendCustomWithGenericReturnType();

            Console.WriteLine("Sending hello email with template");


            var emailCourier = new EmailCourier();

            await emailCourier.To("fakefluentapi@gmail.com")
                                              .Cc("fakefluentapi@gmail.com")
                                              .From("fakefluentapi@gmail.com")
                                              .Subject("Hello")
                                              .WithTemplate(AppDomain.CurrentDomain.BaseDirectory, "/src/Emails/HelloEmail.cshtml")
                                              .Send(new HelloEmailBody() { Name = "Test Name" });

            Console.WriteLine("Email Sent...");

            Console.WriteLine("Sending fluent email with template");

            await emailCourier.To("fakefluentapi@gmail.com")
                                  .Cc("itower99@gmail.com")
                                  .From("fakefluentapi@gmail.com")
                                  .Subject("Hello")
                                  .WithTemplate(AppDomain.CurrentDomain.BaseDirectory, "/src/Emails/FluentEmailHelp.cshtml")
                                  .Send(new FluentEmailBody() { Number = 123453 });

            Console.WriteLine("Email Sent...");

            await SendCustomWithNoReturnType();

            await SendBulk();

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }


        public static async Task SendBulk()
        {
            var emailCourier = new EmailCourier();
            var emails = new List<EmailSendBulk>()
            {
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com" },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com" },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com" },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  },
                                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com" },
                new EmailSendBulk() {Cc ="fakefluentapi@gmail.com", From ="fakefluentapi@gmail.com", Name="Hello", Subject= "Hello", To = "fakefluentapi@gmail.com"  }
            };

            await emailCourier.WithTemplateForBulk(AppDomain.CurrentDomain.BaseDirectory, "/src/Emails/HelloEmail.cshtml").SendBulk(emails);
        }

        private static async Task SendCustomWithNoReturnType()
        {
            var customEmail = new CustomEmail() { Name = "Custom Test" };

            var emailCourier = new EmailCourier();

            await emailCourier.To("fakefluentapi@gmail.com")
                                              .Cc("fakefluentapi@gmail.com")
                                              .From("fakefluentapi@gmail.com")
                                              .Subject("Hello")
                                              .WithTemplate(AppDomain.CurrentDomain.BaseDirectory, "/src/Emails/HelloEmail.cshtml")
                                              .Send((message) =>
                                              {
                                                  LogToConsole(message);
                                              }, customEmail);
        }

        private static void LogToConsole(CustomEmail email)
        {
            Console.WriteLine(string.Format("The custom email for {0} action executed...", email.Name));
        }

        private static async Task<CustomEmailResult> SendCustomWithGenericReturnType()
        {
            var customEmail = new CustomEmail() { Name = "Custom Test" };

            var emailCourier = new EmailCourier();

            return await emailCourier.To("fakefluentapi@gmail.com")
                                              .Cc("fakefluentapi@gmail.com")
                                              .From("fakefluentapi@gmail.com")
                                              .Subject("Hello")
                                              .WithTemplate(AppDomain.CurrentDomain.BaseDirectory, "/src/Emails/HelloEmail.cshtml").Send(input => 
                                              {
                                                  // Do custom work after send here
                                                  var result = new CustomEmailResult() {IsSuccess = true };

                                                  return result;
                                              }, customEmail);
        }

        private static void SetAppDomainPermissions()
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                // RazorEngine cannot clean up from the default appdomain...
                Console.WriteLine("Switching to second AppDomain, for RazorEngine...");
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var current = AppDomain.CurrentDomain;
                // You only need to add strongnames when your appdomain is not a full trust environment.
                var strongNames = new StrongName[0];

                var domain = AppDomain.CreateDomain(
                    "MyMainDomain", null,
                    current.SetupInformation, new PermissionSet(PermissionState.Unrestricted),
                    strongNames);
                var exitCode = domain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location);
                // RazorEngine will cleanup. 
                AppDomain.Unload(domain);
            }
        }
    }
}
