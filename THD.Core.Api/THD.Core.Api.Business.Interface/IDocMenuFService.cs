using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuFService
    {
        #region Menu F1
        Task<ModelMenuF1_InterfaceData> MenuF1InterfaceDataAsync(string RegisterId);
        Task<IList<ModelMenuF1Report>> GetAllReportDataF1Async(ModelMenuF1_InterfaceData search_data);
        #endregion

        #region Menu F1 Edit
        Task<ModelMenuF1Edit_InterfaceData> MenuF1EditInterfaceDataAsync(string RegisterId, string UserId);
        Task<ModelResponseMessageUpdateUserRegister> UpdateUserRegisterAsync(ModelRegisterEdit model);
        #endregion

        #region Menu F2
        Task<ModelMenuF2_InterfaceData> MenuF2InterfaceDataAsync(string RegisterId, string UserGroup);
        Task<IList<ModelMenuF2Report>> GetAllReportDataF2Async(ModelMenuF2_InterfaceData search_data);
        Task<ModelMenuF2Edit> GetUserEditPermissionF2Async(string UserGroup, string MenuCode);
        Task<ModelResponseMessageUpdateUserRegister> UpdatePermissionGroupAsync(ModelMenuF2Edit model);

        #endregion

        #region Menu F Account User

        Task<ModelMenuFAccount_InterfaceData> MenuAccountInterfaceDataAsync(string RegisterId);

        Task<ModelResponseMessageUpdateUserRegister> UpdateUserAccountAsync(ModelUpdateAccountUser model);

        #endregion

    }
}
