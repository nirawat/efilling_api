﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuCService
    {
        //Menu C1 ------------------------------------------------------------------------------
        Task<ModelMenuC1_InterfaceData> MenuC1InterfaceDataAsync(string userid, string username);
        Task<ModelCountOfYear> GetMeetingRoundOfProjectAsync(int year);
        Task<ModelMenuC1Data> GetProjectNumberWithDataC1Async(string project_number);
        Task<ModelRegisterData> GetRegisterUserDataAsync(string register_id);
        Task<ModelResponseC1Message> AddDocMenuC1Async(ModelMenuC1 model);
        Task<ModelResponseMessage> CloseMeetingAsync(ModelCloseMeeting model);

        //Menu C1 Edit --------------------------------------------------------------------------
        Task<ModelMenuC1_InterfaceData> MenuC1InterfaceDataEditAsync(string project_number, string RegisterId);
        Task<ModelResponseC1Message> UpdateDocMenuC1EditAsync(ModelMenuC1 model);

        //Menu C1 2------------------------------------------------------------------------------
        Task<ModelMenuC12_InterfaceData> MenuC12InterfaceDataAsync(string RegisterId);
        Task<ModelMenuC12Data> GetProjectNumberWithDataC12Async(string project_number);
        Task<ModelRegisterDataC12> GetRegisterUserDataC12Async(string register_id);
        Task<ModelResponseC12Message> AddDocMenuC12Async(ModelMenuC12 model);


        //Menu C2 ------------------------------------------------------------------------------
        Task<ModelMenuC2_InterfaceData> MenuC2InterfaceDataAsync(string userid, string username);


        Task<ModelMenuC2Data> GetProjectNumberWithDataC2Async(string project_number);

        Task<IList<ModelSelectOption>> GetAllAssignerUserAsync();

        Task<ModelMenuC2Data> GetRegisterUserDataC2Async(string register_id);

        Task<ModelResponseC2Message> AddDocMenuC2Async(ModelMenuC2 model);


        //Menu C2 Edit ------------------------------------------------------------------------------
        Task<ModelMenuC2_InterfaceData> MenuC2InterfaceDataEditAsync(string project_number, string userid, string username);

        Task<ModelResponseC2Message> UpdateDocMenuC2EditAsync(ModelMenuC2 model);


        //Menu C2 2------------------------------------------------------------------------------
        Task<ModelMenuC22_InterfaceData> MenuC22InterfaceDataAsync(string userid, string username);

        Task<ModelMenuC22Data> GetProjectNumberWithDataC22Async(string project_number);

        Task<IList<ModelSelectOption>> GetAllAssignerUserC22Async();

        Task<ModelMenuC22Data> GetRegisterUserDataC22Async(string register_id);

        Task<ModelResponseC22Message> AddDocMenuC22Async(ModelMenuC22 model);



        //Menu C3 ------------------------------------------------------------------------------

        //บันทึกการประชุม
        Task<ModelMenuC3_InterfaceData> MenuC3InterfaceDataAsync(string RegisterId);
        Task<ModelCountOfYearC3> GetDefaultRoundC3Async(int yearof);
        Task<ModelResponseMessage> AddDocMenuC3Async(ModelMenuC3 model);
        Task<IList<ModelMenuC3_History>> GetAllHistoryDataC3Async();


        //ระเบียบวาระที่ 1
        Task<ModelMenuC31_InterfaceData> MenuC31InterfaceDataAsync(string RegisterId);
        Task<ModelResponseMessage> AddDocMenuC31Async(ModelMenuC31 model);



        //ระเบียบวาระที่ 2
        Task<ModelMenuC32_InterfaceData> MenuC32InterfaceDataAsync(string RegisterId);
        Task<bool> MenuC32CheckAttachmentAsync(int meetingid);
        Task<ModelMenuC32_DownloadFile> MenuC32DownloadAttachmentZipAsync(int meetingid);
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





        // History -----------------------------------------------------------------------------------------

        // History C3 Tab4
        Task<ModelMenuC3Tab4_InterfaceData_History> MenuC3Tab4InterfaceHistoryDataAsync();
        Task<IList<ModelMenuC3Tab4_Data>> GetAllReportHistoryDataC3Tab4Async(ModelMenuC3Tab4_InterfaceData_History search);



        // Share function ----------------------------------------------------------------------------------
        Task<IList<ModelSelectOption>> GetAllProjectAsync(string AssignerCode, string DocProcess);

        Task<IList<ModelSelectOption>> GetAllProjectLabAsync(string AssignerCode, string DocProcess);


    }
}
