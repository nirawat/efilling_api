﻿using THD.Core.Api.Business.Interface;
using THD.Core.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Text;
using THD.Core.Api.Helpers;
using System.Threading;
using System.Collections.Generic;
using THD.Core.Api.Models.Config;

namespace THD.Core.Api.Private.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateDocMenuCController : ControllerBase
    {
        private readonly IDocMenuCService _IDocMenuCService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;
        private readonly IMailTemplateService _IMailTemplateService;


        public PrivateDocMenuCController(
            IDocMenuCService IDocMenuCService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig,
            IMailTemplateService MailTemplateService)
        {
            _IDocMenuCService = IDocMenuCService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
            _IMailTemplateService = MailTemplateService;
        }


        #region Menu C1

        [HttpGet("MenuC1InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC1InterfaceData(string userid, string username)
        {
            ModelMenuC1_InterfaceData e = await _IDocMenuCService.MenuC1InterfaceDataAsync(userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetMeetingRoundOfProject/{Year}")]
        public async Task<IActionResult> GetMeetingRoundOfProject(int Year)
        {
            ModelCountOfYear e = await _IDocMenuCService.GetMeetingRoundOfProjectAsync(Year);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithDataC1/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC1(string ProjectNumber)
        {
            ModelMenuC1Data e = await _IDocMenuCService.GetProjectNumberWithDataC1Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetRegisterUserData/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserData(string RegisterId)
        {
            ModelRegisterData e = await _IDocMenuCService.GetRegisterUserDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC1")]
        public async Task<IActionResult> AddDocMenuC1([FromBody]ModelMenuC1 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC1Message e = await _IDocMenuCService.AddDocMenuC1Async(model);

            if (e.Status == true)
            {
                _result = Ok(e);

                try
                {
                    await _IMailTemplateService.MailTemplate3Async(model, e.filebase64);
                }
                catch (Exception ex)
                {
                    //Keep
                }

            }
            else _result = BadRequest();

            return _result;

        }

        #endregion

        #region Menu C1 Edit

        [HttpGet("MenuC1InterfaceDataEdit/{ProjectNumber}/{RegisterId}")]
        public async Task<IActionResult> MenuC1InterfaceDataEdit(string ProjectNumber, string RegisterId)
        {
            ModelMenuC1_InterfaceData e = await _IDocMenuCService.MenuC1InterfaceDataEditAsync(ProjectNumber, RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();


        }
        [HttpPost("UpdateDocMenuC1Edit")]
        public async Task<IActionResult> UpdateDocMenuC1Edit([FromBody]ModelMenuC1 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC1Message e = await _IDocMenuCService.UpdateDocMenuC1EditAsync(model);

            if (e.Status == true)
            {
                _result = Ok(e);

                try
                {
                    await _IMailTemplateService.MailTemplate3Async(model, e.filebase64);
                }
                catch (Exception ex)
                {
                    //Keep
                }
            }
            else _result = BadRequest();

            return _result;

        }



        #endregion

        #region Menu C1_2

        [HttpGet("MenuC12InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC12InterfaceData(string RegisterId)
        {
            ModelMenuC12_InterfaceData e = await _IDocMenuCService.MenuC12InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataC12/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC12(string ProjectNumber)
        {
            ModelMenuC12Data e = await _IDocMenuCService.GetProjectNumberWithDataC12Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetRegisterUserDataC12/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC12(string RegisterId)
        {
            ModelRegisterDataC12 e = await _IDocMenuCService.GetRegisterUserDataC12Async(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC12")]
        public async Task<IActionResult> AddDocMenuC12([FromBody]ModelMenuC12 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC12Message e = await _IDocMenuCService.AddDocMenuC12Async(model);

            if (e.Status == true)
            {
                _result = Ok(e);

                //string serverip = Encoding.UTF8.GetString(Convert.FromBase64String(_EnvironmentConfig.Server));

                //string linkactive = $"{serverip}/{"efilling/log_in"}";

                //string mail_body = "<h3>เรื่อง ขอความร่วมมือพิจารณาห้องปฏิบัติการ</h3>" + Environment.NewLine +
                //                   "<h2>ตามบันทึกไฟล์แนบ</h2></br>" + Environment.NewLine +
                //                   "<h4>คุณสามารถดูเอกสารไฟล์แนบได้โดย <a href='" + linkactive + "'>คลิ้กเพื่อไปยังหน้าเพจ</a>.</h4>";

                //await EmailHelper.SentGmail(e.EmailArray, "eFilling : ขอความร่วมมือพิจารณาห้องปฏิบัติการ", mail_body);
            }

            return _result;

        }

        #endregion

        #region Menu C2
        [HttpGet("MenuC2InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC2InterfaceData(string userid, string username)
        {
            ModelMenuC2_InterfaceData e = await _IDocMenuCService.MenuC2InterfaceDataAsync(userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();

        }


        [HttpGet("GetProjectNumberWithDataC2/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC2(string ProjectNumber)
        {
            ModelMenuC2Data e = await _IDocMenuCService.GetProjectNumberWithDataC2Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllAssignerUser")]
        public async Task<IActionResult> GetAllAssignerUser()
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllAssignerUserAsync();

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetRegisterUserDataC2/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC2(string RegisterId)
        {
            ModelMenuC2Data e = await _IDocMenuCService.GetRegisterUserDataC2Async(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC2")]
        public async Task<IActionResult> AddDocMenuC2([FromBody]ModelMenuC2 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC2Message e = await _IDocMenuCService.AddDocMenuC2Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #endregion

        #region Menu C2 Edit

        [HttpGet("MenuC2InterfaceDataEdit/{DocId}/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC2InterfaceDataEdit(int docid, string userid, string username)
        {
            ModelMenuC2_InterfaceData e = await _IDocMenuCService.MenuC2InterfaceDataEditAsync(docid, userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();


        }

        [HttpPost("UpdateDocMenuC2Edit")]
        public async Task<IActionResult> UpdateDocMenuC2Edit([FromBody]ModelMenuC2 model)
        {
            ModelResponseC2Message e = await _IDocMenuCService.UpdateDocMenuC2EditAsync(model);

            if (e.Status) return Ok(e);
            else return BadRequest();
        }

        #endregion

        #region Menu C2_2

        [HttpGet("MenuC22InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC22InterfaceData(string userid, string username)
        {
            ModelMenuC22_InterfaceData e = await _IDocMenuCService.MenuC22InterfaceDataAsync(userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetDefaultRoundC3/{YearOf}")]
        public async Task<IActionResult> GetDefaultRoundC3(int yearof)
        {
            ModelCountOfYearC3 e = await _IDocMenuCService.GetDefaultRoundC3Async(yearof);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithDataC22/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC22(string ProjectNumber)
        {
            ModelMenuC22Data e = await _IDocMenuCService.GetProjectNumberWithDataC22Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllAssignerUserC22")]
        public async Task<IActionResult> GetAllAssignerUserC22()
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllAssignerUserC22Async();

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetRegisterUserDataC22/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC22(string RegisterId)
        {
            ModelMenuC22Data e = await _IDocMenuCService.GetRegisterUserDataC22Async(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC22")]
        public async Task<IActionResult> AddDocMenuC22([FromBody]ModelMenuC22 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC22Message e = await _IDocMenuCService.AddDocMenuC22Async(model);

            if (e.Status) return Ok();
            else return BadRequest(e);

        }

        #endregion Menu C2_2

        #region Menu C3

        // บันทึกวาระการประชุม --------------------------------------------------------------------------------------------

        [HttpGet("MenuC3InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC3InterfaceData(string RegisterId)
        {
            ModelMenuC3_InterfaceData e = await _IDocMenuCService.MenuC3InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllHistoryDataC3")]
        public async Task<IActionResult> GetAllHistoryDataC3()
        {
            IList<ModelMenuC3_History> e = await _IDocMenuCService.GetAllHistoryDataC3Async();

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC3")]
        public async Task<IActionResult> AddDocMenuC3([FromBody]ModelMenuC3 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC3Message e = await _IDocMenuCService.AddDocMenuC3Async(model);

            if (e.Status)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }




        // ระเบียบวาระที่ 3 แก้ไข --------------------------------------------------------------------------------------------

        [HttpGet("MenuC3EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC3EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC3_InterfaceData e = await _IDocMenuCService.MenuC3EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC3Edit")]
        public async Task<IActionResult> UpdateDocMenuC3Edit([FromBody]ModelMenuC3 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC3Message e = await _IDocMenuCService.UpdateDocMenuC3EditAsync(model);

            if (e.Status)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }


        // ระเบียบวาระที่ 1 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC31InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC31InterfaceData(string RegisterId)
        {
            ModelMenuC31_InterfaceData e = await _IDocMenuCService.MenuC31InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC31")]
        public async Task<IActionResult> AddDocMenuC31([FromBody]ModelMenuC31 model)
        {
            ModelResponseC31Message e = await _IDocMenuCService.AddDocMenuC31Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        // ระเบียบวาระที่ 1 แก้ไข 
        [HttpGet("MenuC31EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC31EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC31_InterfaceData e = await _IDocMenuCService.MenuC31EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC31Edit")]
        public async Task<IActionResult> UpdateDocMenuC31Edit([FromBody]ModelMenuC31 model)
        {
            ModelResponseC31Message e = await _IDocMenuCService.UpdateDocMenuC31EditAsync(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }


        // ระเบียบวาระที่ 2 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC32InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC32InterfaceData(string RegisterId)
        {
            ModelMenuC32_InterfaceData e = await _IDocMenuCService.MenuC32InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("MenuC32CheckAttachment/{meetingid}")]
        public async Task<IActionResult> MenuC32CheckAttachment(int meetingid)
        {
            bool resp = await _IDocMenuCService.MenuC32CheckAttachmentAsync(meetingid);

            if (resp == true) return Ok(true);
            else return BadRequest();

        }

        [HttpGet("MenuC32DownloadAttachmentZip/{meetingid}")]
        public async Task<IActionResult> MenuC32DownloadAttachmentZip(int meetingid)
        {
            ModelMenuC32_DownloadFile e = await _IDocMenuCService.MenuC32DownloadAttachmentZipAsync(meetingid);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetC32DownloadFileById/{meetingid}/{id}")]
        public async Task<IActionResult> GetC32DownloadFileById(int meetingid, int id)
        {
            ModelMenuC32_DownloadFile e = await _IDocMenuCService.GetC32DownloadFileByIdAsync(meetingid, id);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC32")]
        public async Task<IActionResult> AddDocMenuC32([FromBody]ModelMenuC32 model)
        {
            ModelResponseC32Message e = await _IDocMenuCService.AddDocMenuC32Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }



        // ระเบียบวาระที่ 2 แก้ไข 
        [HttpGet("MenuC32EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC32EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC32_InterfaceData e = await _IDocMenuCService.MenuC32EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC32Edit")]
        public async Task<IActionResult> UpdateDocMenuC32Edit([FromBody]ModelMenuC32 model)
        {
            ModelResponseC32Message e = await _IDocMenuCService.UpdateDocMenuC32EditAsync(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }




        // ระเบียบวาระที่ 3 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC33InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC33InterfaceData(string RegisterId)
        {
            ModelMenuC33_InterfaceData e = await _IDocMenuCService.MenuC33InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC33")]
        public async Task<IActionResult> AddDocMenuC33([FromBody]ModelMenuC33 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC33Message e = await _IDocMenuCService.AddDocMenuC33Async(model);

            if (e.Status == true)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }

        [HttpGet("GetAllApprovalTypeByProjectC2ForTab3/{ProjectNumber}")]
        public async Task<IActionResult> GetAllApprovalTypeByProjectC2ForTab3(string ProjectNumber)
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllApprovalTypeByProjectC2ForTab3Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataC3Tab3/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC3Tab3(string ProjectNumber)
        {
            ModelMenuC33Data e = await _IDocMenuCService.GetProjectNumberWithDataC3Tab3Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllHistoryDataC3Tab3")]
        public async Task<IActionResult> GetAllHistoryDataC3Tab3()
        {
            IList<ModelMenuC33HistoryData> e = await _IDocMenuCService.GetAllHistoryDataC3Tab3Async();

            if (e != null) return Ok(e);
            else return BadRequest();

        }


        // ระเบียบวาระที่ 3 แก้ไข 
        [HttpGet("MenuC33EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC33EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC33_InterfaceData e = await _IDocMenuCService.MenuC33EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC33Edit")]
        public async Task<IActionResult> UpdateDocMenuC33Edit([FromBody]ModelMenuC33 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC33Message e = await _IDocMenuCService.UpdateDocMenuC33EditAsync(model);

            if (e.Status == true)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }


        // ระเบียบวาระที่ 4 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC34InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC34InterfaceData(string RegisterId)
        {
            ModelMenuC34_InterfaceData e = await _IDocMenuCService.MenuC34InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC34")]
        public async Task<IActionResult> AddDocMenuC34([FromBody]ModelMenuC34 model)
        {

            IActionResult _result = BadRequest();

            ModelResponseC34Message e = await _IDocMenuCService.AddDocMenuC34Async(model);

            if (e.Status == true)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }

        [HttpGet("GetProjectNumberWithDataC3Tab4/{Type}/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC3Tab4(int type, string ProjectNumber)
        {
            ModelMenuC34Tab4Data e = await _IDocMenuCService.GetProjectNumberWithDataC3Tab4Async(type, ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllProjectNumberTab4/{Type}")]
        public async Task<IActionResult> GetAllProjectNumberTab4(int type)
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllProjectNumberTab4Async(type);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetC34DownloadFileById/{docid}")]
        public async Task<IActionResult> GetC34DownloadFileById(int docid)
        {
            ModelMenuC34_DownloadFile e = await _IDocMenuCService.GetC34DownloadFileByIdAsync(docid);

            if (e != null) return Ok(e);
            else return BadRequest();

        }



        // ระเบียบวาระที่ 4 แก้ไข 
        [HttpGet("MenuC34EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC34EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC34_InterfaceData e = await _IDocMenuCService.MenuC34EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC34Edit")]
        public async Task<IActionResult> UpdateDocMenuC34Edit([FromBody]ModelMenuC34 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseC34Message e = await _IDocMenuCService.UpdateDocMenuC34EditAsync(model);

            if (e.Status == true)
            {
                _result = Ok(e);
            }
            else _result = BadRequest(e);

            return _result;

        }



        // ระเบียบวาระที่ 5 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC35InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC35InterfaceData(string RegisterId)
        {
            ModelMenuC35_InterfaceData e = await _IDocMenuCService.MenuC35InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuC35")]
        public async Task<IActionResult> AddDocMenuC35([FromBody]ModelMenuC35 model)
        {
            ModelResponseC35Message e = await _IDocMenuCService.AddDocMenuC35Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        // ระเบียบวาระที่ 5 แก้ไข 
        [HttpGet("MenuC35EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC35EditInterfaceData(string UserId, string ProectNumber)
        {
            ModelMenuC35_InterfaceData e = await _IDocMenuCService.MenuC35EditInterfaceDataAsync(UserId, ProectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuC35Edit")]
        public async Task<IActionResult> UpdateDocMenuC35Edit([FromBody]ModelMenuC35 model)
        {
            ModelResponseC35Message e = await _IDocMenuCService.UpdateDocMenuC35EditAsync(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }



        //พิมพ์วาระการประชุม -------------------------------------------------------
        [HttpGet("PrintReportAgendaDraft/{DocId}/{Round}/{Year}")]
        public async Task<IActionResult> PrintReportAgendaDraft(int DocId, int Round, int Year)
        {

            IActionResult _result = BadRequest();

            ModelResponseMessageReportAgenda e = await _IDocMenuCService.PrintReportAgendaDraftAsync(DocId, Round, Year);

            if (e.Status)
            {
                _result = Ok(e);
            }
            else _result = BadRequest();

            return _result;

        }

        [HttpPost("PrintReportAgendaReal")]
        public async Task<IActionResult> PrintReportAgendaReal([FromBody]ModelPrintMeeting model)
        {

            IActionResult _result = BadRequest();

            ModelResponseMessageReportAgenda e = await _IDocMenuCService.PrintReportAgendaRealAsync(model);

            if (e.Status)
            {
                _result = Ok(e);

                await _IMailTemplateService.MailTemplate6Async(model.meetingofround, model.meetingofyear, e.filebase64);

            }
            else _result = BadRequest();

            return _result;

        }



        //พิมพ์รายงานการประชุม -------------------------------------------------------

        [HttpGet("PrintReportMeetingDraft/{DocId}/{Round}/{Year}")]
        public async Task<IActionResult> PrintReportMeetingDraft(int DocId, int Round, int Year)
        {

            IActionResult _result = BadRequest();

            ModelResponseMessageReportMeeting e = await _IDocMenuCService.PrintReportMeetingDraftAsync(DocId, Round, Year);

            if (e.Status)
            {
                _result = Ok(e);
            }
            else _result = BadRequest();

            return _result;

        }

        [HttpPost("PrintReportMeetingReal")]
        public async Task<IActionResult> PrintReportMeetingReal([FromBody]ModelPrintMeeting model)
        {

            IActionResult _result = BadRequest();

            ModelResponseMessageReportMeeting e = await _IDocMenuCService.PrintReportMeetingRealAsync(model);

            if (e.Status)
            {
                _result = Ok(e);

                await _IMailTemplateService.MailMeetingCompleteAsync(model.meetingofround, model.meetingofyear, e);

            }
            else _result = BadRequest();

            return _result;

        }







        // History -----------------------------------------------------------------------

        //History C3 Tab4
        [HttpGet("MenuC3Tab4InterfaceHistoryData")]
        public async Task<IActionResult> MenuC3Tab4InterfaceHistoryData()
        {
            ModelMenuC3Tab4_InterfaceData_History e = await _IDocMenuCService.MenuC3Tab4InterfaceHistoryDataAsync();

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportHistoryDataC3Tab4")]
        public async Task<IActionResult> GetAllReportHistoryDataC3Tab4(ModelMenuC3Tab4_InterfaceData_History search)
        {
            IList<ModelMenuC3Tab4_Data> e = await _IDocMenuCService.GetAllReportHistoryDataC3Tab4Async(search);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion


        // Share ---------------------------------

        [HttpGet("GetAllProject/{AssignerCode}/{DocProcess}")]
        public async Task<IActionResult> GetAllProject(string AssignerCode, string DocProcess)
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllProjectAsync(AssignerCode, DocProcess);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetAllProjectLab/{AssignerCode}/{DocProcess}")]
        public async Task<IActionResult> GetAllProjectLab(string AssignerCode, string DocProcess)
        {
            IList<ModelSelectOption> e = await _IDocMenuCService.GetAllProjectLabAsync(AssignerCode, DocProcess);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

    }
}