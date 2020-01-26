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
    public class PrivateSystemController : ControllerBase
    {
        private readonly ISystemService _ISystemService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateSystemController(
            ISystemService ISystemService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _ISystemService = ISystemService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(ModelUserLogin model)
        {
            ModelResponseMessageLogin e = await _ISystemService.LogInAsync(model);

            if (e != null && e.Status) return Ok(e);
            return BadRequest();
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut(ModelUserLogin model)
        {
            ModelResponseMessageLogin e = await _ISystemService.LogOutAsync(model);

            if (e != null && e.Status) return Ok(e);
            return BadRequest();
        }



    }
}