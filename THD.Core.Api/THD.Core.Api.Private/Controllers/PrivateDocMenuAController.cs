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
    public class PrivateDocMenuAController : ControllerBase
    {
        private readonly IDocMenuAService _IDocMenuAService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;
        private readonly IMailTemplateService _IMailTemplateService;

        public PrivateDocMenuAController(
            IDocMenuAService IDocMenuAService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig,
            IMailTemplateService MailTemplateService)
        {
            _IDocMenuAService = IDocMenuAService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
            _IMailTemplateService = MailTemplateService;
        }

        [HttpGet("MenuA1InterfaceData/{Userid}/{Username}")]
        public async Task<IActionResult> MenuA1InterfaceData(string userid, string username)
        {
            ModelMenuA1_InterfaceData e = await _IDocMenuAService.MenuA1InterfaceDataAsync(userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuA1")]
        public async Task<IActionResult> AddDocMenuA1([FromBody]ModelMenuA1 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA1Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #region Menu A1 Edit

        [HttpGet("MenuA1InterfaceDataEdit/{DocId}/{Userid}/{Username}")]
        public async Task<IActionResult> MenuA1InterfaceDataEdit(int DocId, string userid, string username)
        {
            ModelMenuA1_InterfaceData e = await _IDocMenuAService.MenuA1InterfaceDataEditAsync(DocId, userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetA1DownloadFileById/{DocId}/{Id}")]
        public async Task<IActionResult> GetA1DownloadFileById(int DocId, int Id)
        {
            ModelMenuA1_FileDownload e = await _IDocMenuAService.GetA1DownloadFileByIdAsync(DocId, Id);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuA1Edit")]
        public async Task<IActionResult> UpdateDocMenuA1Edit([FromBody]ModelMenuA1 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.UpdateDocMenuA1EditAsync(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #endregion

        [HttpGet("MenuA2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA2InterfaceData(string RegisterId)
        {
            ModelMenuA2_InterfaceData e = await _IDocMenuAService.MenuA2InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuA2")]
        public async Task<IActionResult> AddDocMenuA2([FromBody]ModelMenuA2 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA2Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }



        [HttpGet("MenuA3InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA3InterfaceData(string RegisterId)
        {
            ModelMenuA3_InterfaceData e = await _IDocMenuAService.MenuA3InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataA3/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA3(string ProjectNumber)
        {
            ModelMenuA3ProjectNumberData e = await _IDocMenuAService.GetProjectNumberWithDataA3Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuA3")]
        public async Task<IActionResult> AddDocMenuA3([FromBody]ModelMenuA3 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA3Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #region "Menu A3 Edit"

        [HttpGet("MenuA3EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA3EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuA3_InterfaceData e = await _IDocMenuAService.MenuA3EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion

        [HttpGet("MenuA4InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA4InterfaceData(string RegisterId)
        {
            ModelMenuA4_InterfaceData e = await _IDocMenuAService.MenuA4InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataA4/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA4(string ProjectNumber)
        {
            ModelMenuA4ProjectNumberData e = await _IDocMenuAService.GetProjectNumberWithDataA4Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuA4")]
        public async Task<IActionResult> AddDocMenuA4([FromBody]ModelMenuA4 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA4Async(model);

            if (e.Status)
            {
                _result = Ok(e);

                try
                {
                    await _IMailTemplateService.MailTemplate9Async(model.projectnumber, e.filebase64);
                }
                catch (Exception ex)
                {
                    //Keep
                }

            }
            else _result = BadRequest();

            return _result;

        }

        #region "Menu A4 Edit"

        [HttpGet("MenuA4EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA4EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuA4_InterfaceData e = await _IDocMenuAService.MenuA4EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion



        [HttpGet("MenuA5InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA5InterfaceData(string RegisterId)
        {
            ModelMenuA5_InterfaceData e = await _IDocMenuAService.MenuA5InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithDataA5/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA5(string ProjectNumber)
        {
            ModelMenuA5ProjectNumberData e = await _IDocMenuAService.GetProjectNumberWithDataA5Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuA5")]
        public async Task<IActionResult> AddDocMenuA5([FromBody]ModelMenuA5 model)
        {
            IActionResult _result = BadRequest();

            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA5Async(model);

            if (e.Status)
            {
                _result = Ok(e);

                try
                {
                    await _IMailTemplateService.MailTemplate8Async(model.projectnumber, e.filebase64);
                }
                catch (Exception ex)
                {
                    //Keep
                }

            }
            else _result = BadRequest();

            return _result;

        }

        #region "Menu A5 Edit"

        [HttpGet("MenuA5EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA5EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuA5_InterfaceData e = await _IDocMenuAService.MenuA5EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion

        [HttpGet("MenuA6InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA6InterfaceData(string RegisterId)
        {
            ModelMenuA6_InterfaceData e = await _IDocMenuAService.MenuA6InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataA6/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA6(string ProjectNumber)
        {
            ModelMenuA6ProjectNumberData e = await _IDocMenuAService.GetProjectNumberWithDataA6Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpPost("AddDocMenuA6")]
        public async Task<IActionResult> AddDocMenuA6([FromBody]ModelMenuA6 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA6Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #region "Menu A6 Edit"

        [HttpGet("MenuA6EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA6EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuA6_InterfaceData e = await _IDocMenuAService.MenuA6EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion

        [HttpGet("MenuA7InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA7InterfaceData(string RegisterId)
        {
            ModelMenuA7_InterfaceData e = await _IDocMenuAService.MenuA7InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetProjectNumberWithDataA7/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA7(string ProjectNumber)
        {
            ModelMenuA7ProjectNumberData e = await _IDocMenuAService.GetProjectNumberWithDataA7Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuA7")]
        public async Task<IActionResult> AddDocMenuA7([FromBody]ModelMenuA7 model)
        {
            ModelResponseMessage e = await _IDocMenuAService.AddDocMenuA7Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #region "Menu A7 Edit"

        [HttpGet("MenuA7EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA7EditInterfaceData(string UserId, string ProjectNumber)
        {
            ModelMenuA7_InterfaceData e = await _IDocMenuAService.MenuA7EditInterfaceDataAsync(UserId, ProjectNumber);
            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion







    }
}