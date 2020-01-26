using System;
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
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Native.Web;
using THD.Core.Api.Repository.ReportFiles;
using THD.Core.Api.Models.ReportModels;
using DevExpress.DataAccess.ObjectBinding;
using THD.Core.Api.Models.Config;

namespace THD.Core.Api.Repository.DataHandler
{
    public class DocMenuReportRepository : IDocMenuReportRepository
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly IDropdownListRepository _IDropdownListRepository;

        public DocMenuReportRepository(IEnvironmentConfig EnvironmentConfig, IConfiguration configuration, IDropdownListRepository DropdownListRepository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));
            _IDropdownListRepository = DropdownListRepository;
        }


        // Report 1-2-16 --------------------------------------------------------------------
        public async Task<model_rpt_1_file> GetReportR1_2Async(int doc_id)
        {
            model_rpt_1_file resp = new model_rpt_1_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                model_rpt_1_report rptData1_2 = new model_rpt_1_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_report_1_2", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData1_2.projecttype = reader["project_type"].ToString();
                                rptData1_2.Doc_head_2 = reader["doc_number"].ToString();
                                rptData1_2.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                                rptData1_2.Presenter_name = reader["project_head_name"].ToString();
                                rptData1_2.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData1_2.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData1_2.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData1_2.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData1_2.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData1_2.Job_Position = "";
                                rptData1_2.Faculty_name = reader["faculty_name"].ToString();
                                rptData1_2.Research_name_thai = reader["project_name_thai"].ToString();
                                rptData1_2.Research_name_eng = reader["project_name_eng"].ToString();
                                rptData1_2.Faculty_name = reader["faculty_name"].ToString();

                                rptData1_2.Advisor_fullname = reader["project_consultant"].ToString();
                                rptData1_2.HeadofResearch_fullname = reader["project_head_name"].ToString();
                                rptData1_2.co_research_fullname1 = reader["member_project_1"].ToString();
                                rptData1_2.co_research_fullname2 = reader["member_project_2"].ToString();

                            }
                        }
                        reader.Close();
                    }



                    //using (SqlCommand cmd = new SqlCommand("sp_report_16", conn))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;

                    //    cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                    //    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    //    if (reader.HasRows)
                    //    {
                    //        while (await reader.ReadAsync())
                    //        {
                    //            rptData.projecttype = reader["project_type"].ToString();
                    //            rptData.Doc_head_2 = reader["doc_number"].ToString();
                    //            rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                    //            rptData.Presenter_name = reader["project_head_name"].ToString();
                    //            rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                    //            rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                    //            rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                    //            rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                    //            rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                    //            rptData.Job_Position = "";
                    //            rptData.Faculty_name = reader["faculty_name"].ToString();
                    //            rptData.Research_name_thai = reader["project_name_thai"].ToString();
                    //            rptData.Research_name_eng = reader["project_name_eng"].ToString();
                    //            rptData.Faculty_name = reader["faculty_name"].ToString();

                    //            rptData.Advisor_fullname = reader["project_consultant"].ToString();
                    //            rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                    //            rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                    //            rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                    //        }
                    //    }
                    //    reader.Close();
                    //}



                    conn.Close();
                }

                rptR1 rpt1 = new rptR1();
                rptR2 rpt2 = new rptR2();

                ObjectDataSource ds1_2 = new ObjectDataSource();
                ds1_2.Constructor = new ObjectConstructorInfo();
                ds1_2.DataSource = rptData1_2;

                string report_nameR1_2 = "A1_R1_2_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_pathR1_2 = _IEnvironmentConfig.PathReport + report_nameR1_2;

                string R16_title = "";
                if (rptData1_2 != null && rptData1_2.projecttype == "1")
                {
                    rpt1.DataSource = ds1_2;
                    rpt1.ExportToPdf(report_full_pathR1_2);
                    R16_title = "ระดับห้องปฏิบัติการ";
                }
                if (rptData1_2 != null && rptData1_2.projecttype == "2")
                {
                    rpt2.DataSource = ds1_2;
                    rpt2.ExportToPdf(report_full_pathR1_2);
                    R16_title = "ระดับภาคสนาม";
                }
                string fBase64 = string.Empty;
                if (File.Exists(report_full_pathR1_2))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_pathR1_2));
                    fBase64 = readFileByte;
                }
                resp.filename1_2 = report_nameR1_2;
                resp.filebase1_2_64 = fBase64;



                rptR16 rpt16 = new rptR16();
                ObjectDataSource ds16 = new ObjectDataSource();
                ds16.Constructor = new ObjectConstructorInfo();
                //ds16.DataSource = rptData16;
                string report_nameR16 = "A1_R16_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_pathR16 = _IEnvironmentConfig.PathReport + report_nameR16;
                rpt16.DataSource = ds16;
                rpt16.ExportToPdf(report_full_pathR16);
                if (File.Exists(report_full_pathR16))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_pathR16));
                    fBase64 = readFileByte;
                }
                resp.filename16 = report_nameR16;
                resp.filebase16_64 = fBase64;


            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 3 --------------------------------------------------------------------------
        public async Task<model_rpt_3_file> GetReportR3Async(int doc_id)
        {
            model_rpt_3_file resp = new model_rpt_3_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR3 rpt = new rptR3();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A3_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 4 --------------------------------------------------------------------------
        public async Task<model_rpt_4_file> GetReportR4Async(int doc_id)
        {
            model_rpt_4_file resp = new model_rpt_4_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR4 rpt = new rptR4();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A4_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 5 --------------------------------------------------------------------------
        public async Task<model_rpt_5_file> GetReportR5Async(int doc_id)
        {
            model_rpt_5_file resp = new model_rpt_5_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR5 rpt = new rptR5();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A5_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 6 --------------------------------------------------------------------------
        public async Task<model_rpt_6_file> GetReportR6Async(int doc_id)
        {
            model_rpt_6_file resp = new model_rpt_6_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR6 rpt = new rptR6();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A6_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 7 --------------------------------------------------------------------------
        public async Task<model_rpt_7_file> GetReportR7Async(int doc_id)
        {
            model_rpt_7_file resp = new model_rpt_7_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR7 rpt = new rptR7();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A7_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 8 --------------------------------------------------------------------------
        public async Task<model_rpt_8_file> GetReportR8Async(int doc_id)
        {
            model_rpt_8_file resp = new model_rpt_8_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR8 rpt = new rptR8();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "B1_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 9 --------------------------------------------------------------------------
        public async Task<model_rpt_9_file> GetReportR9Async(int doc_id)
        {
            model_rpt_9_file resp = new model_rpt_9_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR9 rpt = new rptR9();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "D1_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 10 --------------------------------------------------------------------------
        public async Task<model_rpt_10_file> GetReportR10Async(int doc_id)
        {
            model_rpt_10_file resp = new model_rpt_10_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR10 rpt = new rptR10();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C2_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 11 --------------------------------------------------------------------------
        public async Task<model_rpt_11_file> GetReportR11Async(int doc_id)
        {
            model_rpt_11_file resp = new model_rpt_11_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR11 rpt = new rptR11();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C1_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 12 --------------------------------------------------------------------------
        public async Task<model_rpt_12_file> GetReportR12Async(int doc_id)
        {
            model_rpt_12_file resp = new model_rpt_12_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR12 rpt = new rptR12();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C3_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 13 --------------------------------------------------------------------------
        public async Task<model_rpt_13_file> GetReportR13Async(int doc_id)
        {
            model_rpt_13_file resp = new model_rpt_13_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_2_report rptData = new model_rpt_2_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_2", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_type"].ToString();
                //                rptData.Doc_head_2 = reader["doc_number"].ToString();
                //                rptData.Doc_head_4 = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
                //                rptData.Presenter_name = reader["project_head_name"].ToString();
                //                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                //                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                //                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                //                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                //                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                //                rptData.Job_Position = "";
                //                rptData.Faculty_name = reader["faculty_name"].ToString();
                //                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                //                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                //                rptData.Faculty_name = reader["faculty_name"].ToString();

                //                rptData.Advisor_fullname = reader["project_consultant"].ToString();
                //                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                //                rptData.co_research_fullname1 = reader["member_project_1"].ToString();
                //                rptData.co_research_fullname2 = reader["member_project_2"].ToString();

                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR13 rpt = new rptR13();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C3_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt.DataSource = dataSource;
                rpt.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 14 --------------------------------------------------------------------------

        public async Task<model_rpt_14_file> GetReportR14Async(int doc_id)
        {
            model_rpt_14_file resp = new model_rpt_14_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                model_rpt_14_report rptData = new model_rpt_14_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_15", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_according_type_method"].ToString();
                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR14 rpt14 = new rptR14();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt14.DataSource = dataSource;
                rpt14.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        //public async Task<ModelMenuR1ReportFile> GetReportR14Async(int meeting_id)
        //{
        //    ModelMenuR1ReportFile resp = new ModelMenuR1ReportFile();

        //    var close_doc = await CloseDocumentMeetingOfYearAsync(meeting_id);

        //    if (!close_doc.Status)
        //    {
        //        resp.message = close_doc.Message;
        //        return resp;
        //    }

        //    var cultureInfo = new CultureInfo("th-TH");
        //    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        //    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


        //    try
        //    {
        //        model_r14 rpt_header = new model_r14()
        //        {
        //            head_line_1 = "รายงานการประชุมคณะกรรมการเพื่อความปลอดภัยทางชีวภาพ มหาวิทยาลัยนเรศวร",
        //            head_line_4 = "เวลา 09.00 น. เป็นต้นไป",
        //            head_line_6 = "รายนามผู้เข้าประชุม",
        //            head_line_7 = "รายนามผู้ติดราชการ",
        //            head_line_8 = "เริ่มประชุม",
        //            head_line_9 = "-",
        //            meeting_close = "-"
        //        };

        //        rpt_header.ListMeetingData = new List<model_ListMeetingName>();
        //        rpt_header.ListMemberData = new List<model_ListMeetingName>();
        //        rpt_header.ListAgendaData_1_1 = new List<model_ListAgendaData_1>();
        //        rpt_header.ListAgendaData_1_2 = new List<model_ListAgendaData_1>();
        //        rpt_header.ListAgendaData_2 = new List<model_ListAgendaData_2>();
        //        rpt_header.ListAgendaData_3 = new List<model_ListAgendaData_3_4>();
        //        rpt_header.ListAgendaData_4 = new List<model_ListAgendaData_3_4>();
        //        rpt_header.ListAgendaData_5 = new List<model_ListAgendaData_5>();

        //        // Get All User Register
        //        IList<ModelMenuR1RegisterInfo> list_userinfo = await GetRegisterInforAsync(0);

        //        using (SqlConnection conn = new SqlConnection(ConnectionString))
        //        {
        //            conn.Open();

        //            string sql = "SELECT TOP(1) doc_id,year_of_meeting,meeting_round,B.name_thai, " +
        //                        "meeting_date, meeting_location,meeting_start,meeting_close,committees_array,attendees_array " +
        //                        "FROM Doc_MenuC3 A " +
        //                        "LEFT OUTER JOIN MST_MeetingRecordType B " +
        //                        "ON A.meeting_record_id = B.id " +
        //                        "WHERE doc_id='" + meeting_id + "' ";

        //            // ส่วนหัวเอกสาร
        //            using (SqlCommand command = new SqlCommand(sql, conn))
        //            {
        //                SqlDataReader reader = await command.ExecuteReaderAsync();

        //                if (reader.HasRows)
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        rpt_header.head_line_2 = "ครั้งที่ " + Convert.ToInt32(reader["meeting_round"]) + "/" + Convert.ToInt32(reader["year_of_meeting"]);
        //                        rpt_header.head_line_3 = "วัน" + Convert.ToDateTime(reader["meeting_date"]).ToString("dddd ที่ MMMM yyyy");
        //                        rpt_header.head_line_5 = reader["meeting_location"].ToString();
        //                        rpt_header.head_line_9 = reader["meeting_start"].ToString();
        //                        rpt_header.meeting_close = reader["meeting_close"].ToString();

        //                        // Meeting Committees
        //                        List<ModelSelectOption> list_committees = JsonConvert.DeserializeObject<List<ModelSelectOption>>(reader["committees_array"].ToString());
        //                        if (list_committees != null && list_committees.Count > 0)
        //                        {
        //                            int ir = 1;
        //                            foreach (var item in list_committees)
        //                            {
        //                                var user_id = Encoding.UTF8.GetString(Convert.FromBase64String(item.value));
        //                                var user_info = list_userinfo.FirstOrDefault(e => e.registerid == user_id);

        //                                model_ListMeetingName data = new model_ListMeetingName()
        //                                {
        //                                    seq = ir.ToString() + ".",
        //                                    name = (user_info != null) ? user_info.name : "",
        //                                    position = (user_info != null) ? user_info.position : "",
        //                                    department = (user_info != null) ? "(" + user_info.faculty + ")" : "",
        //                                };
        //                                rpt_header.ListMeetingData.Add(data);
        //                                ir++;
        //                            }
        //                        }

        //                        // Meeting Attendees
        //                        List<ModelSelectOption> list_attendees = JsonConvert.DeserializeObject<List<ModelSelectOption>>(reader["attendees_array"].ToString());
        //                        if (list_attendees != null && list_attendees.Count > 0)
        //                        {
        //                            int ir = 1;
        //                            foreach (var item in list_attendees)
        //                            {
        //                                var user_id = Encoding.UTF8.GetString(Convert.FromBase64String(item.value));
        //                                var user_info = list_userinfo.FirstOrDefault(e => e.registerid == user_id);

        //                                model_ListMeetingName data = new model_ListMeetingName()
        //                                {
        //                                    seq = ir.ToString() + ".",
        //                                    name = (user_info != null) ? user_info.name : "",
        //                                    position = (user_info != null) ? user_info.position : "",
        //                                    department = (user_info != null) ? "(" + user_info.faculty + ")" : "",
        //                                };
        //                                rpt_header.ListMemberData.Add(data);
        //                                ir++;
        //                            }
        //                        }
        //                    }
        //                }
        //                reader.Close();
        //            }


        //            // ระเบียบวาระที่ 1.1 -------------------------------------------------------------------------------------

        //            string sql_1_1 = "SELECT * FROM Doc_MenuC3_Tab1 WHERE meeting_id='" + meeting_id + "' AND group_data='1.1' ORDER BY group_data,id ASC";

        //            using (SqlCommand cmd_1_1 = new SqlCommand(sql_1_1, conn))
        //            {
        //                SqlDataReader reader_1_1 = await cmd_1_1.ExecuteReaderAsync();

        //                if (reader_1_1.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_1_1.ReadAsync())
        //                    {
        //                        model_ListAgendaData_1 rpt_data_1_1 = new model_ListAgendaData_1()
        //                        {
        //                            subject_line_1 = "ระเบียบวาระที่ 1.1." + ir + " เรื่อง " + reader_1_1["input1"].ToString(),
        //                            detail_summary = reader_1_1["input2"].ToString(),
        //                            detail_conclusion = reader_1_1["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_1_1.Add(rpt_data_1_1);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_1_1.Close();
        //            }

        //            // ระเบียบวาระที่ 1.2 -------------------------------------------------------------------------------------

        //            string sql_1_2 = "SELECT * FROM Doc_MenuC3_Tab1 WHERE meeting_id='" + meeting_id + "' AND group_data='1.2' ORDER BY group_data,id ASC";

        //            using (SqlCommand cmd_1_2 = new SqlCommand(sql_1_2, conn))
        //            {
        //                SqlDataReader reader_1_2 = await cmd_1_2.ExecuteReaderAsync();

        //                if (reader_1_2.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_1_2.ReadAsync())
        //                    {
        //                        model_ListAgendaData_1 rpt_data_1_2 = new model_ListAgendaData_1()
        //                        {
        //                            subject_line_1 = "ระเบียบวาระที่ 1.2." + ir + " เรื่อง " + reader_1_2["input1"].ToString(),
        //                            detail_summary = reader_1_2["input2"].ToString(),
        //                            detail_conclusion = reader_1_2["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_1_2.Add(rpt_data_1_2);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_1_2.Close();
        //            }

        //            // ระเบียบวาระที่ 2 -------------------------------------------------------------------------------------

        //            string sql_2 = "SELECT * FROM Doc_MenuC3_Tab2 WHERE meeting_id='" + meeting_id + "' ORDER BY id ASC";

        //            using (SqlCommand cmd_2 = new SqlCommand(sql_2, conn))
        //            {
        //                SqlDataReader reader_2 = await cmd_2.ExecuteReaderAsync();

        //                if (reader_2.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_2.ReadAsync())
        //                    {
        //                        model_ListAgendaData_2 rpt_data_2 = new model_ListAgendaData_2()
        //                        {
        //                            subject = "ระเบียบวาระที่ 2." + ir + " เรื่อง " + reader_2["input1"].ToString(),
        //                            detail_summary = reader_2["input3"].ToString(),
        //                            attachmen_name = reader_2["input2"].ToString(),
        //                            detail_conclusion = reader_2["input4"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_2.Add(rpt_data_2);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_2.Close();
        //            }


        //            // ระเบียบวาระที่ 5 -------------------------------------------------------------------------------------

        //            model_ListAgendaData_5 rpt_data_5_default = new model_ListAgendaData_5()
        //            {
        //                subject = "ระเบียบวารที่ 5.1",
        //            };

        //            string sql_5 = "SELECT * FROM Doc_MenuC3_Tab5 WHERE meeting_id='" + meeting_id + "' ORDER BY ID ASC";

        //            using (SqlCommand cmd_5 = new SqlCommand(sql_5, conn))
        //            {
        //                SqlDataReader reader_5 = await cmd_5.ExecuteReaderAsync();

        //                if (reader_5.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_5.ReadAsync())
        //                    {
        //                        model_ListAgendaData_5 rpt_data_5 = new model_ListAgendaData_5()
        //                        {
        //                            subject = "ระเบียบวาระที่ 5." + ir + " เรื่อง " + reader_5["input1"].ToString(),
        //                            detail_summary = reader_5["input2"].ToString(),
        //                            detail_conclusion = reader_5["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_5.Add(rpt_data_5);
        //                        ir += 1;
        //                    }
        //                }
        //                else rpt_header.ListAgendaData_5.Add(rpt_data_5_default);
        //                reader_5.Close();
        //            }


        //            // ผู้เซ็นต์เอกสาร ----------------------------------------------------------------------------
        //            rpt_header.signature_1 = new model_signature()
        //            {
        //                name = "(นางสาวปรางทิพย์ แก้วประสิทธิ์)",
        //                position_name = "เจ้าหน้าที่บริหารงานทั่วไป",
        //                assign_name = "ผู้จัดทำรายงานการประชุม",
        //            };
        //            rpt_header.signature_2 = new model_signature()
        //            {
        //                name = "(นายยงยุทธ บ่อแก้ว)",
        //                position_name = "หัวหน้างานจัดการมาตรฐานและเครือข่าย",
        //                assign_name = "ผู้ตรวจรายงานการประชุม",
        //            };
        //            rpt_header.signature_3 = new model_signature()
        //            {
        //                name = "(ดร.วิสาข์ สุพรรณไพบูลย์)",
        //                position_name = "ประธานคณะกรรมการเพือความปลอดภัยทางชีวภาพ",
        //                assign_name = "มหาวิทยาลัยนเรศวร",
        //            };

        //            conn.Close();
        //        }

        //        rptR14 rpt = new rptR14();

        //        ObjectDataSource dataSource = new ObjectDataSource();

        //        dataSource.Constructor = new ObjectConstructorInfo();

        //        dataSource.DataSource = rpt_header;

        //        rpt.DataSource = dataSource;

        //        string report_name = meeting_id + DateTime.Now.ToString("_yyyyMMddHHmmss_r14").ToString() + ".pdf";

        //        string report_full_path = _IEnvironmentConfig.PathReport + report_name;

        //        rpt.ExportToPdf(report_full_path);

        //        string fBase64 = string.Empty;
        //        if (File.Exists(report_full_path))
        //        {
        //            string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
        //            fBase64 = readFileByte;
        //        }

        //        resp.filename = report_name;

        //        resp.filebase64 = fBase64;
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.message = ex.Message.ToString();
        //    }
        //    return resp;
        //}

        // Report 15 --------------------------------------------------------------------------
        //public async Task<ModelMenuR1ReportFile> GetReportR15Async(int meeting_id)
        //{
        //    var cultureInfo = new CultureInfo("th-TH");
        //    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        //    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        //    ModelMenuR1ReportFile resp = new ModelMenuR1ReportFile();
        //    try
        //    {
        //        model_r14 rpt_header = new model_r14()
        //        {
        //            head_line_1 = "รายงานการประชุมคณะกรรมการเพื่อความปลอดภัยทางชีวภาพ มหาวิทยาลัยนเรศวร",
        //            head_line_4 = "เวลา 09.00 น. เป็นต้นไป",
        //            head_line_6 = "รายนามผู้เข้าประชุม",
        //            head_line_7 = "รายนามผู้ติดตาม",
        //            head_line_8 = "เริ่มประชุม",
        //            head_line_9 = "-",
        //            meeting_close = "-"
        //        };

        //        rpt_header.ListMeetingData = new List<model_ListMeetingName>();
        //        rpt_header.ListMemberData = new List<model_ListMeetingName>();
        //        rpt_header.ListAgendaData_1_1 = new List<model_ListAgendaData_1>();
        //        rpt_header.ListAgendaData_1_2 = new List<model_ListAgendaData_1>();
        //        rpt_header.ListAgendaData_2 = new List<model_ListAgendaData_2>();
        //        rpt_header.ListAgendaData_3 = new List<model_ListAgendaData_3_4>();
        //        rpt_header.ListAgendaData_4 = new List<model_ListAgendaData_3_4>();
        //        rpt_header.ListAgendaData_5 = new List<model_ListAgendaData_5>();

        //        // Get All User Register
        //        IList<ModelMenuR1RegisterInfo> list_userinfo = await GetRegisterInforAsync(0);

        //        using (SqlConnection conn = new SqlConnection(ConnectionString))
        //        {
        //            conn.Open();

        //            string sql = "SELECT TOP(1) doc_id,year_of_meeting,meeting_round,B.name_thai, " +
        //                        "meeting_date, meeting_location,meeting_start,meeting_close,committees_array,attendees_array " +
        //                        "FROM Doc_MenuC3 A " +
        //                        "LEFT OUTER JOIN MST_MeetingRecordType B " +
        //                        "ON A.meeting_record_id = B.id " +
        //                        "WHERE doc_id='" + meeting_id + "' ";

        //            // ส่วนหัวเอกสาร
        //            using (SqlCommand command = new SqlCommand(sql, conn))
        //            {
        //                SqlDataReader reader = await command.ExecuteReaderAsync();

        //                if (reader.HasRows)
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        rpt_header.head_line_2 = "ครั้งที่ " + Convert.ToInt32(reader["meeting_round"]) + "/" + Convert.ToInt32(reader["year_of_meeting"]);
        //                        rpt_header.head_line_3 = "วัน" + Convert.ToDateTime(reader["meeting_date"]).ToString("dddd ที่ MMMM yyyy");
        //                        rpt_header.head_line_5 = reader["meeting_location"].ToString();
        //                        rpt_header.head_line_9 = reader["meeting_start"].ToString();
        //                        rpt_header.meeting_close = reader["meeting_close"].ToString();

        //                        // Meeting Committees
        //                        List<ModelSelectOption> list_committees = JsonConvert.DeserializeObject<List<ModelSelectOption>>(reader["committees_array"].ToString());
        //                        if (list_committees != null && list_committees.Count > 0)
        //                        {
        //                            int ir = 1;
        //                            foreach (var item in list_committees)
        //                            {
        //                                var user_id = Encoding.UTF8.GetString(Convert.FromBase64String(item.value));
        //                                var user_info = list_userinfo.FirstOrDefault(e => e.registerid == user_id);

        //                                model_ListMeetingName data = new model_ListMeetingName()
        //                                {
        //                                    seq = ir.ToString() + ".",
        //                                    name = (user_info != null) ? user_info.name : "",
        //                                    position = (user_info != null) ? user_info.position : "",
        //                                    department = (user_info != null) ? "(" + user_info.faculty + ")" : "",
        //                                };
        //                                rpt_header.ListMeetingData.Add(data);
        //                                ir++;
        //                            }
        //                        }

        //                        // Meeting Attendees
        //                        List<ModelSelectOption> list_attendees = JsonConvert.DeserializeObject<List<ModelSelectOption>>(reader["attendees_array"].ToString());
        //                        if (list_attendees != null && list_attendees.Count > 0)
        //                        {
        //                            int ir = 1;
        //                            foreach (var item in list_attendees)
        //                            {
        //                                var user_id = Encoding.UTF8.GetString(Convert.FromBase64String(item.value));
        //                                var user_info = list_userinfo.FirstOrDefault(e => e.registerid == user_id);

        //                                model_ListMeetingName data = new model_ListMeetingName()
        //                                {
        //                                    seq = ir.ToString() + ".",
        //                                    name = (user_info != null) ? user_info.name : "",
        //                                    position = (user_info != null) ? user_info.position : "",
        //                                    department = (user_info != null) ? "(" + user_info.faculty + ")" : "",
        //                                };
        //                                rpt_header.ListMemberData.Add(data);
        //                                ir++;
        //                            }
        //                        }
        //                    }
        //                }
        //                reader.Close();
        //            }


        //            // ระเบียบวาระที่ 1.1 -------------------------------------------------------------------------------------

        //            string sql_1_1 = "SELECT * FROM Doc_MenuC3_Tab1 WHERE meeting_id='" + meeting_id + "' AND group_data='1.1' ORDER BY group_data,id ASC";

        //            using (SqlCommand cmd_1_1 = new SqlCommand(sql_1_1, conn))
        //            {
        //                SqlDataReader reader_1_1 = await cmd_1_1.ExecuteReaderAsync();

        //                if (reader_1_1.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_1_1.ReadAsync())
        //                    {
        //                        model_ListAgendaData_1 rpt_data_1_1 = new model_ListAgendaData_1()
        //                        {
        //                            subject_line_1 = "1.1." + ir + " เรื่อง " + reader_1_1["input1"].ToString(),
        //                            detail_summary = reader_1_1["input2"].ToString(),
        //                            detail_conclusion = reader_1_1["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_1_1.Add(rpt_data_1_1);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_1_1.Close();
        //            }

        //            // ระเบียบวาระที่ 1.2 -------------------------------------------------------------------------------------

        //            string sql_1_2 = "SELECT * FROM Doc_MenuC3_Tab1 WHERE meeting_id='" + meeting_id + "' AND group_data='1.2' ORDER BY group_data,id ASC";

        //            using (SqlCommand cmd_1_2 = new SqlCommand(sql_1_2, conn))
        //            {
        //                SqlDataReader reader_1_2 = await cmd_1_2.ExecuteReaderAsync();

        //                if (reader_1_2.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_1_2.ReadAsync())
        //                    {
        //                        model_ListAgendaData_1 rpt_data_1_2 = new model_ListAgendaData_1()
        //                        {
        //                            subject_line_1 = "1.2." + ir + " เรื่อง " + reader_1_2["input1"].ToString(),
        //                            detail_summary = reader_1_2["input2"].ToString(),
        //                            detail_conclusion = reader_1_2["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_1_2.Add(rpt_data_1_2);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_1_2.Close();
        //            }

        //            // ระเบียบวาระที่ 2 -------------------------------------------------------------------------------------

        //            string sql_2 = "SELECT * FROM Doc_MenuC3_Tab2 WHERE meeting_id='" + meeting_id + "' ORDER BY id ASC";

        //            using (SqlCommand cmd_2 = new SqlCommand(sql_2, conn))
        //            {
        //                SqlDataReader reader_2 = await cmd_2.ExecuteReaderAsync();

        //                if (reader_2.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_2.ReadAsync())
        //                    {
        //                        model_ListAgendaData_2 rpt_data_2 = new model_ListAgendaData_2()
        //                        {
        //                            subject = "2." + ir + " เรื่อง " + reader_2["input1"].ToString(),
        //                            detail_summary = reader_2["input3"].ToString(),
        //                            attachmen_name = reader_2["input2"].ToString(),
        //                            detail_conclusion = reader_2["input4"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_2.Add(rpt_data_2);
        //                        ir += 1;
        //                    }
        //                }
        //                reader_2.Close();
        //            }


        //            // ระเบียบวาระที่ 5 -------------------------------------------------------------------------------------

        //            model_ListAgendaData_5 rpt_data_5_default = new model_ListAgendaData_5()
        //            {
        //                subject = "5.1",
        //            };

        //            string sql_5 = "SELECT * FROM Doc_MenuC3_Tab5 WHERE meeting_id='" + meeting_id + "' ORDER BY ID ASC";

        //            using (SqlCommand cmd_5 = new SqlCommand(sql_5, conn))
        //            {
        //                SqlDataReader reader_5 = await cmd_5.ExecuteReaderAsync();

        //                if (reader_5.HasRows)
        //                {
        //                    int ir = 1;
        //                    while (await reader_5.ReadAsync())
        //                    {
        //                        model_ListAgendaData_5 rpt_data_5 = new model_ListAgendaData_5()
        //                        {
        //                            subject = "5." + ir + " เรื่อง " + reader_5["input1"].ToString(),
        //                            detail_summary = reader_5["input2"].ToString(),
        //                            detail_conclusion = reader_5["input3"].ToString(),
        //                        };
        //                        rpt_header.ListAgendaData_5.Add(rpt_data_5);
        //                        ir += 1;
        //                    }
        //                }
        //                else rpt_header.ListAgendaData_5.Add(rpt_data_5_default);
        //                reader_5.Close();
        //            }


        //            // ผู้เซ็นต์เอกสาร ----------------------------------------------------------------------------
        //            rpt_header.signature_1 = new model_signature()
        //            {
        //                name = "(นางสาวปรางทิพย์ แก้วประสิทธิ์)",
        //                position_name = "เจ้าหน้าที่บริหารงานทั่วไป",
        //                assign_name = "ผู้จัดทำรายงานการประชุม",
        //            };
        //            rpt_header.signature_2 = new model_signature()
        //            {
        //                name = "(นายยงยุทธ บ่อแก้ว)",
        //                position_name = "หัวหน้างานจัดการมาตรฐานและเครือข่าย",
        //                assign_name = "ผู้ตรวจรายงานการประชุม",
        //            };
        //            rpt_header.signature_3 = new model_signature()
        //            {
        //                name = "(ดร.วิสาข์ สุพรรณไพบูลย์)",
        //                position_name = "ประธานคณะกรรมการเพือความปลอดภัยทางชีวภาพ",
        //                assign_name = "มหาวิทยาลัยนเรศวร",
        //            };

        //            conn.Close();
        //        }

        //        //----------------------------------------------------
        //        rptR15 rpt_15 = new rptR15();

        //        ObjectDataSource dts15 = new ObjectDataSource();

        //        dts15.Constructor = new ObjectConstructorInfo();

        //        dts15.DataSource = rpt_header;

        //        rpt_15.DataSource = dts15;

        //        //----------------------------------------------------

        //        //----------------------------------------------------

        //        string report_name = meeting_id + DateTime.Now.ToString("_yyyyMMddHHmmss_r14").ToString() + ".pdf";

        //        string report_full_path = _IEnvironmentConfig.PathReport + report_name;

        //        rpt_15.ExportToPdf(report_full_path);

        //        string fBase64 = string.Empty;
        //        if (File.Exists(report_full_path))
        //        {
        //            string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
        //            fBase64 = readFileByte;
        //        }

        //        resp.filename = report_name;

        //        resp.filebase64 = fBase64;
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.message = ex.Message.ToString();
        //    }
        //    return resp;
        //}

        public async Task<IList<ModelMenuR1RegisterInfo>> GetRegisterInforAsync(int user_id)
        {

            string sql = "SELECT A.register_id,A.email, " +
                        "(A.first_name + A.full_name) as full_name, B.name_thai as position, C.name_thai as faculty " +
                        "FROM RegisterUser A " +
                        "LEFT OUTER JOIN MST_Position B ON A.position = B.id " +
                        "LEFT OUTER JOIN MST_Faculty C ON A.faculty = C.id " +
                        "WHERE 1=1 ";

            if (user_id > 0)
            {
                sql += " AND A.register_id ='" + user_id + "'";
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelMenuR1RegisterInfo> list = new List<ModelMenuR1RegisterInfo>();
                        while (await reader.ReadAsync())
                        {
                            ModelMenuR1RegisterInfo e = new ModelMenuR1RegisterInfo();
                            e.registerid = reader["register_id"].ToString();
                            e.email = reader["email"].ToString();
                            e.name = reader["full_name"].ToString();
                            e.position = reader["position"].ToString();
                            e.faculty = reader["faculty"].ToString();
                            list.Add(e);
                        }
                        return list;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<model_rpt_15_file> GetReportR15Async(int doc_id)
        {
            model_rpt_15_file resp = new model_rpt_15_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                model_rpt_15_report rptData = new model_rpt_15_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_15", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_according_type_method"].ToString();
                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}

                rptR15 rpt15 = new rptR15();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "C_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                rpt15.DataSource = dataSource;
                rpt15.ExportToPdf(report_full_path);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        // Report 17-18 --------------------------------------------------------------------------
        public async Task<model_rpt_17_file> GetReportR17_18Async(int doc_id)
        {
            model_rpt_17_file resp = new model_rpt_17_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                model_rpt_17_report rptData = new model_rpt_17_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_17_18", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData.projecttype = reader["project_according_type_method"].ToString();
                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR17 rpt17 = new rptR17();
                rptR18 rpt18 = new rptR18();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                //dataSource.DataSource = rptData;

                string report_name = "A2_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path = _IEnvironmentConfig.PathReport + report_name;

                if (rptData != null && rptData.projecttype == "1")
                {
                    rpt17.DataSource = dataSource;
                    rpt17.ExportToPdf(report_full_path);
                }

                if (rptData != null && rptData.projecttype == "2")
                {
                    rpt18.DataSource = dataSource;
                    rpt18.ExportToPdf(report_full_path);
                }

                string fBase64 = string.Empty;
                if (File.Exists(report_full_path))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path));
                    fBase64 = readFileByte;
                }

                resp.filename = report_name;

                resp.filebase64 = fBase64;
            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }


        // รายงานหลังจากบันทึกมติที่ประชุมครบเรียบร้อย -----------------------------------------------------
        public async Task<model_rpt_meeting_file> GetAllReportMeetingAsync(int doc_id)
        {
            model_rpt_meeting_file resp = new model_rpt_meeting_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                //model_rpt_17_report rptData = new model_rpt_17_report();

                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("sp_report_17_18", conn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                //        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //        if (reader.HasRows)
                //        {
                //            while (await reader.ReadAsync())
                //            {
                //                rptData.projecttype = reader["project_according_type_method"].ToString();
                //            }
                //        }
                //        reader.Close();
                //    }
                //    conn.Close();
                //}


                string fBase64 = string.Empty;

                // Report 9 ----------------------------------------------------------------------------------------------------------
                rptR9 rpt9 = new rptR9();
                ObjectDataSource ds9 = new ObjectDataSource();
                ds9.Constructor = new ObjectConstructorInfo();
                //ds9.DataSource = rptData;
                string report_name_9 = "meeting_r9_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path_9 = _IEnvironmentConfig.PathReport + report_name_9;
                rpt9.DataSource = ds9;
                rpt9.ExportToPdf(report_full_path_9);
                if (File.Exists(report_full_path_9))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path_9));
                    fBase64 = readFileByte;
                }
                resp.filename9 = report_name_9;
                resp.filebase964 = fBase64;



                // Report 12 ----------------------------------------------------------------------------------------------------------
                rptR12 rpt12 = new rptR12();
                ObjectDataSource ds12 = new ObjectDataSource();
                ds12.Constructor = new ObjectConstructorInfo();
                //ds12.DataSource = rptData;

                string report_name_12 = "meeting_r12_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path_12 = _IEnvironmentConfig.PathReport + report_name_12;

                rpt12.DataSource = ds12;
                rpt12.ExportToPdf(report_full_path_12);

                if (File.Exists(report_full_path_12))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path_12));
                    fBase64 = readFileByte;
                }
                resp.filename12 = report_name_12;
                resp.filebase1264 = fBase64;


                // Report 13 ----------------------------------------------------------------------------------------------------------
                rptR13 rpt13 = new rptR13();
                ObjectDataSource ds13 = new ObjectDataSource();
                ds13.Constructor = new ObjectConstructorInfo();
                //ds13.DataSource = rptData;

                string report_name_13 = "meeting_r13_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path_13 = _IEnvironmentConfig.PathReport + report_name_13;

                rpt13.DataSource = ds13;
                rpt13.ExportToPdf(report_full_path_13);

                if (File.Exists(report_full_path_13))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path_13));
                    fBase64 = readFileByte;
                }
                resp.filename13 = report_name_13;
                resp.filebase1364 = fBase64;

                // Report 14 ----------------------------------------------------------------------------------------------------------
                rptR14 rpt14 = new rptR14();
                ObjectDataSource ds14 = new ObjectDataSource();
                ds14.Constructor = new ObjectConstructorInfo();
                //ds13.DataSource = rptData;

                string report_name_14 = "meeting_r14_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_path_14 = _IEnvironmentConfig.PathReport + report_name_14;

                rpt14.DataSource = ds14;
                rpt14.ExportToPdf(report_full_path_14);

                if (File.Exists(report_full_path_14))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_path_14));
                    fBase64 = readFileByte;
                }
                resp.filename14 = report_name_14;
                resp.filebase1464 = fBase64;

            }
            catch (Exception ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }


    }

}
