﻿using System;
using System.Collections.Generic;
using THD.Core.Api.Repository.DataContext;
using THD.Core.Api.Repository.Interface;
using Microsoft.Extensions.Configuration;
using THD.Core.Api.Entities.Tables;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THD.Core.Api.Helpers;
using System.Data;
using THD.Core.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using THD.Core.Api.Models.ReportModels;

namespace THD.Core.Api.Repository.DataHandler
{
    public class DocMenuA5Repository : IDocMenuA5Repository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly IDropdownListRepository _IDropdownListRepository;
        private readonly IRegisterUserRepository _IRegisterUserRepository;
        private readonly IDocMenuReportRepository _IDocMenuReportRepository;
        public DocMenuA5Repository(
            IConfiguration configuration,
            IDropdownListRepository DropdownListRepository,
            IRegisterUserRepository IRegisterUserRepository,
            IDocMenuReportRepository DocMenuReportRepository
            )
        {
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));
            _IDropdownListRepository = DropdownListRepository;
            _IRegisterUserRepository = IRegisterUserRepository;
            _IDocMenuReportRepository = DocMenuReportRepository;
        }

        public async Task<ModelMenuA5_InterfaceData> MenuA5InterfaceDataAsync(string RegisterId)
        {
            ModelMenuA5_InterfaceData resp = new ModelMenuA5_InterfaceData();

            resp.ListProjectNumber = new List<ModelSelectOption>();

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M007");

            if (resp.UserPermission != null && resp.UserPermission.alldata == true)
            {
                resp.ListProjectNumber = await GetAllProjectForA5Async("", "A3,A5,A6,A7");
            }
            else
            {
                resp.ListProjectNumber = await GetAllProjectForA5Async(RegisterId, "A3,A5,A6,A7");
            }

            return resp;
        }

        public async Task<IList<ModelSelectOption>> GetAllProjectForA5Async(string AssignerCode, string DocProcess)
        {

            string sql = "SELECT * FROM [dbo].[Doc_Process] " +
                        "WHERE project_type='PROJECT' AND doc_process_to='" + DocProcess + "' ";

            if (!string.IsNullOrEmpty(AssignerCode))
            {
                sql += " AND project_by='" + Encoding.UTF8.GetString(Convert.FromBase64String(AssignerCode)) + "' ";
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelSelectOption> e = new List<ModelSelectOption>();
                        while (await reader.ReadAsync())
                        {
                            ModelSelectOption item = new ModelSelectOption();
                            item.value = reader["project_number"].ToString();
                            item.label = reader["project_number"].ToString() + " : " + reader["project_name_thai"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelMenuA5ProjectNumberData> GetProjectNumberWithDataA5Async(string project_number)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_getdata_for_a5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@project_number", SqlDbType.VarChar, 50).Value = project_number;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            ModelMenuA5ProjectNumberData e = new ModelMenuA5ProjectNumberData();
                            while (await reader.ReadAsync())
                            {
                                e.projectname1 = reader[1].ToString();
                                e.projectname2 = reader[2].ToString();
                                e.projectheadname = reader[3].ToString();
                                e.facultyname = reader[4].ToString();
                                e.positionname = reader[5].ToString();
                                e.certificatetype = reader[6].ToString();
                                e.dateofapproval = Convert.ToDateTime(reader[7]).ToString("dd/MM/yyyy");
                            }
                            return e;
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }

        public async Task<ModelResponseMessage> AddDocMenuA5Async(ModelMenuA5 model)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            ModelResponseMessage resp = new ModelResponseMessage();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_doc_menu_a5", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@doc_date", SqlDbType.DateTime).Value = model.docdate.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@project_number", SqlDbType.VarChar, 20).Value = ParseDataHelper.ConvertDBNull(model.projectnumber);
                    cmd.Parameters.Add("@project_head_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectheadname);
                    cmd.Parameters.Add("@position_name_thai", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.positionnamethai);
                    cmd.Parameters.Add("@faculty_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.facultyname);
                    cmd.Parameters.Add("@project_name_thai", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnamethai);
                    cmd.Parameters.Add("@project_name_eng", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnameeng);
                    cmd.Parameters.Add("@accept_type_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.accepttypenamethai);
                    cmd.Parameters.Add("@conclusion_date", SqlDbType.DateTime).Value = Convert.ToDateTime(model.conclusiondate);
                    cmd.Parameters.Add("@file1name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file1name);

                    SqlParameter rStatus = cmd.Parameters.Add("@rStatus", SqlDbType.Int);
                    rStatus.Direction = ParameterDirection.Output;
                    SqlParameter rMessage = cmd.Parameters.Add("@rMessage", SqlDbType.NVarChar, 500);
                    rMessage.Direction = ParameterDirection.Output;
                    SqlParameter rDocId = cmd.Parameters.Add("@rDocId", SqlDbType.Int);
                    rDocId.Direction = ParameterDirection.Output;

                    await cmd.ExecuteNonQueryAsync();

                    if ((int)cmd.Parameters["@rStatus"].Value > 0)
                    {
                        resp.Status = true;

                        model_rpt_3_file rpt = await _IDocMenuReportRepository.GetReportR3Async((int)cmd.Parameters["@rDocId"].Value);

                        resp.filename = rpt.filename;
                        resp.filebase64 = rpt.filebase64;
                    }
                    else resp.Message = (string)cmd.Parameters["@rMessage"].Value;
                }
                conn.Close();
            }
            return resp;
        }


        #region "Edit"
        public async Task<ModelMenuA5_InterfaceData> MenuA5EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            ModelMenuA5_InterfaceData resp = new ModelMenuA5_InterfaceData();

            resp.editdata = new ModelMenuA5();
            resp.editdata = await GetMenuA5DataEditAsync(ProjectNumber);

            ModelSelectOption defaultProject = new ModelSelectOption();
            defaultProject.value = resp.editdata.projectnumber;
            defaultProject.label = resp.editdata.projectnamethai;
            resp.ListProjectNumber = new List<ModelSelectOption>();
            resp.ListProjectNumber.Add(defaultProject);

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(UserId, "M007");

            return resp;
        }

        private async Task<ModelMenuA5> GetMenuA5DataEditAsync(string ProjectNumber)
        {
            string sql = "SELECT TOP(1)* FROM Doc_MenuA5 " +
                         "WHERE project_number='" + ProjectNumber + "' ORDER BY doc_id DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuA5 e = new ModelMenuA5();
                        while (await reader.ReadAsync())
                        {
                            e.docid = reader["doc_id"].ToString();
                            e.projectnumber = reader["project_number"].ToString();
                            e.projectheadname = reader["project_head_name"].ToString();
                            e.positionnamethai = reader["position_name_thai"].ToString();
                            e.facultyname = reader["faculty_name"].ToString();
                            e.projectnamethai = reader["project_name_thai"].ToString();
                            e.projectnameeng = reader["project_name_eng"].ToString();
                            e.accepttypenamethai = reader["accept_type_name"].ToString();
                            e.conclusiondate = Convert.ToDateTime(reader["conclusion_date"]).ToString("dd/MM/yyyy");
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }
        #endregion

    }
}
