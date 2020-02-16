using System;
using System.Collections.Generic;
using THD.Core.Api.Business.Interface;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using THD.Core.Api.Models;
using THD.Core.Api.Repository.Interface;
using System.Text;
using THD.Core.Api.Helpers;
using System.IO;
using static THD.Core.Api.Helpers.ServerDirectorys;
using THD.Core.Api.Models.Config;
using System.Globalization;

namespace THD.Core.Api.Business
{
    public class DocMenuFService : IDocMenuFService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuF1Repository _IDocMenuF1Repository;
        private readonly IDocMenuF2Repository _IDocMenuF2Repository;

        public DocMenuFService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuF1Repository DocMenuF1Repository,
            IDocMenuF2Repository DocMenuF2Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuF1Repository = DocMenuF1Repository;
            _IDocMenuF2Repository = DocMenuF2Repository;
        }

        #region Menu F1

        public async Task<ModelMenuF1_InterfaceData> MenuF1InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuF1Repository.MenuF1InterfaceDataAsync(RegisterId);
        }

        public async Task<IList<ModelMenuF1Report>> GetAllReportDataF1Async(ModelMenuF1_InterfaceData search_data)
        {
            return await _IDocMenuF1Repository.GetAllReportDataF1Async(search_data);
        }
        #endregion

        #region Menu F1 Edit

        public async Task<ModelMenuF1Edit_InterfaceData> MenuF1EditInterfaceDataAsync(string RegisterId, string UserId)
        {
            return await _IDocMenuF1Repository.MenuF1EditInterfaceDataAsync(RegisterId, UserId);
        }

        public async Task<ModelResponseMessageUpdateUserRegister> UpdateUserRegisterAsync(ModelRegisterEdit model)
        {
            return await _IDocMenuF1Repository.UpdateUserRegisterAsync(model);
        }
        #endregion

        #region Menu F2

        public async Task<ModelMenuF2_InterfaceData> MenuF2InterfaceDataAsync(string RegisterId, string UserGroup)
        {
            return await _IDocMenuF2Repository.MenuF2InterfaceDataAsync(RegisterId, UserGroup);
        }

        public async Task<IList<ModelMenuF2Report>> GetAllReportDataF2Async(ModelMenuF2_InterfaceData search_data)
        {
            return await _IDocMenuF2Repository.GetAllReportDataF2Async(search_data);
        }

        public async Task<ModelMenuF2Edit> GetUserEditPermissionF2Async(string UserGroup, string MenuCode)
        {
            return await _IDocMenuF2Repository.GetUserEditPermissionF2Async(UserGroup, MenuCode);
        }

        public async Task<ModelResponseMessageUpdateUserRegister> UpdatePermissionGroupAsync(ModelMenuF2Edit model)
        {
            return await _IDocMenuF2Repository.UpdatePermissionGroupAsync(model);
        }

        #endregion

        #region Menu F Account User

        public async Task<ModelMenuFAccount_InterfaceData> MenuAccountInterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuF1Repository.MenuAccountInterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseMessageUpdateUserRegister> UpdateUserAccountAsync(ModelUpdateAccountUser model)
        {
            return await _IDocMenuF1Repository.UpdateUserAccountAsync(model);
        }

        #endregion
    }

}
