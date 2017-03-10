namespace GenericsEmailDemo.src.Interfaces.EmailSequenceInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentEmailApiDemo.src.Interfaces;

    public interface ISendBulk
    {
        ISendBulkEmail WithTemplateForBulk(string templatePath, string templateFilename);
    }

    public interface IEmailToSequence
    {
        IEmailCcSequence To(string to);
    }

    public interface IEmailFromSequence
    {
        IEmailSubjectSequence From(string from);
    }

    public interface IEmailSubjectSequence
    {
        IEmailTemplate Subject(string subject);
    }

    public interface IEmailCcSequence
    {
        IEmailFromSequence Cc(string cc);
    }

    public interface ISend
    {
        Task Send<T>(T message);
        Task<TOut> Send<TIn, TOut>(Func<TIn, TOut> customFunction, TIn input);
        Task Send<TIn>(Action<TIn> customVoidMethod, TIn inputData);
    }

    public interface ISendBulkEmail
    {
        Task SendBulk<T>(IEnumerable<T> enumerableData) where T : IEmailSendBulk;
    }

    public interface IEmailTemplate
    {
        ISend WithTemplate(string templatePath, string templateFileName);
    }
}
