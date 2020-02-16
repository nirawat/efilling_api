using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using THD.Core.Api.Models;
using THD.Core.Api.Public.Models;

namespace THD.Core.Api.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicDocMenuFController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuFController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        #region Menu F1

        [HttpGet("MenuF1InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuF1InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"MenuF1InterfaceData"}/{RegisterId}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpPost("GetAllReportDataF1")]
        public async Task<IActionResult> GetAllReportDataF1(ModelMenuF1_InterfaceData SearchData)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"GetAllReportDataF1"}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Post(requestUri, BearerToken, SearchData);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        #endregion

        #region Menu F1 Edit

        [HttpGet("MenuF1EditInterfaceData/{RegisterId}/{UserId}")]
        public async Task<IActionResult> MenuF1EditInterfaceData(string RegisterId, string UserId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"MenuF1EditInterfaceData"}/{RegisterId}/{UserId}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpPost("UpdateUserRegister")]
        public async Task<IActionResult> UpdateUserRegister(ModelRegisterEdit model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"UpdateUserRegister"}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Post(requestUri, BearerToken, model);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }
        }

        #endregion

        #region Menu F2

        [HttpGet("MenuF2InterfaceData/{RegisterId}/{UserGroup}")]
        public async Task<IActionResult> MenuF2InterfaceData(string RegisterId, string UserGroup)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"MenuF2InterfaceData"}/{RegisterId}/{UserGroup}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpPost("GetAllReportDataF2")]
        public async Task<IActionResult> GetAllReportDataF2(ModelMenuF2_InterfaceData SearchData)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"GetAllReportDataF2"}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Post(requestUri, BearerToken, SearchData);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpGet("GetUserEditPermissionF2/{UserGroup}/{MenuCode}")]
        public async Task<IActionResult> GetUserEditPermissionF2(string UserGroup, string MenuCode)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"GetUserEditPermissionF2"}/{UserGroup}/{MenuCode}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpPost("UpdatePermissionGroup")]
        public async Task<IActionResult> UpdatePermissionGroup(ModelMenuF2Edit model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"UpdatePermissionGroup"}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Post(requestUri, BearerToken, model);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }
        }

        #endregion

        #region Menu F Account User

        [HttpGet("MenuAccountInterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuAccountInterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"MenuAccountInterfaceData"}/{RegisterId}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Get(requestUri, BearerToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }

        }

        [HttpPost("UpdateUserAccount")]
        public async Task<IActionResult> UpdateUserAccount(ModelUpdateAccountUser model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuF"}/{"UpdateUserAccount"}";
            string authHeader = HttpContext.Request?.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                BearerToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            var response = await HttpRequestFactory.Post(requestUri, BearerToken, model);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.ContentAsString());
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ContentAsString());
                case HttpStatusCode.OK:
                    return Ok(response.ContentAsString());
                default:
                    return StatusCode(500);
            }
        }
        #endregion

    }
}