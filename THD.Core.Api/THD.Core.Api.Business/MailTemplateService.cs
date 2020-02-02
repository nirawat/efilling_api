using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Business.Interface;
using THD.Core.Api.Models;
using THD.Core.Api.Models.Config;
using THD.Core.Api.Repository.Interface;

namespace THD.Core.Api.Business
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IMailTemplateRepository _IMailTemplateRepository;

        public MailTemplateService(
            IEnvironmentConfig EnvironmentConfig,
            IMailTemplateRepository MailTemplateRepository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IMailTemplateRepository = MailTemplateRepository;
        }


        public async Task<bool> MailTemplate1Async(ModelMenuB1 model)
        {
            return await _IMailTemplateRepository.MailTemplate1Async(model);
        }



    }
}
