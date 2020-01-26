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
using Newtonsoft.Json;
using System.Globalization;
using static THD.Core.Api.Helpers.ServerDirectorys;
using THD.Core.Api.Models.Config;
using THD.Core.Api.Models.ReportModels;

namespace THD.Core.Api.Repository.DataHandler
{
    public class DocMenuA1Repository : IDocMenuA1Repository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly IDropdownListRepository _IDropdownListRepository;
        private readonly IRegisterUserRepository _IRegisterUserRepository;
        private readonly IDocMenuReportRepository _IDocMenuReportRepository;
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        public DocMenuA1Repository(
            IConfiguration configuration,
            IDropdownListRepository DropdownListRepository,
            IRegisterUserRepository RegisterUserRepository,
            IDocMenuReportRepository DocMenuReportRepository,
            IEnvironmentConfig EnvironmentConfig)
        {
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));

            _IDropdownListRepository = DropdownListRepository;
            _IRegisterUserRepository = RegisterUserRepository;
            _IDocMenuReportRepository = DocMenuReportRepository;
            _IEnvironmentConfig = EnvironmentConfig;
        }

        #region Menu A1
        public async Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataAsync(string userid, string username)
        {
            string user_id = Encoding.UTF8.GetString(Convert.FromBase64String(userid));

            ModelMenuA1_InterfaceData resp = new ModelMenuA1_InterfaceData();

            resp.ListCommittees = new List<ModelSelectOption>();
            //resp.ListCommittees = await GetAllRegisterUserByCharacterAsync();
            ModelSelectOption user_login = new ModelSelectOption();
            user_login.value = userid;
            user_login.label = username + " (เช้าสู่ระบบ)";
            resp.ListCommittees.Add(user_login);
            resp.defaultusername = user_login.label;
            resp.defaultuserid = userid;

            ModelRegisterActive user_info = new ModelRegisterActive();
            user_info = await _IRegisterUserRepository.GetFullRegisterUserByIdAsync(user_id);

            if (user_info != null)
            {
                resp.facultyname = user_info.facultyname;
                resp.workphone = user_info.workphone;
                resp.mobile = user_info.mobile;
                resp.fax = user_info.fax;
                resp.email = user_info.email;
            }

            resp.ListMembers = new List<ModelSelectOption>();
            resp.ListMembers = await _IDropdownListRepository.GetAllRegisterUserByCharacterAsync(string.Empty);

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(userid, "M003");

            return resp;
        }

        public async Task<IList<ModelSelectOption>> GetAllRegisterUserByCharacterAsync()
        {

            string sql = "SELECT register_id, first_name, full_name FROM RegisterUser WHERE IsActive='1' AND Character IN ('1','2') ORDER BY full_name ASC";

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
                            item.value = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["register_id"].ToString()));
                            item.label = reader["first_name"].ToString() + " " + reader["full_name"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelResponseMessage> AddDocMenuA1Async(ModelMenuA1 model)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            ModelResponseMessage resp = new ModelResponseMessage();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_a1", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_date", SqlDbType.DateTime).Value = model.docdate.ToString("yyyy-MM-dd");
                        cmd.Parameters.Add("@doc_number", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.docnumber);
                        cmd.Parameters.Add("@project_consultant", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectconsultant);
                        cmd.Parameters.Add("@project_type", SqlDbType.VarChar, 2).Value = model.projecttype;
                        cmd.Parameters.Add("@project_head", SqlDbType.VarChar, 50).Value = model.projecthead;
                        cmd.Parameters.Add("@faculty_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.facultyname);
                        cmd.Parameters.Add("@work_phone", SqlDbType.VarChar, 20).Value = ParseDataHelper.ConvertDBNull(model.workphone);
                        cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 10).Value = ParseDataHelper.ConvertDBNull(model.mobile);
                        cmd.Parameters.Add("@fax", SqlDbType.VarChar, 20).Value = ParseDataHelper.ConvertDBNull(model.fax);
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.email);
                        cmd.Parameters.Add("@project_name_thai", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnamethai);
                        cmd.Parameters.Add("@project_name_eng", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnameeng);
                        cmd.Parameters.Add("@budget", SqlDbType.Decimal).Value = model.budget;
                        cmd.Parameters.Add("@money_supply", SqlDbType.VarChar, 200).Value = model.moneysupply;
                        cmd.Parameters.Add("@laboratory_used", SqlDbType.VarChar, 2).Value = model.laboratoryused;
                        cmd.Parameters.Add("@file1name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file1name);
                        cmd.Parameters.Add("@file2name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file2name);
                        cmd.Parameters.Add("@file3name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file3name);
                        cmd.Parameters.Add("@file4name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file4name);
                        cmd.Parameters.Add("@file5name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file5name);
                        cmd.Parameters.Add("@according_type_method", SqlDbType.VarChar, 2).Value = model.accordingtypemethod;
                        cmd.Parameters.Add("@project_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectother);
                        cmd.Parameters.Add("@project_according_type_method", SqlDbType.VarChar, 2).Value = model.projectaccordingtypemethod;
                        cmd.Parameters.Add("@reach_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.reachother);
                        cmd.Parameters.Add("@risk_group_1", SqlDbType.Bit).Value = model.riskgroup1;
                        cmd.Parameters.Add("@risk_group_1_1", SqlDbType.Bit).Value = model.riskgroup11;
                        cmd.Parameters.Add("@risk_group_1_2", SqlDbType.Bit).Value = model.riskgroup12;
                        cmd.Parameters.Add("@risk_group_1_3", SqlDbType.Bit).Value = model.riskgroup13;
                        cmd.Parameters.Add("@risk_group_1_4", SqlDbType.Bit).Value = model.riskgroup14;
                        cmd.Parameters.Add("@risk_group_1_5", SqlDbType.Bit).Value = model.riskgroup15;
                        cmd.Parameters.Add("@risk_group_1_5_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.riskgroup15other);
                        cmd.Parameters.Add("@risk_group_2", SqlDbType.Bit).Value = model.riskgroup2;
                        cmd.Parameters.Add("@risk_group_2_1", SqlDbType.Bit).Value = model.riskgroup21;
                        cmd.Parameters.Add("@risk_group_2_2", SqlDbType.Bit).Value = model.riskgroup22;
                        cmd.Parameters.Add("@risk_group_2_3", SqlDbType.Bit).Value = model.riskgroup23;
                        cmd.Parameters.Add("@risk_group_2_4", SqlDbType.Bit).Value = model.riskgroup24;
                        cmd.Parameters.Add("@risk_group_2_5", SqlDbType.Bit).Value = model.riskgroup25;
                        cmd.Parameters.Add("@risk_group_3", SqlDbType.Bit).Value = model.riskgroup3;
                        cmd.Parameters.Add("@risk_group_3_1", SqlDbType.Bit).Value = model.riskgroup31;
                        cmd.Parameters.Add("@risk_group_3_2", SqlDbType.Bit).Value = model.riskgroup32;
                        cmd.Parameters.Add("@risk_group_3_3", SqlDbType.Bit).Value = model.riskgroup33;
                        cmd.Parameters.Add("@risk_group_3_4", SqlDbType.Bit).Value = model.riskgroup34;
                        cmd.Parameters.Add("@risk_group_3_5", SqlDbType.Bit).Value = model.riskgroup35;
                        cmd.Parameters.Add("@risk_group_4", SqlDbType.Bit).Value = model.riskgroup4;
                        cmd.Parameters.Add("@risk_group_4_1", SqlDbType.Bit).Value = model.riskgroup41;
                        cmd.Parameters.Add("@risk_group_4_2", SqlDbType.Bit).Value = model.riskgroup42;
                        cmd.Parameters.Add("@risk_group_4_3", SqlDbType.Bit).Value = model.riskgroup43;
                        cmd.Parameters.Add("@risk_group_4_4", SqlDbType.Bit).Value = model.riskgroup44;
                        cmd.Parameters.Add("@risk_group_4_5", SqlDbType.Bit).Value = model.riskgroup45;
                        cmd.Parameters.Add("@member_project_1", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member1json);
                        cmd.Parameters.Add("@member_project_2", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member2json);
                        cmd.Parameters.Add("@member_project_3", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member3json);
                        cmd.Parameters.Add("@member_project_4", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member4json);
                        cmd.Parameters.Add("@member_project_5", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member5json);
                        cmd.Parameters.Add("@lab_other_name", SqlDbType.NVarChar).Value = ParseDataHelper.ConvertDBNull(model.labothername);

                        cmd.Parameters.Add("@create_by", SqlDbType.VarChar, 50).Value = Encoding.UTF8.GetString(Convert.FromBase64String(model.createby));

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

                            model_rpt_1_file rpt = await _IDocMenuReportRepository.GetReportR1_2Async((int)cmd.Parameters["@rDocId"].Value);

                            resp.filename1and2 = rpt.filename1_2;
                            resp.filebase1and264 = rpt.filebase1_2_64;
                            resp.filename16 = rpt.filename16;
                            resp.filebase1664 = rpt.filebase16_64;

                        }
                        else resp.Message = (string)cmd.Parameters["@rMessage"].Value;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;

        }

        #endregion

        #region Menu A1 Edit
        public async Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataEditAsync(int doc_id, string userid, string username)
        {
            string user_id = Encoding.UTF8.GetString(Convert.FromBase64String(userid));

            ModelMenuA1_InterfaceData resp = new ModelMenuA1_InterfaceData();

            resp.ListCommittees = new List<ModelSelectOption>();
            //resp.ListCommittees = await GetAllRegisterUserByCharacterAsync();
            ModelSelectOption user_login = new ModelSelectOption();
            user_login.value = userid;
            user_login.label = username + " (เช้าสู่ระบบ)";
            resp.ListCommittees.Add(user_login);
            resp.defaultusername = user_login.label;
            resp.defaultuserid = userid;

            ModelRegisterActive user_info = new ModelRegisterActive();
            user_info = await _IRegisterUserRepository.GetFullRegisterUserByIdAsync(user_id);

            if (user_info != null)
            {
                resp.facultyname = user_info.facultyname;
                resp.workphone = user_info.workphone;
                resp.mobile = user_info.mobile;
                resp.fax = user_info.fax;
                resp.email = user_info.email;
            }

            resp.ListMembers = new List<ModelSelectOption>();
            resp.ListMembers = await _IDropdownListRepository.GetAllRegisterUserByCharacterAsync(string.Empty);

            resp.editdata = new ModelMenuA1();
            resp.editdata = await GetMenuA1DataEditAsync(doc_id);

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(userid, "M003");

            return resp;
        }

        private async Task<ModelMenuA1> GetMenuA1DataEditAsync(int doc_id)
        {
            string sql = "SELECT A1.*,Users.full_name AS project_head_name,MST1.name_thai AS project_type_name, " +
                        "MST2.name_thai AS according_type_method_name, " +
                        "MST3.name_thai AS project_according_type_method_name, " +
                        "MST4.name_thai AS laboratory_used_name, B1.project_key_number " +
                        "FROM [dbo].[Doc_MenuA1] A1 " +
                        "LEFT OUTER JOIN [dbo].[Doc_MenuB1] B1 ON A1.doc_id = B1.project_id " +
                        "LEFT OUTER JOIN [dbo].[RegisterUser] Users ON A1.project_head = Users.register_id " +
                        "LEFT OUTER JOIN [dbo].[MST_ProjectType] MST1 ON A1.project_type = MST1.id " +
                        "LEFT OUTER JOIN [dbo].[MST_AccordingTypeMethod] MST2 ON A1.according_type_method = MST2.id " +
                        "LEFT OUTER JOIN [dbo].[MST_ProjectcAccordingTypeMethod] MST3 ON A1.project_according_type_method = MST3.id " +
                        "LEFT OUTER JOIN [dbo].[MST_Laboratory] MST4 ON A1.laboratory_used = MST4.id " +
                        "WHERE A1.doc_id='" + doc_id + "'";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuA1 e = new ModelMenuA1();
                        while (await reader.ReadAsync())
                        {
                            e.docid = reader["doc_id"].ToString();
                            e.docdate = Convert.ToDateTime(reader["doc_date"]);
                            e.projecttype = reader["project_type"].ToString();
                            e.projecttypename = reader["project_type_name"].ToString();
                            e.projecthead = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader["project_head"].ToString())); ;
                            e.projectheadname = reader["project_head_name"].ToString();
                            e.facultyname = reader["faculty_name"].ToString();
                            e.workphone = reader["work_phone"].ToString();
                            e.mobile = reader["mobile"].ToString();
                            e.fax = reader["fax"].ToString();
                            e.email = reader["email"].ToString();
                            e.projectnamethai = reader["project_name_thai"].ToString();
                            e.projectnameeng = reader["project_name_eng"].ToString();
                            e.budget = reader["budget"].ToString();
                            e.moneysupply = reader["money_supply"].ToString();
                            e.laboratoryused = reader["laboratory_used"].ToString();
                            e.laboratoryusedname = reader["laboratory_used_name"].ToString();
                            e.file1name = reader["file1name"].ToString();
                            e.file2name = reader["file2name"].ToString();
                            e.file3name = reader["file3name"].ToString();
                            e.file4name = reader["file4name"].ToString();
                            e.file5name = reader["file5name"].ToString();
                            e.accordingtypemethod = reader["according_type_method"].ToString();
                            e.accordingtypemethodname = reader["according_type_method_name"].ToString();
                            e.projectother = reader["project_other"].ToString();
                            e.projectaccordingtypemethod = reader["project_according_type_method"].ToString();
                            e.projectaccordingtypemethodname = reader["project_according_type_method_name"].ToString();
                            e.reachother = reader["reach_other"].ToString();
                            e.riskgroup1 = Convert.ToBoolean(reader["risk_group_1"]);
                            e.riskgroup11 = Convert.ToBoolean(reader["risk_group_1_1"]);
                            e.riskgroup12 = Convert.ToBoolean(reader["risk_group_1_2"]);
                            e.riskgroup13 = Convert.ToBoolean(reader["risk_group_1_3"]);
                            e.riskgroup14 = Convert.ToBoolean(reader["risk_group_1_4"]);
                            e.riskgroup15 = Convert.ToBoolean(reader["risk_group_1_5"]);
                            e.riskgroup15other = reader["risk_group_1_5_other"].ToString();
                            e.riskgroup2 = Convert.ToBoolean(reader["risk_group_2"]);
                            e.riskgroup21 = Convert.ToBoolean(reader["risk_group_2_1"]);
                            e.riskgroup22 = Convert.ToBoolean(reader["risk_group_2_2"]);
                            e.riskgroup23 = Convert.ToBoolean(reader["risk_group_2_3"]);
                            e.riskgroup24 = Convert.ToBoolean(reader["risk_group_2_4"]);
                            e.riskgroup25 = Convert.ToBoolean(reader["risk_group_2_5"]);
                            e.riskgroup3 = Convert.ToBoolean(reader["risk_group_3"]);
                            e.riskgroup31 = Convert.ToBoolean(reader["risk_group_3_1"]);
                            e.riskgroup32 = Convert.ToBoolean(reader["risk_group_3_2"]);
                            e.riskgroup33 = Convert.ToBoolean(reader["risk_group_3_3"]);
                            e.riskgroup34 = Convert.ToBoolean(reader["risk_group_3_4"]);
                            e.riskgroup35 = Convert.ToBoolean(reader["risk_group_3_5"]);
                            e.riskgroup4 = Convert.ToBoolean(reader["risk_group_4"]);
                            e.riskgroup41 = Convert.ToBoolean(reader["risk_group_4_1"]);
                            e.riskgroup42 = Convert.ToBoolean(reader["risk_group_4_2"]);
                            e.riskgroup43 = Convert.ToBoolean(reader["risk_group_4_3"]);
                            e.riskgroup44 = Convert.ToBoolean(reader["risk_group_4_4"]);
                            e.riskgroup45 = Convert.ToBoolean(reader["risk_group_4_5"]);
                            e.labothername = reader["lab_other_name"].ToString();
                            e.projeckeynumber = reader["project_key_number"].ToString();
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelMenuA1_FileDownload> GetA1DownloadFileByIdAsync(int DocId, int Id)
        {

            string sql = "SELECT TOP(1) file1name,file2name,file3name,file4name,file5name FROM Doc_MenuA1 WHERE doc_id='" + DocId + "' ";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuA1_FileDownload e = new ModelMenuA1_FileDownload();
                        while (await reader.ReadAsync())
                        {
                            if (Id == 1)
                            {
                                e.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, reader["file1name"].ToString());
                                e.filename = "แบบเสนอเพื่อขอรับการพิจารณารับรองด้านความปลอดภัย";
                            }
                            if (Id == 2)
                            {
                                e.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, reader["file2name"].ToString());
                                e.filename = "โครงการวิจัยฉบับสมบูรณ์";
                            }
                            if (Id == 3)
                            {
                                e.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, reader["file3name"].ToString());
                                e.filename = "เอกสารชี้แจงรายละเอียดของเชื้อที่ใช้/แบบฟอร์มข้อตกลงการใช้ตัวอย่างชีวภาพ";
                            }
                            if (Id == 4)
                            {
                                e.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, reader["file4name"].ToString());
                                e.filename = "หนังสือรับรองและอนุมัติให้ใช้สถานะที่";
                            }
                            if (Id == 5)
                            {
                                e.filebase64 = ServerDirectorys.ReadFileToBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, reader["file5name"].ToString());
                                e.filename = "อื่นๆ";
                            }
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelResponseMessage> UpdateDocMenuA1EditAsync(ModelMenuA1 model)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            ModelResponseMessage resp = new ModelResponseMessage();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_a1_edit", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.Int).Value = model.docid;
                        cmd.Parameters.Add("@doc_date", SqlDbType.DateTime).Value = model.docdate.ToString("yyyy-MM-dd");
                        cmd.Parameters.Add("@project_type", SqlDbType.VarChar, 2).Value = model.projecttype;
                        cmd.Parameters.Add("@project_head", SqlDbType.VarChar, 50).Value = model.projecthead;
                        cmd.Parameters.Add("@faculty_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.facultyname);
                        cmd.Parameters.Add("@work_phone", SqlDbType.VarChar, 20).Value = ParseDataHelper.ConvertDBNull(model.workphone);
                        cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 10).Value = ParseDataHelper.ConvertDBNull(model.mobile);
                        cmd.Parameters.Add("@fax", SqlDbType.VarChar, 20).Value = ParseDataHelper.ConvertDBNull(model.fax);
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.email);
                        cmd.Parameters.Add("@project_name_thai", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnamethai);
                        cmd.Parameters.Add("@project_name_eng", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectnameeng);
                        cmd.Parameters.Add("@budget", SqlDbType.Decimal).Value = model.budget;
                        cmd.Parameters.Add("@money_supply", SqlDbType.VarChar, 200).Value = model.moneysupply;
                        cmd.Parameters.Add("@laboratory_used", SqlDbType.VarChar, 2).Value = model.laboratoryused;
                        cmd.Parameters.Add("@file1name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file1name);
                        cmd.Parameters.Add("@file2name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file2name);
                        cmd.Parameters.Add("@file3name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file3name);
                        cmd.Parameters.Add("@file4name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file4name);
                        cmd.Parameters.Add("@file5name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file5name);
                        cmd.Parameters.Add("@according_type_method", SqlDbType.VarChar, 2).Value = model.accordingtypemethod;
                        cmd.Parameters.Add("@project_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.projectother);
                        cmd.Parameters.Add("@project_according_type_method", SqlDbType.VarChar, 2).Value = model.projectaccordingtypemethod;
                        cmd.Parameters.Add("@reach_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.reachother);
                        cmd.Parameters.Add("@risk_group_1", SqlDbType.Bit).Value = model.riskgroup1;
                        cmd.Parameters.Add("@risk_group_1_1", SqlDbType.Bit).Value = model.riskgroup11;
                        cmd.Parameters.Add("@risk_group_1_2", SqlDbType.Bit).Value = model.riskgroup12;
                        cmd.Parameters.Add("@risk_group_1_3", SqlDbType.Bit).Value = model.riskgroup13;
                        cmd.Parameters.Add("@risk_group_1_4", SqlDbType.Bit).Value = model.riskgroup14;
                        cmd.Parameters.Add("@risk_group_1_5", SqlDbType.Bit).Value = model.riskgroup15;
                        cmd.Parameters.Add("@risk_group_1_5_other", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.riskgroup15other);
                        cmd.Parameters.Add("@risk_group_2", SqlDbType.Bit).Value = model.riskgroup2;
                        cmd.Parameters.Add("@risk_group_2_1", SqlDbType.Bit).Value = model.riskgroup21;
                        cmd.Parameters.Add("@risk_group_2_2", SqlDbType.Bit).Value = model.riskgroup22;
                        cmd.Parameters.Add("@risk_group_2_3", SqlDbType.Bit).Value = model.riskgroup23;
                        cmd.Parameters.Add("@risk_group_2_4", SqlDbType.Bit).Value = model.riskgroup24;
                        cmd.Parameters.Add("@risk_group_2_5", SqlDbType.Bit).Value = model.riskgroup25;
                        cmd.Parameters.Add("@risk_group_3", SqlDbType.Bit).Value = model.riskgroup3;
                        cmd.Parameters.Add("@risk_group_3_1", SqlDbType.Bit).Value = model.riskgroup31;
                        cmd.Parameters.Add("@risk_group_3_2", SqlDbType.Bit).Value = model.riskgroup32;
                        cmd.Parameters.Add("@risk_group_3_3", SqlDbType.Bit).Value = model.riskgroup33;
                        cmd.Parameters.Add("@risk_group_3_4", SqlDbType.Bit).Value = model.riskgroup34;
                        cmd.Parameters.Add("@risk_group_3_5", SqlDbType.Bit).Value = model.riskgroup35;
                        cmd.Parameters.Add("@risk_group_4", SqlDbType.Bit).Value = model.riskgroup4;
                        cmd.Parameters.Add("@risk_group_4_1", SqlDbType.Bit).Value = model.riskgroup41;
                        cmd.Parameters.Add("@risk_group_4_2", SqlDbType.Bit).Value = model.riskgroup42;
                        cmd.Parameters.Add("@risk_group_4_3", SqlDbType.Bit).Value = model.riskgroup43;
                        cmd.Parameters.Add("@risk_group_4_4", SqlDbType.Bit).Value = model.riskgroup44;
                        cmd.Parameters.Add("@risk_group_4_5", SqlDbType.Bit).Value = model.riskgroup45;
                        cmd.Parameters.Add("@member_project_1", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member1json);
                        cmd.Parameters.Add("@member_project_2", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member2json);
                        cmd.Parameters.Add("@member_project_3", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member3json);
                        cmd.Parameters.Add("@member_project_4", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member4json);
                        cmd.Parameters.Add("@member_project_5", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.member5json);
                        cmd.Parameters.Add("@lab_other_name", SqlDbType.NVarChar).Value = ParseDataHelper.ConvertDBNull(model.labothername);

                        SqlParameter rStatus = cmd.Parameters.Add("@rStatus", SqlDbType.Int);
                        rStatus.Direction = ParameterDirection.Output;
                        SqlParameter rMessage = cmd.Parameters.Add("@rMessage", SqlDbType.NVarChar, 500);
                        rMessage.Direction = ParameterDirection.Output;

                        await cmd.ExecuteNonQueryAsync();

                        if ((int)cmd.Parameters["@rStatus"].Value > 0)
                        {
                            resp.Status = true;
                        }
                        else resp.Message = (string)cmd.Parameters["@rMessage"].Value;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

        #endregion

    }
}