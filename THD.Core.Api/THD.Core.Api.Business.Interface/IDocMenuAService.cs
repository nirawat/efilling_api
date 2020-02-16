using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuAService
    {
        #region A1
        Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataAsync(string userid, string username);
        Task<ModelResponseA1Message> AddDocMenuA1Async(ModelMenuA1 model);

        #endregion

        #region "Menu A1 Edit"

        Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataEditAsync(int doc_id, string userid, string username);
        Task<ModelMenuA1_FileDownload> GetA1DownloadFileByIdAsync(int DocId, int Id);
        Task<ModelResponseA1Message> UpdateDocMenuA1EditAsync(ModelMenuA1 model);

        #endregion

        #region A2

        Task<ModelMenuA2_InterfaceData> MenuA2InterfaceDataAsync(string RegisterId);
        Task<ModelResponseA2Message> AddDocMenuA2Async(ModelMenuA2 model);

        #endregion

        #region A3

        Task<ModelMenuA3_InterfaceData> MenuA3InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA3ProjectNumberData> GetProjectNumberWithDataA3Async(string project_number);
        Task<ModelResponseA3Message> AddDocMenuA3Async(ModelMenuA3 model);

        #endregion

        #region "Menu A3 Edit"

        Task<ModelMenuA3_InterfaceData> MenuA3EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelMenuA3_FileDownload> GetA3DownloadFileByIdAsync(int DocId, int Id);

        Task<ModelResponseA3Message> UpdateDocMenuA3EditAsync(ModelMenuA3 model);

        #endregion

        #region A4
        Task<ModelMenuA4_InterfaceData> MenuA4InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA4ProjectNumberData> GetProjectNumberWithDataA4Async(string project_number);
        Task<ModelResponseA4Message> AddDocMenuA4Async(ModelMenuA4 model);
        #endregion

        #region "Menu A4 Edit"

        Task<ModelMenuA4_InterfaceData> MenuA4EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelMenuA4_FileDownload> GetA4DownloadFileByIdAsync(int DocId, int Id);

        Task<ModelResponseA4Message> UpdateDocMenuA4EditAsync(ModelMenuA4 model);

        #endregion

        #region A5

        Task<ModelMenuA5_InterfaceData> MenuA5InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA5ProjectNumberData> GetProjectNumberWithDataA5Async(string project_number);
        Task<ModelResponseA5Message> AddDocMenuA5Async(ModelMenuA5 model);

        #endregion

        #region "Menu A5 Edit"

        Task<ModelMenuA5_InterfaceData> MenuA5EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelMenuA5_FileDownload> GetA5DownloadFileByIdAsync(int DocId, int Id);

        Task<ModelResponseA5Message> UpdateDocMenuA5EditAsync(ModelMenuA5 model);

        #endregion

        #region A6

        Task<ModelMenuA6_InterfaceData> MenuA6InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA6ProjectNumberData> GetProjectNumberWithDataA6Async(string project_number);
        Task<ModelResponseA6Message> AddDocMenuA6Async(ModelMenuA6 model);

        #endregion

        #region "Menu A6 Edit"

        Task<ModelMenuA6_InterfaceData> MenuA6EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelMenuA6_FileDownload> GetA6DownloadFileByIdAsync(int DocId, int Id);

        Task<ModelResponseA6Message> UpdateDocMenuA6EditAsync(ModelMenuA6 model);

        #endregion

        #region A7

        Task<ModelMenuA7_InterfaceData> MenuA7InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA7ProjectNumberData> GetProjectNumberWithDataA7Async(string project_number);
        Task<ModelResponseA7Message> AddDocMenuA7Async(ModelMenuA7 model);

        #endregion

        #region "Menu A7 Edit"

        Task<ModelMenuA7_InterfaceData> MenuA7EditInterfaceDataAsync(string UserId, string ProjectNumber);

        Task<ModelMenuA7_FileDownload> GetA7DownloadFileByIdAsync(int DocId, int Id);

        Task<ModelResponseA7Message> UpdateDocMenuA7EditAsync(ModelMenuA7 model);

        #endregion

    }
}
