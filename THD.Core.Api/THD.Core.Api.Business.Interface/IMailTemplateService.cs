using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IMailTemplateService
    {
        Task<bool> MailTemplate1Async(int DocId, string rptBase64);
        Task<bool> MailTemplate2Async(string ProjectNumber, string rptBase64);
        Task<bool> MailTemplate3Async(ModelMenuC1 model, string rptBase64);
        Task<bool> MailTemplate4Async(string ProjectNumber, string rptBase64);
        Task<bool> MailTemplate5Async(string ProjectNumber, string rptBase64);
        Task<bool> MailTemplate6Async(ModelMenuC3 model, string rptBase64);
        Task<bool> MailTemplate7Async(ModelCloseMeeting model, string rptBase64);
        Task<bool> MailTemplate8Async(string ProjectNumber, string rptBase64);
        Task<bool> MailTemplate9Async(string ProjectNumber, string rptBase64);

    }
}
