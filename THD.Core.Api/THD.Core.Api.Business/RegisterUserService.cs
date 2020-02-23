using System;
using System.Collections.Generic;
using THD.Core.Api.Business.Interface;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using THD.Core.Api.Models;
using THD.Core.Api.Repository.Interface;
using System.Text;
using THD.Core.Api.Helpers;

namespace THD.Core.Api.Business
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IRegisterUserRepository _IRegisterUserRepository;

        public RegisterUserService(IRegisterUserRepository RegisterUserRepository)
        {
            _IRegisterUserRepository = RegisterUserRepository;
        }

        public async Task<ModelRegisterActive_InterfaceData> ActiveUserAccountInterfaceAsync(string RegisterId)
        {
            return await _IRegisterUserRepository.ActiveUserAccountInterfaceAsync(RegisterId);
        }

        public async Task<ModelResponseMessageRegisterUser> AddRegisterUserAsync(ModelRegisterUser model)
        {
            ModelResponseMessageRegisterUser resp = new ModelResponseMessageRegisterUser();

            if (model.passw == model.confirmpassw)
            {
                EntityRegisterUser entity_model = new EntityRegisterUser();

                entity_model.userid = model.userid;
                entity_model.passw = GenerateToken.GetPassword(model.passw);
                entity_model.confirmpassw = GenerateToken.GetPassword(model.confirmpassw);
                entity_model.email = model.email;
                entity_model.register_date = DateTime.Now;
                entity_model.register_expire = DateTime.Now.AddDays(3);

                resp = await _IRegisterUserRepository.AddRegisterUserAsync(entity_model);
            }
            else resp.Message = "Password no match.";

            return resp;

        }
        public async Task<ModelResponseMessageRegisterActive> AddRegisterActiveAsync(ModelRegisterActive model)
        {
            ModelResponseMessageRegisterActive resp = new ModelResponseMessageRegisterActive();

            EntityRegisterUser entity_model = new EntityRegisterUser();

            entity_model.register_id = Encoding.UTF8.GetString(Convert.FromBase64String(model.registerid));
            entity_model.first_name_1 = model.firstname1;
            entity_model.first_name_2 = model.firstname2;
            entity_model.first_name = model.firstname;
            entity_model.full_name = model.fullname;
            entity_model.position = model.position;
            entity_model.work_phone = model.workphone;
            entity_model.faculty = model.faculty;
            entity_model.mobile = model.mobile;
            entity_model.fax = model.fax;
            entity_model.education = model.education;
            entity_model.character = model.character;
            entity_model.note1 = model.note1;
            entity_model.note2 = model.note2;
            entity_model.note3 = model.note3;
            entity_model.confirm_date = DateTime.Now;
            entity_model.member_expire = DateTime.Now.AddYears(10);

            resp = await _IRegisterUserRepository.AddRegisterActiveAsync(entity_model);

            return resp;

        }
        public async Task<ModelRegisterActive> GetRegisterUserActiveAsync(string RegisterId)
        {
            var register_user = await _IRegisterUserRepository.GetRegisterUserActiveAsync(RegisterId);

            if (register_user != null && !string.IsNullOrEmpty(register_user.register_id))
            {
                ModelRegisterActive model = new ModelRegisterActive();

                model.registerid = register_user.register_id;
                model.userid = register_user.userid;
                model.passw = register_user.passw;
                model.confirmpassw = register_user.confirmpassw;
                model.email = register_user.email;
                model.registerdate = register_user.register_date;
                model.registerexpire = register_user.register_expire;
                model.firstname1 = register_user.first_name_1;
                model.firstname2 = register_user.first_name_2;
                model.firstname = register_user.first_name;
                model.fullname = register_user.full_name;
                model.faculty = register_user.faculty;
                model.position = register_user.position;
                model.workphone = register_user.work_phone;
                model.mobile = register_user.mobile;
                model.fax = register_user.fax;
                model.education = register_user.education;
                model.character = register_user.character;
                model.note1 = register_user.mobile;
                model.note2 = register_user.note2;
                model.note3 = register_user.note3;
                model.isactive = register_user.isactive;

                return model;
            }
            return null;
        }
        public async Task<ModelRegisterActive> GetFullRegisterUserByIdAsync(string RegisterId)
        {
            return await _IRegisterUserRepository.GetFullRegisterUserByIdAsync(RegisterId);
        }
        public async Task<ModelRegisterActive> GetRegisterUserInActiveAsync(string RegisterId)
        {
            var register_user = await _IRegisterUserRepository.GetRegisterUserInActiveAsync(RegisterId);

            if (register_user != null && !string.IsNullOrEmpty(register_user.register_id))
            {
                ModelRegisterActive model = new ModelRegisterActive();

                model.registerid = register_user.register_id;
                model.userid = register_user.userid;
                model.email = register_user.email;
                model.registerdate = register_user.register_date;
                model.registerexpire = register_user.register_expire;

                return model;
            }
            return null;
        }
        public async Task<ModelPermissionPage> GetPermissionPageAsync(string RegisterId, string PageCode)
        {
            return await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, PageCode);
        }

        public async Task<ModelResponseMessageUpdateUserRegister> ResetPasswordAsync(ModelResetPassword model)
        {
            ModelResponseMessageUpdateUserRegister resp = new ModelResponseMessageUpdateUserRegister();

            resp.Status = false;

            if (string.IsNullOrEmpty(model.oldpassw))
            {
                resp.Message = "กรุณาระบุรหัสผ่านเดิม";
            }
            else if (string.IsNullOrEmpty(model.newpassw))
            {
                resp.Message = "กรุณาระบุรหัสผ่านใหม่";
            }
            else if (string.IsNullOrEmpty(model.confirmpassw))
            {
                resp.Message = "กรุณายืนยันรหัสผ่าน";
            }
            else if (model.newpassw != model.confirmpassw)
            {
                resp.Message = "รหัสผ่านใหม่ไม่ตรงกัน";
            }
            else
            {
                resp = await _IRegisterUserRepository.ResetPasswordAsync(model);
            }

            return resp;

        }
    }
}
