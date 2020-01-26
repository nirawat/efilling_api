using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuHomeService
    {
        Task<ModelMenuHome1_InterfaceData> MenuHome1InterfaceDataAsync(string RegisterId);
        Task<ModelMenuHome1_DownloadFile> DownloadFileHome1Async(string project_number);
        Task<ModelMenuHome1_ResultNote> GetResultNoteHome1Async(string project_number);
        Task<IList<ModelMenuHome1ReportData>> GetAllReportDataHome1Async(ModelMenuHome1_InterfaceData search_data);
        Task<ModelMenuHome2_InterfaceData> MenuHome2InterfaceDataAsync(string RegisterId);
        Task<ModelMenuHome2_DownloadFile> DownloadFileHome2Async(string project_number);
        Task<IList<ModelMenuHome2ReportData>> GetAllReportDataHome2Async(ModelMenuHome2_InterfaceData search_data);
    }
}
