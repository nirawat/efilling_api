using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuBService
    {
        Task<ModelMenuB1_InterfaceData> MenuB1InterfaceDataAsync(string userid, string username);
        Task<IList<ModelSelectOption>> GetAllProjectNameThaiAsync(string project_head);
        Task<IList<ModelSelectOption>> GetAllDownloadFileByProjectIdAsync(string project_id);
        Task<ModelMenuB1_FileDownload> GetAllDownloadFileByFileNameAsync(string filename);
        Task<ModelMenuB1_GetDataByProjectNameThai> GetDataByProjectNameThaiAsync(int project_id);
        Task<ModelMenuB1Data> GetProjectNumberWithDataAsync(string project_number);
        Task<ModelResponseMessageAddDocB1> AddDocMenuB1Async(ModelMenuB1 model);



        Task<ModelMenuB1_InterfaceData> MenuB1InterfaceDataEditAsync(string project_number, string userid, string username);
        Task<ModelResponseMessageAddDocB1> UpdateDocMenuB1Async(ModelMenuB1Edit model);



        Task<ModelMenuB2_InterfaceData> MenuB2InterfaceDataAsync(string RegisterId);
        Task<IList<ModelSelectOption>> GetAllLabNumberAsync();
        Task<ModelMenuB2_FileDownload> GetDownloadFileByFileNameB2Async(string filename);
        Task<ModelMenuB2Data> GetLabNumberWithDataB2Async(string lab_number);
        Task<ModelResponseMessageAddDocB2> AddDocMenuB2Async(ModelMenuB2 model);
    }
}
