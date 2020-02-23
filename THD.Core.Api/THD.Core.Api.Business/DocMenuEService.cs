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

namespace THD.Core.Api.Business
{
    public class DocMenuEService : IDocMenuEService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuE1Repository _IDocMenuE1Repository;

        public DocMenuEService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuE1Repository DocMenuE1Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuE1Repository = DocMenuE1Repository;
        }

        #region Menu E1

        public async Task<ModelMenuE1_InterfaceData> MenuE1InterfaceDataAsync(string RegisterId, string Passw)
        {
            return await _IDocMenuE1Repository.MenuE1InterfaceDataAsync(RegisterId, Passw);
        }

        public async Task<ModelResponseMessage> AddDocMenuE1Async(ModelMenuE1 model)
        {

            ModelResponseMessage resp = new ModelResponseMessage();

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docDate = DateTime.Now;

            resp = await _IDocMenuE1Repository.AddDocMenuE1Async(model);

            return resp;
        }

        public async Task<ModelMenuE1_InterfaceReportData> MenuE1InterfaceReportDataAsync(ModelMenuE1_InterfaceReportData search)
        {
            return await _IDocMenuE1Repository.MenuE1InterfaceReportDataAsync(search);
        }

        #endregion


    }









}
