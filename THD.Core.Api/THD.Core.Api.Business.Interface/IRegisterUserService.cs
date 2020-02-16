using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Models;
using System.Text;

namespace THD.Core.Api.Business.Interface
{
    public interface IRegisterUserService
    {
        Task<ModelResponseMessageRegisterUser> AddRegisterUserAsync(ModelRegisterUser model);
        Task<ModelResponseMessageRegisterActive> AddRegisterActiveAsync(ModelRegisterActive model);
        Task<ModelRegisterActive> GetRegisterUserActiveAsync(string RegisterId);
        Task<ModelRegisterActive> GetFullRegisterUserByIdAsync(string RegisterId);
        Task<ModelRegisterActive> GetRegisterUserInActiveAsync(string RegisterId);
        Task<ModelPermissionPage> GetPermissionPageAsync(string RegisterId, string PageCode);
        Task<ModelResponseMessageUpdateUserRegister> ResetPasswordAsync(ModelResetPassword model);
    }
}
