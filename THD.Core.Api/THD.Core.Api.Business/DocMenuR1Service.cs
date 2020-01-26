using System;
using System.Collections.Generic;
using THD.Core.Api.Business.Interface;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using THD.Core.Api.Models;
using THD.Core.Api.Repository.Interface;
using System.Text;
using THD.Core.Api.Helpers;
using System.IO;
using static THD.Core.Api.Helpers.ServerDirectorys;
using THD.Core.Api.Models.Config;
using System.Globalization;
using THD.Core.Api.Repository.DataHandler;

namespace THD.Core.Api.Business
{
    public class DocMenuRService : IDocMenuRService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuR1Repository _IDocMenR1Repository;

        public DocMenuRService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuR1Repository DocMenuR1Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenR1Repository = DocMenuR1Repository;
        }

        #region Menu R1

        public async Task<ModelMenuR1_InterfaceData> MenuR1InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenR1Repository.MenuR1InterfaceDataAsync(RegisterId);
        }

        public async Task<IList<ModelMenuR1Data>> GetAllReportHistoryDataR1Async(ModelMenuR1_InterfaceData model)
        {
            return await _IDocMenR1Repository.GetAllReportHistoryDataR1Async(model);
        }

        #endregion


    }
}
