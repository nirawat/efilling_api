using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IDocMenuC3Repository
    {
        //บันทึกการประชุม
        Task<ModelMenuC3_InterfaceData> MenuC3InterfaceDataAsync(string RegisterId);
        Task<ModelCountOfYearC3> GetDefaultRoundC3Async(int yearof);
        Task<IList<ModelMenuC3_History>> GetAllHistoryDataC3Async();
        Task<ModelResponseMessage> AddDocMenuC3Async(ModelMenuC3 model);
        Task<ModelResponseMessage> CloseMeetingAsync(ModelCloseMeeting model);


        //ระเบียบวาระที่ 1
        Task<ModelMenuC31_InterfaceData> MenuC31InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuC31Async(ModelMenuC31 model);




        //ระเบียบวาระที่ 2
        Task<ModelMenuC32_InterfaceData> MenuC32InterfaceDataAsync(string RegisterId);
        Task<bool> MenuC32CheckAttachmentAsync(int meetingid);
        Task<ModelMenuC32_DownloadFileName> MenuC32DownloadAttachmentNameAsync(int meetingid);
        Task<ModelResponseMessage> AddDocMenuC32Async(ModelMenuC32 model);



        //ระเบียบวาระที่ 3
        Task<ModelMenuC33_InterfaceData> MenuC33InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuC33Async(ModelMenuC33 model);
        Task<ModelMenuC33Data> GetProjectNumberWithDataC3Tab3Async(string project_number);
        Task<IList<ModelSelectOption>> GetAllApprovalTypeByProjectC2ForTab3Async(string project_number);
        Task<IList<ModelMenuC33HistoryData>> GetAllHistoryDataC3Tab3Async();



        //ระเบียบวาระที่ 4
        Task<ModelMenuC34_InterfaceData> MenuC34InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuC34Async(ModelMenuC34 model);
        Task<ModelMenuC34Tab4Data> GetProjectNumberWithDataC3Tab4Async(int type, string project_number);
        Task<IList<ModelSelectOption>> GetAllProjectNumberTab4Async(int type);


        //ระเบียบวาระที่ 5
        Task<ModelMenuC35_InterfaceData> MenuC35InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuC35Async(ModelMenuC35 model);


    }
}
