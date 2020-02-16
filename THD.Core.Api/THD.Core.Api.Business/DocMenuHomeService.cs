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
using System.IO.Compression;

namespace THD.Core.Api.Business
{
    public class DocMenuHomeService : IDocMenuHomeService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuHomeRepository _IDocMenuHomeRepository;

        public DocMenuHomeService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuHomeRepository DocMenuHomeRepository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuHomeRepository = DocMenuHomeRepository;
        }

        #region Menu Home 1

        public async Task<ModelMenuHome1_InterfaceData> MenuHome1InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuHomeRepository.MenuHome1InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuHome1_DownloadFile> DownloadFileHome1Async(string project_number)
        {

            var file_download = await _IDocMenuHomeRepository.GetFileDownloadHome1Async(project_number);

            if (file_download != null)
            {
                string fileZipName = project_number + "-" + DateTime.Now.ToString("ddMMyyHHtt") + ".zip";

                ModelMenuHome1_DownloadFile download_file = new ModelMenuHome1_DownloadFile();

                download_file.filename = fileZipName;
                download_file.filebase64 = ServerDirectorys.DownloadFileHome1(_IEnvironmentConfig.PathArchive, _IEnvironmentConfig.PathDocument, FolderDocument.menuA1,
                                                                                project_number, fileZipName, file_download.file1name, file_download.file2name,
                                                                                file_download.file3name, file_download.file4name, file_download.file5name);
                if (download_file.filebase64 != null) return download_file;
            }
            return null;


        }

        public async Task<IList<ResultCommentNote>> GetResultNoteHome1Async(string project_number, string user_id)
        {
            return await _IDocMenuHomeRepository.GetResultNoteHome1Async(project_number, user_id);
        }

        public async Task<IList<ModelMenuHome1ReportData>> GetAllReportDataHome1Async(ModelMenuHome1_InterfaceData search_data)
        {
            return await _IDocMenuHomeRepository.GetAllReportDataHome1Async(search_data);
        }

        #endregion

        #region Menu Home 2

        public async Task<ModelMenuHome2_InterfaceData> MenuHome2InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuHomeRepository.MenuHome2InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuHome2_DownloadFile> DownloadFileHome2Async(string project_number)
        {

            var file_download = await _IDocMenuHomeRepository.GetFileDownloadHome2Async(project_number);

            if (file_download != null)
            {
                string extention = "";
                if (!string.IsNullOrEmpty(file_download.filename1) && !string.IsNullOrEmpty(file_download.filename2)) extention = ".zip";
                if (!string.IsNullOrEmpty(file_download.filename1) && string.IsNullOrEmpty(file_download.filename2)) extention = ".pdf";
                if (!string.IsNullOrEmpty(file_download.filename2) && string.IsNullOrEmpty(file_download.filename1)) extention = ".pdf";
                string fileZipName = project_number + "-" + DateTime.Now.ToString("ddMMyyHHtt") + extention;

                ModelMenuHome2_DownloadFile download_file = new ModelMenuHome2_DownloadFile();

                download_file.filename = fileZipName;
                download_file.filebase64 = ServerDirectorys.DownloadFileHome2(_IEnvironmentConfig.PathArchive, _IEnvironmentConfig.PathDocument, FolderDocument.menuA2, project_number, fileZipName, file_download.filename1, file_download.filename2);

                if (download_file.filebase64 != null) return download_file;
            }
            return null;

        }

        public async Task<IList<ModelMenuHome2ReportData>> GetAllReportDataHome2Async(ModelMenuHome2_InterfaceData search_data)
        {
            return await _IDocMenuHomeRepository.GetAllReportDataHome2Async(search_data);
        }

        #endregion



    }
}
