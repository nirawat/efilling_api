using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IDocMenuD1Repository
    {
        Task<ModelMenuD1_InterfaceData> MenuD1InterfaceDataAsync(string RegisterId);
        Task<ModelMenuD1ProjectNumberData> GetProjectNumberWithDataD1Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuD1Async(ModelMenuD1 model);
    }
}
