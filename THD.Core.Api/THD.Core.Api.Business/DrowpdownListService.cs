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
    public class DropdownListService : IDropdownListService
    {
        private readonly IDropdownListRepository _IDropdownListRepository;

        public DropdownListService(IDropdownListRepository DropdownListRepository)
        {
            _IDropdownListRepository = DropdownListRepository;
        }

        public async Task<IList<ModelSelectOption>> GetAllRegisterUserByCharacterAsync(string character)
        {
            return await _IDropdownListRepository.GetAllRegisterUserByCharacterAsync(character);
        }

    }
}
