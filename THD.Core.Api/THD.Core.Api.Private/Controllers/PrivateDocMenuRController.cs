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
using System.IO;

namespace THD.Core.Api.Private.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateDocMenuRController : ControllerBase
    {
        private readonly IDocMenuRService _IDocMenuRService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateDocMenuRController(
            IDocMenuRService IDocMenuRService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _IDocMenuRService = IDocMenuRService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }

        //Menu R1
        [HttpGet("MenuR1InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuR1InterfaceData(string RegisterId)
        {
            ModelMenuR1_InterfaceData e = await _IDocMenuRService.MenuR1InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportHistoryDataR1")]
        public async Task<IActionResult> GetAllReportHistoryDataR1(ModelMenuR1_InterfaceData search)
        {
            IList<ModelMenuR1Data> e = await _IDocMenuRService.GetAllReportHistoryDataR1Async(search);

            if (e != null) return Ok(e);
            else return BadRequest();
        }


    }
}