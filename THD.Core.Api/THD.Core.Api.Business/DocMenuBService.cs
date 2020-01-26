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
    public class DocMenuBService : IDocMenuBService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuB1Repository _IDocMenuB1Repository;
        private readonly IDocMenuB2Repository _IDocMenuB2Repository;

        public DocMenuBService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuB1Repository DocMenuB1Repository,
            IDocMenuB2Repository DocMenuB2Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuB1Repository = DocMenuB1Repository;
            _IDocMenuB2Repository = DocMenuB2Repository;
        }

        #region Menu B1

        public async Task<ModelMenuB1_InterfaceData> MenuB1InterfaceDataAsync(string userid, string username)
        {
            return await _IDocMenuB1Repository.MenuB1InterfaceDataAsync(userid, username);
        }

        public async Task<IList<ModelSelectOption>> GetAllProjectNameThaiAsync(string project_head)
        {
            string ProjectHead = Encoding.UTF8.GetString(Convert.FromBase64String(project_head));
            return await _IDocMenuB1Repository.GetAllProjectNameThaiAsync(ProjectHead);
        }
        public async Task<IList<ModelSelectOption>> GetAllDownloadFileByProjectIdAsync(string project_id)
        {
            return await _IDocMenuB1Repository.GetAllDownloadFileByProjectIdAsync(project_id);
        }

        public async Task<ModelMenuB1_FileDownload> GetAllDownloadFileByFileNameAsync(string filename)
        {
            ModelMenuB1_FileDownload resp = new ModelMenuB1_FileDownload();

            if (!string.IsNullOrEmpty(filename))
            {
                resp.filename = filename;
                resp.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, filename);
                return resp;
            }
            else return null;
        }

        public async Task<ModelMenuB1Data> GetProjectNumberWithDataAsync(string project_number)
        {
            return await _IDocMenuB1Repository.GetProjectNumberWithDataAsync(project_number);
        }

        public async Task<ModelMenuB1_GetDataByProjectNameThai> GetDataByProjectNameThaiAsync(int project_id)
        {
            return await _IDocMenuB1Repository.GetDataByProjectNameThaiAsync(project_id);
        }

        public async Task<ModelResponseMessageAddDocB1> AddDocMenuB1Async(ModelMenuB1 model)
        {

            ModelResponseMessageAddDocB1 resp = new ModelResponseMessageAddDocB1();

            if (string.IsNullOrWhiteSpace(model.projectid))
            {
                resp.Status = false;
                resp.Message = "กรุณาเลือก ชื่อโครงการวิจัยไทย!";
                return resp;
            }
            //if (string.IsNullOrWhiteSpace(model.filedownloadname))
            //{
            //    resp.Status = false;
            //    resp.Message = "กรุณาเลือก ดาวน์โลหดข้อเสนอ!";
            //    return resp;
            //}
            if (string.IsNullOrWhiteSpace(model.meetingdate))
            {
                resp.Status = false;
                resp.Message = "กรุณาระบุ กำหนดวันที่ประชุม!";
                return resp;
            }

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;
            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");
            model.projecthead = Encoding.UTF8.GetString(Convert.FromBase64String(model.projecthead));

            resp = await _IDocMenuB1Repository.AddDocMenuB1Async(model);

            return resp;
        }

        #endregion

        #region Menu B1 Edit

        public async Task<ModelMenuB1_InterfaceData> MenuB1InterfaceDataEditAsync(string project_number, string userid, string username)
        {
            return await _IDocMenuB1Repository.MenuB1InterfaceDataEditAsync(project_number, userid, username);
        }
        public async Task<ModelResponseMessageAddDocB1> UpdateDocMenuB1Async(ModelMenuB1Edit model)
        {

            ModelResponseMessageAddDocB1 resp = new ModelResponseMessageAddDocB1();

            if (string.IsNullOrWhiteSpace(model.projectid))
            {
                resp.Status = false;
                resp.Message = "กรุณาเลือก ชื่อโครงการวิจัยไทย!";
                return resp;
            }
            //if (string.IsNullOrWhiteSpace(model.filedownloadname))
            //{
            //    resp.Status = false;
            //    resp.Message = "กรุณาเลือก ดาวน์โลหดข้อเสนอ!";
            //    return resp;
            //}
            if (string.IsNullOrWhiteSpace(model.meetingdate))
            {
                resp.Status = false;
                resp.Message = "กรุณาระบุ กำหนดวันที่ประชุม!";
                return resp;
            }

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;
            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");
            //model.meetingdate = Convert.ToDateTime(model.meetingdate.ToString("yyyy-MM-dd"));
            model.projecthead = Encoding.UTF8.GetString(Convert.FromBase64String(model.projecthead));

            resp = await _IDocMenuB1Repository.UpdateDocMenuB1Async(model);

            return resp;
        }

        #endregion

        #region Menu B2

        public async Task<ModelMenuB2_InterfaceData> MenuB2InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuB2Repository.MenuB2InterfaceDataAsync(RegisterId);
        }

        public async Task<IList<ModelSelectOption>> GetAllLabNumberAsync()
        {
            return await _IDocMenuB2Repository.GetAllLabNumberAsync();
        }
        public async Task<ModelMenuB2_FileDownload> GetDownloadFileByFileNameB2Async(string filename)
        {
            ModelMenuB2_FileDownload resp = new ModelMenuB2_FileDownload();

            if (!string.IsNullOrEmpty(filename))
            {
                resp.filename = filename;
                resp.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA2, filename);
                return resp;
            }
            else return null;
        }

        public async Task<ModelMenuB2Data> GetLabNumberWithDataB2Async(string lab_number)
        {
            return await _IDocMenuB2Repository.GetLabNumberWithDataB2Async(lab_number);
        }

        public async Task<ModelResponseMessageAddDocB2> AddDocMenuB2Async(ModelMenuB2 model)
        {

            ModelResponseMessageAddDocB2 resp = new ModelResponseMessageAddDocB2();

            if (string.IsNullOrWhiteSpace(model.labNumber))
            {
                resp.Status = false;
                resp.Message = "กรุณาเลือก ประเภทห้องปฏิบัติการ!";
                return resp;
            }
            if (string.IsNullOrWhiteSpace(model.meetingdate))
            {
                resp.Status = false;
                resp.Message = "กรุณาระบุ กำหนดวันที่ประชุม!";
                return resp;
            }

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            model.docdate = DateTime.Now;
            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");
            //model.projecthead = Encoding.UTF8.GetString(Convert.FromBase64String(model.projecthead));

            resp = await _IDocMenuB2Repository.AddDocMenuB2Async(model);

            return resp;
        }


        #endregion

    }
}
