using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuDService
    {

        #region D1
        Task<ModelMenuD1_InterfaceData> MenuD1InterfaceDataAsync(string RegisterId);
        Task<ModelMenuD1ProjectNumberData> GetProjectNumberWithDataD1Async(string project_number);
        Task<ModelResponseD1Message> AddDocMenuD1Async(ModelMenuD1 model);
        #endregion

        #region "Menu D1 Edit"

        Task<ModelMenuD1_InterfaceData> MenuD1EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelResponseD1Message> UpdateDocMenuD1EditAsync(ModelMenuD1 model);

        #endregion

        #region D2

        Task<ModelMenuD2_InterfaceData> MenuD2InterfaceDataAsync(string RegisterId);
        Task<ModelMenuD2ProjectNumberData> GetProjectNumberWithDataD2Async(string project_number);
        Task<ModelMenuD2_FileDownload> GetAllDownloadFileByFileNameAsync(string filename);
        Task<ModelResponseD2Message> AddDocMenuD2Async(ModelMenuD2 model);

        #endregion
    }
}
