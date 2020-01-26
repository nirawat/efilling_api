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

namespace THD.Core.Api.Business
{
    public class DocMenuCService : IDocMenuCService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuC1Repository _IDocMenuC1Repository;
        private readonly IDocMenuC2Repository _IDocMenuC2Repository;
        private readonly IDocMenuC3Repository _IDocMenuC3Repository;
        private readonly IDocMeetingRoundRepository _IDocMeetingRoundRepository;

        private readonly IDocMenuC34HistoryRepository _IDocMenuC34HistoryRepository;

        public DocMenuCService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuC1Repository DocMenuC1Repository,
            IDocMenuC2Repository DocMenuC2Repository,
            IDocMenuC3Repository DocMenuC3Repository,
            IDocMenuC34HistoryRepository IDocMenuC34HistoryRepository,
            IDocMeetingRoundRepository DocMeetingRoundRepository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuC1Repository = DocMenuC1Repository;
            _IDocMenuC2Repository = DocMenuC2Repository;
            _IDocMenuC3Repository = DocMenuC3Repository;
            _IDocMenuC34HistoryRepository = IDocMenuC34HistoryRepository;
            _IDocMeetingRoundRepository = DocMeetingRoundRepository;
        }


        #region MenuC1

        public async Task<ModelMenuC1_InterfaceData> MenuC1InterfaceDataAsync(string userid, string username)
        {
            return await _IDocMenuC1Repository.MenuC1InterfaceDataAsync(userid, username);
        }

        public async Task<ModelCountOfYear> GetMeetingRoundOfProjectAsync(int year)
        {
            return await _IDocMeetingRoundRepository.GetMeetingRoundOfProjectAsync(year);
        }

        public async Task<ModelMenuC1Data> GetProjectNumberWithDataC1Async(string project_number)
        {
            return await _IDocMenuC1Repository.GetProjectNumberWithDataC1Async(project_number);
        }

        public async Task<ModelRegisterData> GetRegisterUserDataAsync(string register_id)
        {
            return await _IDocMenuC1Repository.GetRegisterUserDataAsync(register_id);
        }

        public async Task<ModelResponseC1Message> AddDocMenuC1Async(ModelMenuC1 model)
        {

            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuC1Repository.AddDocMenuC1Async(model);

            return resp;
        }


        #endregion

        #region MenuC1 Edit

        public async Task<ModelMenuC1_InterfaceData> MenuC1InterfaceDataEditAsync(string project_number, string RegisterId)
        {
            return await _IDocMenuC1Repository.MenuC1InterfaceDataEditAsync(project_number, RegisterId);
        }

        public async Task<ModelResponseC1Message> UpdateDocMenuC1EditAsync(ModelMenuC1 model)
        {

            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuC1Repository.UpdateDocMenuC1EditAsync(model);

            return resp;
        }


        #endregion

        #region MenuC1_2

        public async Task<ModelMenuC12_InterfaceData> MenuC12InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC1Repository.MenuC12InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuC12Data> GetProjectNumberWithDataC12Async(string project_number)
        {
            return await _IDocMenuC1Repository.GetProjectNumberWithDataC12Async(project_number);
        }

        public async Task<ModelRegisterDataC12> GetRegisterUserDataC12Async(string register_id)
        {
            return await _IDocMenuC1Repository.GetRegisterUserDataC12Async(register_id);
        }

        public async Task<ModelResponseC12Message> AddDocMenuC12Async(ModelMenuC12 model)
        {

            model.meetingdate = Convert.ToDateTime(model.meetingdate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuC1Repository.AddDocMenuC12Async(model);

            return resp;
        }


        #endregion

        #region MenuC2

        public async Task<ModelMenuC2_InterfaceData> MenuC2InterfaceDataAsync(string userid, string username)
        {
            return await _IDocMenuC2Repository.MenuC2InterfaceDataAsync(userid, username);
        }

        public async Task<ModelMenuC2Data> GetProjectNumberWithDataC2Async(string project_number)
        {
            return await _IDocMenuC2Repository.GetProjectNumberWithDataC2Async(project_number);
        }

        public async Task<IList<ModelSelectOption>> GetAllAssignerUserAsync()
        {
            return await _IDocMenuC2Repository.GetAllAssignerUserAsync();
        }

        public async Task<ModelMenuC2Data> GetRegisterUserDataC2Async(string register_id)
        {
            return await _IDocMenuC2Repository.GetRegisterUserDataC2Async(register_id);
        }

        public async Task<ModelResponseC2Message> AddDocMenuC2Async(ModelMenuC2 model)
        {

            var resp = await _IDocMenuC2Repository.AddDocMenuC2Async(model);

            return resp;
        }


        #endregion

        #region Menu C2 Edit

        public async Task<ModelMenuC2_InterfaceData> MenuC2InterfaceDataEditAsync(string project_number, string userid, string username)
        {
            return await _IDocMenuC2Repository.MenuC2InterfaceDataEditAsync(project_number, userid, username);
        }

        public async Task<ModelResponseC2Message> UpdateDocMenuC2EditAsync(ModelMenuC2 model)
        {
            var resp = await _IDocMenuC2Repository.UpdateDocMenuC2EditAsync(model);

            return resp;
        }

        #endregion

        #region MenuC2_2

        public async Task<ModelMenuC22_InterfaceData> MenuC22InterfaceDataAsync(string userid, string username)
        {
            return await _IDocMenuC2Repository.MenuC22InterfaceDataAsync(userid, username);
        }

        public async Task<ModelMenuC22Data> GetProjectNumberWithDataC22Async(string project_number)
        {
            return await _IDocMenuC2Repository.GetProjectNumberWithDataC22Async(project_number);
        }

        public async Task<IList<ModelSelectOption>> GetAllAssignerUserC22Async()
        {
            return await _IDocMenuC2Repository.GetAllAssignerUserC22Async();
        }

        public async Task<ModelMenuC22Data> GetRegisterUserDataC22Async(string register_id)
        {
            return await _IDocMenuC2Repository.GetRegisterUserDataC22Async(register_id);
        }

        public async Task<ModelResponseC22Message> AddDocMenuC22Async(ModelMenuC22 model)
        {

            var resp = await _IDocMenuC2Repository.AddDocMenuC22Async(model);

            return resp;
        }


        #endregion

        #region MenuC3

        // บันทึกการประชุม ------------------------------------------------------------------------------

        public async Task<ModelMenuC3_InterfaceData> MenuC3InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC3InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelCountOfYearC3> GetDefaultRoundC3Async(int yearof)
        {
            return await _IDocMenuC3Repository.GetDefaultRoundC3Async(yearof);
        }

        public async Task<IList<ModelMenuC3_History>> GetAllHistoryDataC3Async()
        {
            return await _IDocMenuC3Repository.GetAllHistoryDataC3Async();
        }

        public async Task<ModelResponseMessage> AddDocMenuC3Async(ModelMenuC3 model)
        {

            var resp = await _IDocMenuC3Repository.AddDocMenuC3Async(model);

            return resp;
        }

        public async Task<ModelResponseMessage> CloseMeetingAsync(ModelCloseMeeting model)
        {

            var resp = await _IDocMenuC3Repository.CloseMeetingAsync(model);

            return resp;
        }


        // ระเบียบวาระที่ 1 ------------------------------------------------------------------------------

        public async Task<ModelMenuC31_InterfaceData> MenuC31InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC31InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseMessage> AddDocMenuC31Async(ModelMenuC31 model)
        {
            return await _IDocMenuC3Repository.AddDocMenuC31Async(model);
        }



        // ระเบียบวาระที่ 2 ------------------------------------------------------------------------------

        public async Task<ModelMenuC32_InterfaceData> MenuC32InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC32InterfaceDataAsync(RegisterId);
        }

        public async Task<bool> MenuC32CheckAttachmentAsync(int meetingid)
        {
            return await _IDocMenuC3Repository.MenuC32CheckAttachmentAsync(meetingid);
        }

        public async Task<ModelMenuC32_DownloadFile> MenuC32DownloadAttachmentZipAsync(int meetingid)
        {

            var file_download = await _IDocMenuC3Repository.MenuC32DownloadAttachmentNameAsync(meetingid);

            if (file_download != null)
            {
                string fileZipName = meetingid + "-" + DateTime.Now.ToString("ddMMyyHHtt") + ".zip";

                ModelMenuC32_DownloadFile download_file = new ModelMenuC32_DownloadFile();

                download_file.filename = fileZipName;
                download_file.filebase64 = ServerDirectorys.DownloadFileC3Tab2(_IEnvironmentConfig.PathArchive, _IEnvironmentConfig.PathDocument, FolderDocument.menuC3Tab2,
                                                                                meetingid.ToString(), fileZipName, file_download.file1name, file_download.file2name, file_download.file3name);
                if (download_file.filebase64 != null) return download_file;
            }
            return null;


        }

        public async Task<ModelResponseMessage> AddDocMenuC32Async(ModelMenuC32 model)
        {
            model.tab2Group1Seq1FileInput2 = string.IsNullOrEmpty(model.tab2Group1Seq1FileInput2) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.tab2Group1Seq1FileInput2);
            model.tab2Group1Seq2FileInput2 = string.IsNullOrEmpty(model.tab2Group1Seq2FileInput2) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.tab2Group1Seq2FileInput2);
            model.tab2Group1Seq3FileInput2 = string.IsNullOrEmpty(model.tab2Group1Seq3FileInput2) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.tab2Group1Seq3FileInput2);

            var resp = await _IDocMenuC3Repository.AddDocMenuC32Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.tab2Group1Seq1FileInput2Base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuC3Tab2, model.tab2Group1Seq1FileInput2, model.tab2Group1Seq1FileInput2Base64);
                if (!string.IsNullOrEmpty(model.tab2Group1Seq2FileInput2Base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuC3Tab2, model.tab2Group1Seq2FileInput2, model.tab2Group1Seq2FileInput2Base64);
                if (!string.IsNullOrEmpty(model.tab2Group1Seq3FileInput2Base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuC3Tab2, model.tab2Group1Seq3FileInput2, model.tab2Group1Seq3FileInput2Base64);
            }
            return resp;
        }



        // ระเบียบวาระที่ 3 ------------------------------------------------------------------------------

        public async Task<ModelMenuC33_InterfaceData> MenuC33InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC33InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseMessage> AddDocMenuC33Async(ModelMenuC33 model)
        {
            return await _IDocMenuC3Repository.AddDocMenuC33Async(model);
        }

        public async Task<ModelMenuC33Data> GetProjectNumberWithDataC3Tab3Async(string project_number)
        {
            return await _IDocMenuC3Repository.GetProjectNumberWithDataC3Tab3Async(project_number);
        }

        public async Task<IList<ModelSelectOption>> GetAllApprovalTypeByProjectC2ForTab3Async(string project_number)
        {
            return await _IDocMenuC3Repository.GetAllApprovalTypeByProjectC2ForTab3Async(project_number);
        }

        public async Task<IList<ModelMenuC33HistoryData>> GetAllHistoryDataC3Tab3Async()
        {
            return await _IDocMenuC3Repository.GetAllHistoryDataC3Tab3Async();
        }

        // ระเบียบวาระที่ 4 ------------------------------------------------------------------------------

        public async Task<ModelMenuC34_InterfaceData> MenuC34InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC34InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseMessage> AddDocMenuC34Async(ModelMenuC34 model)
        {
            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);

            var resp = await _IDocMenuC3Repository.AddDocMenuC34Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuC3Tab4, model.file1name, model.file1base64);
            }
            return resp;
        }

        public async Task<IList<ModelSelectOption>> GetAllProjectNumberTab4Async(int type)
        {
            return await _IDocMenuC3Repository.GetAllProjectNumberTab4Async(type);
        }

        public async Task<ModelMenuC34Tab4Data> GetProjectNumberWithDataC3Tab4Async(int type, string project_number)
        {
            return await _IDocMenuC3Repository.GetProjectNumberWithDataC3Tab4Async(type, project_number);

        }

        // ระเบียบวาระที่ 5 ------------------------------------------------------------------------------

        public async Task<ModelMenuC35_InterfaceData> MenuC35InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuC3Repository.MenuC35InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseMessage> AddDocMenuC35Async(ModelMenuC35 model)
        {
            return await _IDocMenuC3Repository.AddDocMenuC35Async(model);
        }






        #endregion


        // History ----------------------------------------------------------------------

        #region C3 Tab4

        public async Task<ModelMenuC3Tab4_InterfaceData_History> MenuC3Tab4InterfaceHistoryDataAsync()
        {
            return await _IDocMenuC34HistoryRepository.MenuC3Tab4InterfaceHistoryDataAsync();
        }

        public async Task<IList<ModelMenuC3Tab4_Data>> GetAllReportHistoryDataC3Tab4Async(ModelMenuC3Tab4_InterfaceData_History search)
        {
            return await _IDocMenuC34HistoryRepository.GetAllReportHistoryDataC3Tab4Async(search);
        }

        #endregion


        // Share ...............................
        public async Task<IList<ModelSelectOption>> GetAllProjectAsync(string AssignerCode, string DocProcess)
        {
            return await _IDocMenuC2Repository.GetAllProjectAsync(AssignerCode, DocProcess);
        }

        public async Task<IList<ModelSelectOption>> GetAllProjectLabAsync(string AssignerCode, string DocProcess)
        {
            return await _IDocMenuC2Repository.GetAllProjectLabAsync(AssignerCode, DocProcess);
        }
    }
}
