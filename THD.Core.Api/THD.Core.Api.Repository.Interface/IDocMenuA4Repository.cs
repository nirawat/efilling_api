using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IDocMenuA4Repository
    {
        Task<ModelMenuA4_InterfaceData> MenuA4InterfaceDataAsync(string RegisterId);

        Task<ModelMenuA4ProjectNumberData> GetProjectNumberWithDataA4Async(string project_number);

        Task<ModelResponseMessage> AddDocMenuA4Async(ModelMenuA4 model);
    }
}
