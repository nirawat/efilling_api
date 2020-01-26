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
    public class PublicDocMenuEController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuEController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }



        [HttpGet("MenuE1InterfaceData/{RegisterId}/{Passw}")]
        public async Task<IActionResult> MenuE1InterfaceData(string RegisterId, string Passw)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuE"}/{"MenuE1InterfaceData"}/{RegisterId}/{Passw}";
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

        [HttpPost("AddDocMenuE1")]
        public async Task<IActionResult> AddDocMenuE1([FromBody]ModelMenuE1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuE"}/{"AddDocMenuE1"}";
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


        [HttpGet("GetAllReportDataE1")]
        public async Task<IActionResult> GetAllReportDataE1()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuE"}/{"GetAllReportDataE1"}";
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