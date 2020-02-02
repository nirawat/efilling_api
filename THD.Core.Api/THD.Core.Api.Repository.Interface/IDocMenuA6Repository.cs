using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IDocMenuA6Repository
    {
        Task<ModelMenuA6_InterfaceData> MenuA6InterfaceDataAsync(string RegisterId);
        Task<ModelMenuA6ProjectNumberData> GetProjectNumberWithDataA6Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuA6Async(ModelMenuA6 model);


        Task<ModelMenuA6_InterfaceData> MenuA6EditInterfaceDataAsync(string UserId, string ProjectNumber);
    }
}
