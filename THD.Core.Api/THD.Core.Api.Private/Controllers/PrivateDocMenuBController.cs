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
    public class PrivateDocMenuBController : ControllerBase
    {
        private readonly IDocMenuBService _IDocMenuBService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateDocMenuBController(
            IDocMenuBService IDocMenuBService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _IDocMenuBService = IDocMenuBService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }

        //Menu B1
        #region Menu B1

        [HttpGet("MenuB1InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuB1InterfaceData(string userid, string username)
        {
            ModelMenuB1_InterfaceData e = await _IDocMenuBService.MenuB1InterfaceDataAsync(userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetAllProjectNameThai/{ProjectHead}")]
        public async Task<IActionResult> GetAllProjectNameThai(string ProjectHead)
        {
            IList<ModelSelectOption> e = await _IDocMenuBService.GetAllProjectNameThaiAsync(ProjectHead);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetDataByProjectNameThai/{ProjectId}")]
        public async Task<IActionResult> GetDataByProjectNameThai(int ProjectId)
        {
            ModelMenuB1_GetDataByProjectNameThai e = await _IDocMenuBService.GetDataByProjectNameThaiAsync(ProjectId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetAllDownloadFileByProjectId/{ProjectId}")]
        public async Task<IActionResult> GetAllDownloadFileByProjectId(string ProjectId)
        {
            IList<ModelSelectOption> e = await _IDocMenuBService.GetAllDownloadFileByProjectIdAsync(ProjectId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetAllDownloadFileByFileName/{FileName}")]
        public async Task<IActionResult> GetAllDownloadFileByFileName(string FileName)
        {
            ModelMenuB1_FileDownload e = await _IDocMenuBService.GetAllDownloadFileByFileNameAsync(FileName);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetProjectNumberWithData/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithData(string ProjectNumber)
        {
            ModelMenuB1Data e = await _IDocMenuBService.GetProjectNumberWithDataAsync(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuB1")]
        public async Task<IActionResult> AddDocMenuB1([FromBody]ModelMenuB1 model)
        {
            ModelResponseMessageAddDocB1 e = await _IDocMenuBService.AddDocMenuB1Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #endregion

        #region Menu B1 Edit

        [HttpGet("MenuB1InterfaceDataEdit/{ProjectNumber}/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuB1InterfaceData(string projectnumber, string userid, string username)
        {
            ModelMenuB1_InterfaceData e = await _IDocMenuBService.MenuB1InterfaceDataEditAsync(projectnumber, userid, username);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateDocMenuB1")]
        public async Task<IActionResult> UpdateDocMenuB1([FromBody]ModelMenuB1Edit model)
        {
            ModelResponseMessageAddDocB1 e = await _IDocMenuBService.UpdateDocMenuB1Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #endregion

        //Menu B2
        #region Menu B2

        [HttpGet("MenuB2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuB2InterfaceData(string RegisterId)
        {
            ModelMenuB2_InterfaceData e = await _IDocMenuBService.MenuB2InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetAllLabNumber")]
        public async Task<IActionResult> GetAllLabNumber()
        {
            IList<ModelSelectOption> e = await _IDocMenuBService.GetAllLabNumberAsync();

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetDownloadFileByFileNameB2/{FileName}")]
        public async Task<IActionResult> GetDownloadFileByFileNameB2(string FileName)
        {
            ModelMenuB2_FileDownload e = await _IDocMenuBService.GetDownloadFileByFileNameB2Async(FileName);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("GetLabNumberWithDataB2/{LabNumber}")]
        public async Task<IActionResult> GetLabNumberWithDataB2(string LabNumber)
        {
            ModelMenuB2Data e = await _IDocMenuBService.GetLabNumberWithDataB2Async(LabNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuB2")]
        public async Task<IActionResult> AddDocMenuB2([FromBody]ModelMenuB2 model)
        {
            ModelResponseMessageAddDocB2 e = await _IDocMenuBService.AddDocMenuB2Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();

        }

        #endregion


    }
}