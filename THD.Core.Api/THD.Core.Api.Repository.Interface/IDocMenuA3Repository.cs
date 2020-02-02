﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Repository.Interface
{
    public interface IDocMenuA3Repository
    {
        Task<ModelMenuA3_InterfaceData> MenuA3InterfaceDataAsync(string RegisterId);

        Task<ModelMenuA3ProjectNumberData> GetProjectNumberWithDataA3Async(string project_number);

        Task<ModelResponseMessage> AddDocMenuA3Async(ModelMenuA3 model);



        Task<ModelMenuA3_InterfaceData> MenuA3EditInterfaceDataAsync(string UserId, string ProjectNumber);
    }
}
