using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IMailTemplateService
    {
        Task<bool> MailTemplate1Async(ModelMenuB1 model);


    }
}
