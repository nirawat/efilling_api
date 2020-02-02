using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IMailTemplateRepository
    {
        Task<bool> MailTemplate1Async(ModelMenuB1 model);
        //Task<string> MailTemplate2Async(int DocId);
        //Task<string> MailTemplate3Async(int DocId);
        //Task<string> MailTemplate4Async(int DocId);
        //Task<string> MailTemplate5Async(int DocId);
        //Task<string> MailTemplate6Async(int DocId);
        //Task<string> MailTemplate7Async(int DocId);

    }
}
