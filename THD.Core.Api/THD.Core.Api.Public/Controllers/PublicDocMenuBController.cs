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
    public class PublicDocMenuBController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuBController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        //Menu B1
        #region Menu B1

        [HttpGet("MenuB1InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuB1InterfaceData(string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"MenuB1InterfaceData"}/{userid}/{username}";
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

        [HttpGet("GetAllProjectNameThai/{ProjectHead}")]
        public async Task<IActionResult> GetAllProjectNameThai(string ProjectHead)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetAllProjectNameThai"}/{ProjectHead}";
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

        [HttpGet("GetAllDownloadFileByProjectId/{ProjectId}")]
        public async Task<IActionResult> GetAllDownloadFileByProjectId(string ProjectId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetAllDownloadFileByProjectId"}/{ProjectId}";
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

        [HttpGet("GetAllDownloadFileByFileName/{FileName}")]
        public async Task<IActionResult> GetAllDownloadFileByFileName(string FileName)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetAllDownloadFileByFileName"}/{FileName}";
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

        [HttpGet("GetDataByProjectNameThai/{ProjectId}")]
        public async Task<IActionResult> GetDataByProjectNameThai(string ProjectId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetDataByProjectNameThai"}/{ProjectId}";
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

        [HttpGet("GetProjectNumberWithData/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithData(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetProjectNumberWithData"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuB1")]
        public async Task<IActionResult> AddDocMenuB1([FromBody]ModelMenuB1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"AddDocMenuB1"}";
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

        #region Menu B1 Edit

        [HttpGet("MenuB1InterfaceDataEdit/{ProjectNumber}/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuB1InterfaceDataEdit(string projectnumber, string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"MenuB1InterfaceDataEdit"}/{projectnumber}/{userid}/{username}";
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

        [HttpPost("UpdateDocMenuB1")]
        public async Task<IActionResult> UpdateDocMenuB1([FromBody]ModelMenuB1Edit model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"UpdateDocMenuB1"}";
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

        #region Menu B2

        [HttpGet("MenuB2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuB2InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"MenuB2InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetAllLabNumber")]
        public async Task<IActionResult> GetAllLabNumber()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetAllLabNumber"}";
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

        [HttpGet("GetDownloadFileByFileNameB2/{FileName}")]
        public async Task<IActionResult> GetDownloadFileByFileNameB2(string FileName)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetDownloadFileByFileNameB2"}/{FileName}";
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

        [HttpGet("GetLabNumberWithDataB2/{LabNumber}")]
        public async Task<IActionResult> GetLabNumberWithDataB2(string LabNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"GetLabNumberWithDataB2"}/{LabNumber}";
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

        [HttpPost("AddDocMenuB2")]
        public async Task<IActionResult> AddDocMenuB2([FromBody]ModelMenuB2 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuB"}/{"AddDocMenuB2"}";
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