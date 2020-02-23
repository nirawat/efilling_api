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
    public class PrivateDocMenuEController : ControllerBase
    {
        private readonly IDocMenuEService _IDocMenuEService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateDocMenuEController(
            IDocMenuEService IDocMenuEService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _IDocMenuEService = IDocMenuEService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }


        [HttpGet("MenuE1InterfaceData/{RegisterId}/{Passw}")]
        public async Task<IActionResult> MenuE1InterfaceData(string RegisterId, string Passw)
        {
            ModelMenuE1_InterfaceData e = await _IDocMenuEService.MenuE1InterfaceDataAsync(RegisterId, Passw);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("AddDocMenuE1")]
        public async Task<IActionResult> AddDocMenuE1([FromBody]ModelMenuE1 model)
        {
            ModelResponseMessage e = await _IDocMenuEService.AddDocMenuE1Async(model);

            if (e.Status) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("MenuE1InterfaceReportData")]
        public async Task<IActionResult> MenuE1InterfaceReportData([FromBody]ModelMenuE1_InterfaceReportData model)
        {
            ModelMenuE1_InterfaceReportData e = await _IDocMenuEService.MenuE1InterfaceReportDataAsync(model);

            return Ok(e);
        }

    }
}