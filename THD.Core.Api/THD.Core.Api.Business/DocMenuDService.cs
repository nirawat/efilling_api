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
    public class DocMenuDService : IDocMenuDService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuD1Repository _IDocMenuD1Repository;
        private readonly IDocMenuD2Repository _IDocMenuD2Repository;

        public DocMenuDService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuD1Repository DocMenuD1Repository,
            IDocMenuD2Repository DocMenuD2Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuD1Repository = DocMenuD1Repository;
            _IDocMenuD2Repository = DocMenuD2Repository;
        }

        #region MenuD1

        public async Task<ModelMenuD1_InterfaceData> MenuD1InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuD1Repository.MenuD1InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuD1ProjectNumberData> GetProjectNumberWithDataD1Async(string project_number)
        {
            return await _IDocMenuD1Repository.GetProjectNumberWithDataD1Async(project_number);

        }

        public async Task<ModelResponseD1Message> AddDocMenuD1Async(ModelMenuD1 model)
        {

            ModelResponseD1Message resp = new ModelResponseD1Message();

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;

            resp = await _IDocMenuD1Repository.AddDocMenuD1Async(model);

            return resp;
        }

        #endregion

        #region "MenuD1 Edit"

        public async Task<ModelMenuD1_InterfaceData> MenuD1EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuD1Repository.MenuD1EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelResponseD1Message> UpdateDocMenuD1EditAsync(ModelMenuD1 model)
        {

            ModelResponseD1Message resp = new ModelResponseD1Message();

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;

            resp = await _IDocMenuD1Repository.UpdateDocMenuD1EditAsync(model);

            return resp;
        }

        #endregion

        #region MenuD2

        public async Task<ModelMenuD2_InterfaceData> MenuD2InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuD2Repository.MenuD2InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuD2ProjectNumberData> GetProjectNumberWithDataD2Async(string project_number)
        {
            return await _IDocMenuD2Repository.GetProjectNumberWithDataD2Async(project_number);

        }
        public async Task<ModelMenuD2_FileDownload> GetAllDownloadFileByFileNameAsync(string filename)
        {
            ModelMenuD2_FileDownload resp = new ModelMenuD2_FileDownload();

            if (!string.IsNullOrEmpty(filename))
            {
                resp.filename = filename;
                resp.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA7, filename);
                return resp;
            }
            else return null;
        }

        public async Task<ModelResponseD2Message> AddDocMenuD2Async(ModelMenuD2 model)
        {

            ModelResponseD2Message resp = new ModelResponseD2Message();


            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;
            model.agendameetingdate = Convert.ToDateTime(model.agendameetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");
            model.conclusiondate = DateTime.Now.ToString("yyyy-MM-dd");

            resp = await _IDocMenuD2Repository.AddDocMenuD2Async(model);

            return resp;
        }

        #endregion

    }









}
