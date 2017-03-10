namespace GenericsEmailDemo.src.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using FluentEmailApiDemo.src.Interfaces;
    using FluentGenericApiDemo.src.Interfaces;
    using FluentGenericApiDemo.src.Models;
    using Interfaces.EmailSequenceInterfaces;
    using RazorEngine;
    using RazorEngine.Templating;
    using MoreLinq;
    using System.Threading.Tasks;
    using System.Threading;

    public class EmailCourier :
                               ISendBulk,
                               IEmailToSequence,
                               IEmailFromSequence,
                               IEmailSubjectSequence,
                               IEmailCcSequence,
                               IEmailTemplate,
                               ISend,
                               ISendBulkEmail
    {
        private static readonly IEmailConfig _emailConfig;

        private static EmailCourier _courier;

        private readonly Guid _templateKey = Guid.NewGuid();
        private string _path;
        private string _template;

        private string _to;
        private string _from;
        private string _subject;
        private string _cc;

        static EmailCourier()
        {
            _courier = new EmailCourier();
            _emailConfig = CreateEmailConfig();
        }


        public ISendBulkEmail WithTemplateForBulk(string templatePath, string templateFilename)
        {
            this._path = templatePath;
            this._template = templateFilename;
            using (var sr = new StreamReader(string.Format("{0}/{1}", templatePath, templateFilename)))
            {
                this._template = sr.ReadToEnd();
            }

            return this;
        }

        public IEmailCcSequence To(string to)
        {
            this._to = to;
            return this;
        }

        public IEmailSubjectSequence From(string from)
        {
            this._from = from;

            return this;
        }

        public IEmailTemplate Subject(string subject)
        {
            this._subject = subject;

            return this;
        }

        public IEmailFromSequence Cc(string cc)
        {
            this._cc = cc;

            return this;
        }
        public ISend WithTemplate(string templatePath, string templateFilename)
        {
            this._path = templatePath;
            this._template = templateFilename;
            using (var sr = new StreamReader(string.Format("{0}/{1}", templatePath, templateFilename)))
            {
                this._template = sr.ReadToEnd();
            }

            return this;
        }

        public async Task Send<T>(T message)
        {
            // Send message here
            try
            {
                using (var client = GetSmtpClient())
                {

                    var mail = new MailMessage(new MailAddress(this._from), new MailAddress(this._to))
                    {
                        Body = CompileTemplate<T>(message),
                        IsBodyHtml = true,
                        Sender = new MailAddress(this._from),
                        Subject = this._subject,
                    };

                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public async Task<TOut> Send<TIn, TOut>(Func<TIn, TOut> customFunction, TIn input)
        {
            await this.Send(input);

            return customFunction(input);
        }

        public async Task SendBulk<T>(IEnumerable<T> enumerableData) where T : IEmailSendBulk
        {
            try
            {
                var bulk = new List<MailMessage>();
                foreach (var email in enumerableData)
                {
                    var mail = new MailMessage(new MailAddress(email.From), new MailAddress(email.To))
                    {
                        Body = CompileTemplate<T>(email, email.guid.ToString()),
                        IsBodyHtml = true,
                        Sender = new MailAddress(email.From),
                        Subject = email.Subject,
                    };

                    bulk.Add(mail);
                }

                await Task.Run(() => Parallel.ForEach(bulk, (message) =>
                {
                    using (var client = GetSmtpClient())
                    {
                        client.Send(message);
                    }
                }));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public async Task Send<TIn>(Action<TIn> customVoidMethod, TIn inputData)
        {
            await Task.Run(() =>
            {
                customVoidMethod(inputData);
            });
        }
        #region Private Methods
        private static SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = _emailConfig.Host,
                Port = _emailConfig.Port,
                UseDefaultCredentials = false,
                EnableSsl = _emailConfig.IsSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential()
                {
                    Password = _emailConfig.Password,
                    UserName = _emailConfig.Username,
                }
            };
        }

        private bool IsTemplateCached<T>(T type)
        {
            return Engine.Razor.IsTemplateCached(_templateKey.ToString(), typeof(T));
        }

        private string CompileTemplate<T>(T type, string guid = null)
        {
            // Guid logic needs to change for bulk
            if (IsTemplateCached(type))
            {
                return Engine.Razor.Run(guid ?? _templateKey.ToString(), typeof(T), type, null);
            }

            return Engine.Razor.RunCompile(this._template, guid ?? _templateKey.ToString(), typeof(T), type, null);
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
        #endregion
    }
}
