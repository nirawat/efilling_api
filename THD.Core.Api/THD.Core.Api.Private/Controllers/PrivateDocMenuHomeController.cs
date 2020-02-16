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
    public class PrivateDocMenuHomeController : ControllerBase
    {
        private readonly IDocMenuHomeService _IDocMenuHomeService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateDocMenuHomeController(
            IDocMenuHomeService IDocMenuHomeService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _IDocMenuHomeService = IDocMenuHomeService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }

        [HttpGet("MenuHome1InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuHome1InterfaceData(string RegisterId)
        {
            ModelMenuHome1_InterfaceData e = await _IDocMenuHomeService.MenuHome1InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("GetResultNoteHome1/{ProjectNumber}/{UserId}")]
        public async Task<IActionResult> GetResultNoteHome1(string ProjectNumber, string UserId)
        {
            IList<ResultCommentNote> e = await _IDocMenuHomeService.GetResultNoteHome1Async(ProjectNumber, UserId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpGet("DownloadFileHome1/{ProjectNumber}")]
        public async Task<IActionResult> DownloadFileHome1(string ProjectNumber)
        {
            ModelMenuHome1_DownloadFile e = await _IDocMenuHomeService.DownloadFileHome1Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportDataHome1")]
        public async Task<IActionResult> GetAllReportDataHome1([FromBody]ModelMenuHome1_InterfaceData search_data)
        {
            IList<ModelMenuHome1ReportData> e = await _IDocMenuHomeService.GetAllReportDataHome1Async(search_data);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("MenuHome2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuHome2InterfaceData(string RegisterId)
        {
            ModelMenuHome2_InterfaceData e = await _IDocMenuHomeService.MenuHome2InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();

        }

        [HttpGet("DownloadFileHome2/{ProjectNumber}")]
        public async Task<IActionResult> DownloadFileHome2(string ProjectNumber)
        {
            ModelMenuHome2_DownloadFile e = await _IDocMenuHomeService.DownloadFileHome2Async(ProjectNumber);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportDataHome2")]
        public async Task<IActionResult> GetAllReportDataHome2([FromBody]ModelMenuHome2_InterfaceData search_data)
        {
            IList<ModelMenuHome2ReportData> e = await _IDocMenuHomeService.GetAllReportDataHome2Async(search_data);

            if (e != null) return Ok(e);
            else return BadRequest();

        }
    }
}