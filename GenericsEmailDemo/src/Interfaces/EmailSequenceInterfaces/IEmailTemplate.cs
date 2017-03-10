namespace GenericsEmailDemo.src.Interfaces.EmailSequenceInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailTemplate
    {
        ISend WithTemplate(string templatePath);
    }
}
