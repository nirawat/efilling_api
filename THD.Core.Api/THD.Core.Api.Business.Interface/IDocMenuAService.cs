using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuAService
    {
        Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataAsync(string userid, string username);
        Task<ModelResponseMessage> AddDocMenuA1Async(ModelMenuA1 model);

        #region "Menu A1 Edit"

        Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataEditAsync(int doc_id, string userid, string username);
        Task<ModelMenuA1_FileDownload> GetA1DownloadFileByIdAsync(int DocId, int Id);
        Task<ModelResponseMessage> UpdateDocMenuA1EditAsync(ModelMenuA1 model);

        #endregion

        Task<ModelMenuA2_InterfaceData> MenuA2InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuA2Async(ModelMenuA2 model);

        Task<ModelMenuA3_InterfaceData> MenuA3InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA3ProjectNumberData> GetProjectNumberWithDataA3Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA3Async(ModelMenuA3 model);

        #region "Menu A3 Edit"

        Task<ModelMenuA3_InterfaceData> MenuA3EditInterfaceDataAsync(string UserId, string ProjectNumber);

        #endregion

        Task<ModelMenuA4_InterfaceData> MenuA4InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA4ProjectNumberData> GetProjectNumberWithDataA4Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA4Async(ModelMenuA4 model);

        #region "Menu A4 Edit"

        Task<ModelMenuA4_InterfaceData> MenuA4EditInterfaceDataAsync(string UserId, string ProjectNumber);

        #endregion



        Task<ModelMenuA5_InterfaceData> MenuA5InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA5ProjectNumberData> GetProjectNumberWithDataA5Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA5Async(ModelMenuA5 model);

        #region "Menu A5 Edit"

        Task<ModelMenuA5_InterfaceData> MenuA5EditInterfaceDataAsync(string UserId, string ProjectNumber);

        #endregion


        Task<ModelMenuA6_InterfaceData> MenuA6InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA6ProjectNumberData> GetProjectNumberWithDataA6Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA6Async(ModelMenuA6 model);

        #region "Menu A6 Edit"

        Task<ModelMenuA6_InterfaceData> MenuA6EditInterfaceDataAsync(string UserId, string ProjectNumber);

        #endregion

        Task<ModelMenuA7_InterfaceData> MenuA7InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA7ProjectNumberData> GetProjectNumberWithDataA7Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA7Async(ModelMenuA7 model);

        #region "Menu A7 Edit"

        Task<ModelMenuA7_InterfaceData> MenuA7EditInterfaceDataAsync(string UserId, string ProjectNumber);

        #endregion
    }
}
