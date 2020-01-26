﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuEService
    {
        Task<ModelMenuE1_InterfaceData> MenuE1InterfaceDataAsync(string RegisterId, string Passw);
        Task<ModelResponseMessage> AddDocMenuE1Async(ModelMenuE1 model);
        Task<IList<ModelMenuE1Report>> GetAllReportDataE1Async(ModelMenuE1Report search_data);
    }
}
