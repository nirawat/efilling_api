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
    public class PrivateDocMenuFController : ControllerBase
    {
        private readonly IDocMenuFService _IDocMenuFService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;

        public PrivateDocMenuFController(
            IDocMenuFService IDocMenuFService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig)
        {
            _IDocMenuFService = IDocMenuFService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
        }

        #region Menu F1
        [HttpGet("MenuF1InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuF1InterfaceData(string RegisterId)
        {
            ModelMenuF1_InterfaceData e = await _IDocMenuFService.MenuF1InterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportDataF1")]
        public async Task<IActionResult> GetAllReportDataF1(ModelMenuF1_InterfaceData SearchData)
        {
            IList<ModelMenuF1Report> e = await _IDocMenuFService.GetAllReportDataF1Async(SearchData);

            if (e != null) return Ok(e);
            else return BadRequest();

        }
        #endregion

        #region Menu F1 Edit
        [HttpGet("MenuF1EditInterfaceData/{RegisterId}/{UserId}")]
        public async Task<IActionResult> MenuF1EditInterfaceData(string RegisterId, string UserId)
        {
            ModelMenuF1Edit_InterfaceData e = await _IDocMenuFService.MenuF1EditInterfaceDataAsync(RegisterId, UserId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateUserRegister")]
        public async Task<IActionResult> UpdateUserRegisterAsync(ModelRegisterEdit model)
        {
            ModelResponseMessageUpdateUserRegister e = await _IDocMenuFService.UpdateUserRegisterAsync(model);

            if (e.Status) return Ok();
            else return BadRequest();

        }
        #endregion

        #region Menu F2
        [HttpGet("MenuF2InterfaceData/{RegisterId}/{UserGroup}")]
        public async Task<IActionResult> MenuF2InterfaceData(string RegisterId, string UserGroup)
        {
            ModelMenuF2_InterfaceData e = await _IDocMenuFService.MenuF2InterfaceDataAsync(RegisterId, UserGroup);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("GetAllReportDataF2")]
        public async Task<IActionResult> GetAllReportDataF2(ModelMenuF2_InterfaceData SearchData)
        {
            IList<ModelMenuF2Report> e = await _IDocMenuFService.GetAllReportDataF2Async(SearchData);

            if (e != null) return Ok(e);
            else return BadRequest();

        }


        [HttpGet("GetUserEditPermissionF2/{UserGroup}/{MenuCode}")]
        public async Task<IActionResult> GetUserEditPermissionF2(string UserGroup, string MenuCode)
        {
            ModelMenuF2Edit e = await _IDocMenuFService.GetUserEditPermissionF2Async(UserGroup, MenuCode);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdatePermissionGroup")]
        public async Task<IActionResult> UpdatePermissionGroup(ModelMenuF2Edit model)
        {
            ModelResponseMessageUpdateUserRegister e = await _IDocMenuFService.UpdatePermissionGroupAsync(model);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        #endregion

        #region Menu F Account User
        [HttpGet("MenuAccountInterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuAccountInterfaceData(string RegisterId)
        {
            ModelMenuFAccount_InterfaceData e = await _IDocMenuFService.MenuAccountInterfaceDataAsync(RegisterId);

            if (e != null) return Ok(e);
            else return BadRequest();
        }

        [HttpPost("UpdateUserAccount")]
        public async Task<IActionResult> UpdateUserAccount(ModelUpdateAccountUser model)
        {
            ModelResponseMessageUpdateUserRegister e = await _IDocMenuFService.UpdateUserAccountAsync(model);

            if (e.Status) return Ok();
            else return BadRequest();

        }
        #endregion

    }
}