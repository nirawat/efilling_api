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
    public class PublicDocMenuReportController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuReportController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        [HttpGet("GetReportR1_2/{DocId}")]
        public async Task<IActionResult> GetReportR1_2(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR1_2"}/{DocId}";
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

        [HttpGet("GetReportR3/{DocId}")]
        public async Task<IActionResult> GetReportR3(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR3"}/{DocId}";
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

        [HttpGet("GetReportR4/{DocId}")]
        public async Task<IActionResult> GetReportR4(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR4"}/{DocId}";
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

        [HttpGet("GetReportR5/{DocId}")]
        public async Task<IActionResult> GetReportR5(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR5"}/{DocId}";
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

        [HttpGet("GetReportR6/{DocId}")]
        public async Task<IActionResult> GetReportR6(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR6"}/{DocId}";
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

        [HttpGet("GetReportR7/{DocId}")]
        public async Task<IActionResult> GetReportR7(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR7"}/{DocId}";
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

        [HttpGet("GetReportR8/{DocId}")]
        public async Task<IActionResult> GetReportR8(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR8"}/{DocId}";
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

        [HttpGet("GetReportR9/{DocId}")]
        public async Task<IActionResult> GetReportR9(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR9"}/{DocId}";
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

        [HttpGet("GetReportR10/{DocId}")]
        public async Task<IActionResult> GetReportR10(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR10"}/{DocId}";
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

        [HttpGet("GetReportR11/{DocId}")]
        public async Task<IActionResult> GetReportR11(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR11"}/{DocId}";
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

        [HttpGet("GetReportR12/{DocId}/{type}")]
        public async Task<IActionResult> GetReportR12(int DocId, int type)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR12"}/{DocId}/{type}";
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

        [HttpGet("GetReportR13/{DocId}/{type}")]
        public async Task<IActionResult> GetReportR13(int DocId, int type)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR13"}/{DocId}/{type}";
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

        [HttpGet("GetReportR14/{DocId}")]
        public async Task<IActionResult> GetReportR14(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR14"}/{DocId}";
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

        [HttpGet("GetReportR15/{DocId}")]
        public async Task<IActionResult> GetReportR15(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR15"}/{DocId}";
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

        [HttpGet("GetReportR17_18/{DocId}")]
        public async Task<IActionResult> GetReportR17(int DocId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuReport"}/{"GetReportR17_18"}/{DocId}";
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


    }
}