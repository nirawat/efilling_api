using THD.Core.Api.Business.Interface;
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
    public class PrivateDocMenuDController : ControllerBase
    {
        private readonly IDocMenuDService _IDocMenuDService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;
        private readonly IMailTemplateService _IMailTemplateService;

        public PrivateDocMenuDController(
            IDocMenuDService IDocMenuDService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig,
            IMailTemplateService MailTemplateService)
        {
            _IDocMenuDService = IDocMenuDService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
            _IMailTemplateService = MailTemplateService;
        }
        #region D1

        [HttpGet("MenuD1InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuD1InterfaceData(string RegisterId)
        {
            ModelMenuD1_InterfaceData e = await _IDocMenuDService.MenuD1InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithDataD1/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataD1(string ProjectNumber)
        {
            ModelMenuD1ProjectNumberData e = await _IDocMenuDService.GetProjectNumberWithDataD1Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuD1")]
        public async Task<IActionResult> AddDocMenuD1([FromBody]ModelMenuD1 model)
        {
            ModelResponseD1Message e = await _IDocMenuDService.AddDocMenuD1Async(model);

            if (e.Status)
            {
                await _IMailTemplateService.MailTemplate2Async(model.projectnumber, e.filebase64);

                return Ok(e);
            }
            else return BadRequest();

        }

        #endregion

        #region "Menu D1 Edit"

        [HttpGet("MenuD1EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuD1EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuD1_InterfaceData e = await _IDocMenuDService.MenuD1EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuD1Edit")]
        public async Task<IActionResult> UpdateDocMenuD1Edit([FromBody]ModelMenuD1 model)
        {
            ModelResponseD1Message e = await _IDocMenuDService.UpdateDocMenuD1EditAsync(model);

            if (e.Status)
            {
                await _IMailTemplateService.MailTemplate2Async(model.projectnumber, e.filebase64);

                return Ok(e);
            }
            else return BadRequest();

        }

        #endregion

        #region D2
        [HttpGet("MenuD2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuD2InterfaceData(string RegisterId)
        {
            ModelMenuD2_InterfaceData e = await _IDocMenuDService.MenuD2InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithDataD2/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataD2(string ProjectNumber)
        {
            ModelMenuD2ProjectNumberData e = await _IDocMenuDService.GetProjectNumberWithDataD2Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetAllDownloadFileByFileName/{FileName}")]
        public async Task<IActionResult> GetAllDownloadFileByFileName(string FileName)
        {
            ModelMenuD2_FileDownload e = await _IDocMenuDService.GetAllDownloadFileByFileNameAsync(FileName);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuD2")]
        public async Task<IActionResult> AddDocMenuD2([FromBody]ModelMenuD2 model)
        {
            ModelResponseD2Message e = await _IDocMenuDService.AddDocMenuD2Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }
        #endregion

    }
}