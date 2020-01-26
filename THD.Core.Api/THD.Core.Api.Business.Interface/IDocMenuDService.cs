﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using THD.Core.Api.Models;

namespace THD.Core.Api.Business.Interface
{
    public interface IDocMenuDService
    {
        Task<ModelMenuD1_InterfaceData> MenuD1InterfaceDataAsync(string RegisterId);
        Task<ModelMenuD1ProjectNumberData> GetProjectNumberWithDataD1Async(string project_number);
        Task<ModelResponseMessage> AddDocMenuD1Async(ModelMenuD1 model);


        Task<ModelMenuD2_InterfaceData> MenuD2InterfaceDataAsync(string RegisterId);
        Task<ModelMenuD2ProjectNumberData> GetProjectNumberWithDataD2Async(string project_number);
        Task<ModelMenuD2_FileDownload> GetAllDownloadFileByFileNameAsync(string filename);
        Task<ModelResponseMessage> AddDocMenuD2Async(ModelMenuD2 model);

    }
}