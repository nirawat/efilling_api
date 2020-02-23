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
    public class PrivateRegisterController : ControllerBase
    {
        private readonly IRegisterUserService _IRegisterUserService;
        private IHttpContextAccessor _httpContextAccessor;
        private IEnvironmentConfig _EnvironmentConfig;
        private readonly IEmailHelper _EmailHelper;

        public PrivateRegisterController(
            IRegisterUserService IRegisterUserService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentConfig EnvironmentConfig,
            IEmailHelper EmailHelper)
        {
            _IRegisterUserService = IRegisterUserService;
            _httpContextAccessor = httpContextAccessor;
            _EnvironmentConfig = EnvironmentConfig;
            _EmailHelper = EmailHelper;
        }


        [HttpGet("ActiveUserAccountInterface/{RegisterId}")]
        public async Task<IActionResult> ActiveUserAccountInterface(string RegisterId)
        {
            string registerId = Encoding.UTF8.GetString(Convert.FromBase64String(RegisterId));

            ModelRegisterActive_InterfaceData e = await _IRegisterUserService.ActiveUserAccountInterfaceAsync(registerId);

            if (e != null) return Ok(e);
            return BadRequest();
        }

        [HttpGet("GetRegisterUserActive/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserActive(string RegisterId)
        {
            string registerId = Encoding.UTF8.GetString(Convert.FromBase64String(RegisterId));

            ModelRegisterActive e = await _IRegisterUserService.GetRegisterUserActiveAsync(registerId);

            if (e != null) return Ok(e);
            return BadRequest();
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody]ModelRegisterUser model)
        {
            IActionResult _result = BadRequest();

            ModelResponseMessageRegisterUser e = await _IRegisterUserService.AddRegisterUserAsync(model);

            if (e.Status == true)
            {
                _result = Ok(e);

                string serverip = Encoding.UTF8.GetString(Convert.FromBase64String(_EnvironmentConfig.Server));

                string registerId = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(e.RegisterId));

                string linkactive = $"{serverip}/{"efilling/RegisterActive"}?RegisterId={registerId}";

                string mail_body = "<h3>รหัสลงทะเบียนของคุณคือ</h3>" + Environment.NewLine +
                                   "<h2>" + e.RegisterId + "</h2>" + Environment.NewLine +
                                   "<h4>หากคุณต้องการยืนยันการลงทะเบียนกรุณาคลิ้ก <a href='" + linkactive + "'>ยืนยันการลงทะเบียน</a>.</h4>";

                await _EmailHelper.SentGmail(model.email, "eFilling : แจ้งการลงทะเบียน", mail_body, "");
            }
            return _result;

        }


        [HttpGet("GetFullRegisterUserById/{RegisterId}")]
        public async Task<IActionResult> GetFullRegisterUserById(string RegisterId)
        {
            string registerId = Encoding.UTF8.GetString(Convert.FromBase64String(RegisterId));

            ModelRegisterActive e = await _IRegisterUserService.GetFullRegisterUserByIdAsync(registerId);

            if (e != null) return Ok(e);
            return BadRequest();
        }

        [HttpGet("GetRegisterUserInActive/{RegisterId}")]
        public async Task<IActionResult> GetRegisterUserInActive(string RegisterId)
        {
            string registerId = Encoding.UTF8.GetString(Convert.FromBase64String(RegisterId));

            ModelRegisterActive e = await _IRegisterUserService.GetRegisterUserInActiveAsync(registerId);

            if (e != null) return Ok(e);
            return BadRequest();
        }

        [HttpPost("RegisterActive")]
        public async Task<IActionResult> RegisterActive([FromBody]ModelRegisterActive model)
        {
            IActionResult _result = BadRequest();

            CancellationTokenSource source = new CancellationTokenSource();

            var tasks = Task.Run(async delegate
            {

                ModelResponseMessageRegisterActive e = await _IRegisterUserService.AddRegisterActiveAsync(model);

                if (e.Status == true)
                {
                    _result = Ok(e);

                    string serverip = Encoding.UTF8.GetString(Convert.FromBase64String(_EnvironmentConfig.Server));

                    string register_id = Encoding.UTF8.GetString(Convert.FromBase64String(model.registerid));

                    string linkactive = $"{serverip}/{"efilling/log_in"}";

                    string mail_body = "<h3>คุณได้ลงทะเบียนเสร็จสิ้นตามขั้นตอนแล้ว</h3>" + Environment.NewLine +
                                       "<h2>อ้างอิงหมายเลข " + register_id + "</h2>" + Environment.NewLine +
                                       "<h4>ทั้งนี้คุณสามารถเข้าใช้งานระบบ ขอให้ท่านสนุกกับการใช้งาน! <a href='" + linkactive + "'>คลิ้กเพื่อเข้าสู่ระบบ</a>.</h4>";

                    await _EmailHelper.SentGmail(model.email, "eFilling : แจ้งผลการยืนยันลงทะเบียน", mail_body, "");
                }
                return _result;
            });
            source.Cancel();
            try
            {
                tasks.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }
            if (tasks.Status == TaskStatus.RanToCompletion) source.Dispose();

            return _result;

        }

        [HttpGet("GetPermissionPage/{RegisterId}/{PageCode}")]
        public async Task<IActionResult> GetPermissionPage(string RegisterId, string PageCode)
        {
            ModelPermissionPage e = await _IRegisterUserService.GetPermissionPageAsync(RegisterId, PageCode);

            if (e != null) return Ok(e);
            return BadRequest();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ModelResetPassword model)
        {
            ModelResponseMessageUpdateUserRegister e = await _IRegisterUserService.ResetPasswordAsync(model);

            return Ok(e);

        }

    }
}