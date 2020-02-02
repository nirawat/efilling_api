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
    public class PublicDocMenuAController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuAController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        [HttpGet("MenuA1InterfaceData/{Userid}/{Username}")]
        public async Task<IActionResult> MenuA1InterfaceData(string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA1InterfaceData"}/{userid}/{username}";
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

        [HttpPost("AddDocMenuA1")]
        public async Task<IActionResult> AddDocMenuA1([FromBody]ModelMenuA1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA1"}";
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

        #region Menu A1 Edit

        [HttpGet("MenuA1InterfaceDataEdit/{DocId}/{Userid}/{Username}")]
        public async Task<IActionResult> MenuA1InterfaceDataEdit(int DocId, string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA1InterfaceDataEdit"}/{DocId}/{userid}/{username}";
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

        [HttpGet("GetA1DownloadFileById/{DocId}/{Id}")]
        public async Task<IActionResult> GetA1DownloadFileById(int DocId, int Id)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetA1DownloadFileById"}/{DocId}/{Id}";
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

        [HttpPost("UpdateDocMenuA1Edit")]
        public async Task<IActionResult> UpdateDocMenuA1Edit([FromBody]ModelMenuA1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"UpdateDocMenuA1Edit"}";
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

        [HttpGet("MenuA2InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA2InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA2InterfaceData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuA2")]
        public async Task<IActionResult> AddDocMenuA2([FromBody]ModelMenuA2 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA2"}";
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




        [HttpGet("MenuA3InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA3InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA3InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataA3/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA3(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetProjectNumberWithDataA3"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuA3")]
        public async Task<IActionResult> AddDocMenuA3([FromBody]ModelMenuA3 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA3"}";
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

        #region "Menu A3 Edit"

        [HttpGet("MenuA3EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA3EditInterfaceData(string UserId, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA3EditInterfaceData"}/{UserId}/{ProjectNumber}";
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

        #endregion


        [HttpGet("MenuA4InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA4InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA4InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataA4/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA4(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetProjectNumberWithDataA4"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuA4")]
        public async Task<IActionResult> AddDocMenuA4([FromBody]ModelMenuA4 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA4"}";
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

        #region "Menu A4 Edit"

        [HttpGet("MenuA4EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA4EditInterfaceData(string UserId, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA4EditInterfaceData"}/{UserId}/{ProjectNumber}";
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

        #endregion


        [HttpGet("MenuA5InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA5InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA5InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataA5/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA5(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetProjectNumberWithDataA5"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuA5")]
        public async Task<IActionResult> AddDocMenuA5([FromBody]ModelMenuA5 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA5"}";
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

        #region "Menu A5 Edit"

        [HttpGet("MenuA5EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA5EditInterfaceData(string UserId, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA5EditInterfaceData"}/{UserId}/{ProjectNumber}";
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

        #endregion


        [HttpGet("MenuA6InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA6InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA6InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataA6/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA6(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetProjectNumberWithDataA6"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuA6")]
        public async Task<IActionResult> AddDocMenuA6([FromBody]ModelMenuA6 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA6"}";
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

        #region "Menu A6 Edit"

        [HttpGet("MenuA6EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA6EditInterfaceData(string UserId, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA6EditInterfaceData"}/{UserId}/{ProjectNumber}";
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

        #endregion


        [HttpGet("MenuA7InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuA7InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA7InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataA7/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataA7(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"GetProjectNumberWithDataA7"}/{ProjectNumber}";
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

        [HttpPost("AddDocMenuA7")]
        public async Task<IActionResult> AddDocMenuA7([FromBody]ModelMenuA7 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"AddDocMenuA7"}";
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

        #region "Menu A7 Edit"

        [HttpGet("MenuA7EditInterfaceData/{UserId}/{ProjectNumber}")]
        public async Task<IActionResult> MenuA7EditInterfaceData(string UserId, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuA"}/{"MenuA7EditInterfaceData"}/{UserId}/{ProjectNumber}";
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

        #endregion

    }
}