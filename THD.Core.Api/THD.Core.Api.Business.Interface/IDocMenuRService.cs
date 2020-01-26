using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuRService
    {
        // Report R1
        Task<ModelMenuR1_InterfaceData> MenuR1InterfaceDataAsync(string RegisterId);
        Task<IList<ModelMenuR1Data>> GetAllReportHistoryDataR1Async(ModelMenuR1_InterfaceData search);
    }
}
