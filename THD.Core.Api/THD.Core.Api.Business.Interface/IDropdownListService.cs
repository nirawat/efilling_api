using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Models;
using System.Text;

namespace THD.Core.Api.Business.Interface
{
    public interface IDropdownListService
    {
        Task<IList<ModelSelectOption>> GetAllRegisterUserByCharacterAsync(string character);
    }
}
