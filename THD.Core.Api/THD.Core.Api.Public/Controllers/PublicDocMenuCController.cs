using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
    public class PublicDocMenuCController : ControllerBase
    {
        private IWebApiModel _WebApiModel;
        private string BearerToken = "";
        public PublicDocMenuCController(IWebApiModel WebApiModel)
        {
            _WebApiModel = WebApiModel;
        }

        #region Menu C1

        [HttpGet("MenuC1InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC1InterfaceData(string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC1InterfaceData"}/{userid}/{username}";
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

        [HttpGet("GetMeetingRoundOfProject/{Year}")]
        public async Task<IActionResult> GetMeetingRoundOfProject(int Year)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetMeetingRoundOfProject"}/{Year}";
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

        [HttpGet("GetProjectNumberWithDataC1/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC1(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC1"}/{ProjectNumber}";
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

        [HttpGet("GetRegisterUserData/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetRegisterUserData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC1")]
        public async Task<IActionResult> AddDocMenuC1([FromBody]ModelMenuC1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC1"}";
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

        #region Menu C1 Edit

        [HttpGet("MenuC1InterfaceDataEdit/{ProjectNumber}/{RegisterId}")]
        public async Task<IActionResult> MenuC1InterfaceDataEdit(string ProjectNumber, string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC1InterfaceDataEdit"}/{ProjectNumber}/{RegisterId}";
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

        [HttpPost("UpdateDocMenuC1Edit")]
        public async Task<IActionResult> UpdateDocMenuC1Edit([FromBody]ModelMenuC1 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC1Edit"}";
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

        #region Menu C1_2

        [HttpGet("MenuC12InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC12InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC12InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetProjectNumberWithDataC12/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC12(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC12"}/{ProjectNumber}";
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

        [HttpGet("GetRegisterUserDataC12/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC12(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetRegisterUserDataC12"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC12")]
        public async Task<IActionResult> AddDocMenuC12([FromBody]ModelMenuC12 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC12"}";
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

        #region Menu C2

        [HttpGet("MenuC2InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC2InterfaceData(string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC2InterfaceData"}/{userid}/{username}";
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


        [HttpGet("GetProjectNumberWithDataC2/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC2(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC2"}/{ProjectNumber}";
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

        [HttpGet("GetAllAssignerUser")]
        public async Task<IActionResult> GetAllAssignerUser()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllAssignerUser"}";
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

        [HttpGet("GetRegisterUserDataC2/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC2(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetRegisterUserDataC2"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC2")]
        public async Task<IActionResult> AddDocMenuC2([FromBody]ModelMenuC2 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC2"}";
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

        #region Menu C2 Edit

        [HttpGet("MenuC2InterfaceDataEdit/{DocId}/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC2InterfaceDataEdit(int docid, string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC2InterfaceDataEdit"}/{docid}/{userid}/{username}";
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

        [HttpPost("UpdateDocMenuC2Edit")]
        public async Task<IActionResult> UpdateDocMenuC2Edit([FromBody]ModelMenuC2 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC2Edit"}";
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

        #region Menu C2_2

        [HttpGet("MenuC22InterfaceData/{UserId}/{UserName}")]
        public async Task<IActionResult> MenuC22InterfaceData(string userid, string username)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC22InterfaceData"}/{userid}/{username}";
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

        [HttpGet("GetAllProjectNumberC22/{AssignerCode}")]
        public async Task<IActionResult> GetAllProjectNumberC22(string AssignerCode)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllProjectNumberC22"}/{AssignerCode}";
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

        [HttpGet("GetProjectNumberWithDataC22/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataCC2(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC22"}/{ProjectNumber}";
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

        [HttpGet("GetAllAssignerUserC22")]
        public async Task<IActionResult> GetAllAssignerUserC22()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllAssignerUserC22"}";
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

        [HttpGet("GetRegisterUserDataC22/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserDataC22(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetRegisterUserDataC22"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC22")]
        public async Task<IActionResult> AddDocMenuC22([FromBody]ModelMenuC22 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC22"}";
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

        #region Menu C3

        // บันทึกวาระการประชุม --------------------------------------------------------------------------------------------

        [HttpGet("MenuC3InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC3InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC3InterfaceData"}/{RegisterId}";
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

        [HttpGet("GetDefaultRoundC3/{YearOf}")]
        public async Task<IActionResult> GetDefaultRoundC3(int YearOf)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetDefaultRoundC3"}/{YearOf}";
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

        [HttpGet("GetAllHistoryDataC3")]
        public async Task<IActionResult> GetAllHistoryDataC3()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllHistoryDataC3"}";
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

        [HttpPost("AddDocMenuC3")]
        public async Task<IActionResult> AddDocMenuC3([FromBody]ModelMenuC3 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC3"}";
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



        // บันทึกวาระการประชุม แก้ไข 

        [HttpGet("MenuC3EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC3EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC3EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC3Edit")]
        public async Task<IActionResult> UpdateDocMenuC3Edit([FromBody]ModelMenuC3 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC3Edit"}";
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



        // ระเบียบวาระที่ 1 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC31InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC31InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC31InterfaceData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC31")]
        public async Task<IActionResult> AddDocMenuC31([FromBody]ModelMenuC31 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC31"}";
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

        // ระเบียบวาระที่ 1 แก้ไข 

        [HttpGet("MenuC31EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC31EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC31EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC31Edit")]
        public async Task<IActionResult> UpdateDocMenuC31Edit([FromBody]ModelMenuC31 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC31Edit"}";
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


        // ระเบียบวาระที่ 2 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC32InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC32InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC32InterfaceData"}/{RegisterId}";
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

        [HttpGet("MenuC32CheckAttachment/{meetingid}")]
        public async Task<IActionResult> MenuC32CheckAttachment(int meetingid)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC32CheckAttachment"}/{meetingid}";
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

        [HttpGet("MenuC32DownloadAttachmentZip/{meetingid}")]
        public async Task<IActionResult> MenuC32DownloadAttachmentZip(int meetingid)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC32DownloadAttachmentZip"}/{meetingid}";
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

        [HttpGet("GetC32DownloadFileById/{meetingid}/{id}")]
        public async Task<IActionResult> GetC32DownloadFileById(int meetingid, int id)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetC32DownloadFileById"}/{meetingid}/{id}";
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

        [HttpPost("AddDocMenuC32")]
        public async Task<IActionResult> AddDocMenuC32([FromBody]ModelMenuC32 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC32"}";
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

        // ระเบียบวาระที่ 2 แก้ไข 

        [HttpGet("MenuC32EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC32EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC32EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC32Edit")]
        public async Task<IActionResult> UpdateDocMenuC32Edit([FromBody]ModelMenuC32 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC32Edit"}";
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



        // ระเบียบวาระที่ 3 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC33InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC33InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC33InterfaceData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC33")]
        public async Task<IActionResult> AddDocMenuC33([FromBody]ModelMenuC33 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC33"}";
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

        [HttpGet("GetProjectNumberWithDataC3Tab3/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC3Tab3(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC3Tab3"}/{ProjectNumber}";
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

        [HttpGet("GetAllApprovalTypeByProjectC2ForTab3/{ProjectNumber}")]
        public async Task<IActionResult> GetAllApprovalTypeByProjectC2ForTab3(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllApprovalTypeByProjectC2ForTab3"}/{ProjectNumber}";
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

        [HttpGet("GetAllHistoryDataC3Tab3")]
        public async Task<IActionResult> GetAllHistoryDataC3Tab3()
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllHistoryDataC3Tab3"}";
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

        // ระเบียบวาระที่ 3 แก้ไข 

        [HttpGet("MenuC33EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC33EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC33EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC33Edit")]
        public async Task<IActionResult> UpdateDocMenuC33Edit([FromBody]ModelMenuC33 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC33Edit"}";
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


        // ระเบียบวาระที่ 4 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC34InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC34InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC34InterfaceData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC34")]
        public async Task<IActionResult> AddDocMenuC34([FromBody]ModelMenuC34 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC34"}";
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

        [HttpGet("GetAllApprovalTypeByProjectC22ForTab4/{ProjectNumber}")]
        public async Task<IActionResult> GetAllApprovalTypeByProjectC22ForTab4(string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllApprovalTypeByProjectC22ForTab4"}/{ProjectNumber}";
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

        [HttpGet("GetProjectNumberWithDataC3Tab4/{Type}/{ProjectNumber}")]
        public async Task<IActionResult> GetProjectNumberWithDataC3Tab4(int Type, string ProjectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetProjectNumberWithDataC3Tab4"}/{Type}/{ProjectNumber}";
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

        [HttpGet("GetAllProjectNumberTab4/{Type}")]
        public async Task<IActionResult> GetAllProjectNumberTab4(int Type)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllProjectNumberTab4"}/{Type}";
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

        [HttpGet("GetC34DownloadFileById/{docid}")]
        public async Task<IActionResult> GetC34DownloadFileById(int docid)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetC34DownloadFileById"}/{docid}";
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


        // ระเบียบวาระที่ 4 แก้ไข 

        [HttpGet("MenuC34EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC34EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC34EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC34Edit")]
        public async Task<IActionResult> UpdateDocMenuC34Edit([FromBody]ModelMenuC34 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC34Edit"}";
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


        // ระเบียบวาระที่ 5 --------------------------------------------------------------------------------------------

        [HttpGet("MenuC35InterfaceData/{RegisterId}")]
        public async Task<IActionResult> MenuC35InterfaceData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC35InterfaceData"}/{RegisterId}";
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

        [HttpPost("AddDocMenuC35")]
        public async Task<IActionResult> AddDocMenuC35([FromBody]ModelMenuC35 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"AddDocMenuC35"}";
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


        // ระเบียบวาระที่ 5 แก้ไข 

        [HttpGet("MenuC35EditInterfaceData/{UserId}/{ProectNumber}")]
        public async Task<IActionResult> MenuC35EditInterfaceData(string UserId, string ProectNumber)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC35EditInterfaceData"}/{UserId}/{ProectNumber}";
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

        [HttpPost("UpdateDocMenuC35Edit")]
        public async Task<IActionResult> UpdateDocMenuC35Edit([FromBody]ModelMenuC35 model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"UpdateDocMenuC35Edit"}";
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



        //พิมพ์วาระการประชุม -------------------------------------------------------

        [HttpGet("PrintReportAgendaDraft/{DocId}/{Round}/{Year}")]
        public async Task<IActionResult> PrintReportAgendaDraft(int DocId, int Round, int Year)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"PrintReportAgendaDraft"}/{DocId}/{Round}/{Year}";
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

        [HttpPost("PrintReportAgendaReal")]
        public async Task<IActionResult> PrintReportAgendaReal([FromBody]ModelPrintMeeting model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"PrintReportAgendaReal"}";
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


        //พิมพ์รายงานการประชุม -------------------------------------------------------
        [HttpGet("PrintReportMeetingDraft/{DocId}/{Round}/{Year}")]
        public async Task<IActionResult> PrintReportMeetingDraft(int DocId, int Round, int Year)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"PrintReportMeetingDraft"}/{DocId}/{Round}/{Year}";
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

        [HttpPost("PrintReportMeetingReal")]
        public async Task<IActionResult> PrintReportMeetingReal([FromBody]ModelPrintMeeting model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"PrintReportMeetingReal"}";
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




        // History ----------------------------------------------------------------------------------------------

        [HttpGet("MenuC3Tab4InterfaceHistoryData/{RegisterId}")]
        public async Task<IActionResult> MenuC3Tab4InterfaceHistoryData(string RegisterId)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"MenuC3Tab4InterfaceHistoryData"}/{RegisterId}";
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


        [HttpPost("GetAllReportHistoryDataC3Tab4")]
        public async Task<IActionResult> GetAllReportHistoryDataC3Tab4([FromBody]ModelMenuC3Tab4_InterfaceData_History model)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllReportHistoryDataC3Tab4"}";
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



        #region Other Function
        [HttpGet("GetAllProject/{AssignerCode}/{DocProcess}")]
        public async Task<IActionResult> GetAllProject(string AssignerCode, string DocProcess)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllProject"}/{AssignerCode}/{DocProcess}";
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

        [HttpGet("GetAllProjectLab/{AssignerCode}/{DocProcess}")]
        public async Task<IActionResult> GetAllProjectLab(string AssignerCode, string DocProcess)
        {
            var requestUri = $"{_WebApiModel.BaseURL}/{"PrivateDocMenuC"}/{"GetAllProjectLab"}/{AssignerCode}/{DocProcess}";
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