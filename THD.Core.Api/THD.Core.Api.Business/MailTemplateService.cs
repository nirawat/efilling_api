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


        public async Task<bool> MailTemplate1Async(int DocId, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate1Async(DocId, rptBase64);
        }

        public async Task<bool> MailTemplate2Async(string ProjectNumber, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate2Async(ProjectNumber, rptBase64);
        }

        public async Task<bool> MailTemplate3Async(ModelMenuC1 model, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate3Async(model, rptBase64);
        }

        public async Task<bool> MailTemplate4Async(string ProjectNumber, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate4Async(ProjectNumber, rptBase64);
        }

        public async Task<bool> MailTemplate5Async(string ProjectNumber, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate5Async(ProjectNumber, rptBase64);
        }

        public async Task<bool> MailTemplate6Async(ModelMenuC3 model, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate6Async(model, rptBase64);
        }

        public async Task<bool> MailTemplate7Async(string round, string year, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate7Async(round, year, rptBase64);
        }

        public async Task<bool> MailTemplate8Async(string ProjectNumber, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate8Async(ProjectNumber, rptBase64);
        }

        public async Task<bool> MailTemplate9Async(string ProjectNumber, string rptBase64)
        {
            return await _IMailTemplateRepository.MailTemplate9Async(ProjectNumber, rptBase64);
        }


    }
}
