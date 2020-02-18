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
                model_rpt_16_report rptData16 = new model_rpt_16_report();

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
                                rptData1_2.doccode = reader["project_type"].ToString() == "1" ? "NUIBC01-2" : "NUIBC02-2";
                                rptData1_2.projecttype = reader["project_type"].ToString();
                                rptData1_2.title = reader["name_thai"].ToString();
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
                                rptData1_2.HeadofResearch_fullname = reader["project_head_name"].ToString();
                                rptData1_2.Advisor_fullname = reader["project_consult_name"].ToString();


                                MemberProject member1json = new MemberProject();
                                member1json = JsonConvert.DeserializeObject<MemberProject>(reader["member_project_1"].ToString());
                                if (member1json != null)
                                {
                                    var member1 = await GetRegisterInforAsync(member1json.projecthead);
                                    if (member1 != null && member1.Count > 0)
                                        rptData1_2.co_research_fullname1 = member1.FirstOrDefault().name;
                                }

                                MemberProject member2json = new MemberProject();
                                member2json = JsonConvert.DeserializeObject<MemberProject>(reader["member_project_2"].ToString());
                                if (member2json != null)
                                {
                                    var member2 = await GetRegisterInforAsync(member2json.projecthead);
                                    if (member2 != null && member2.Count > 0)
                                        rptData1_2.co_research_fullname2 = member2.FirstOrDefault().name;
                                }

                                MemberProject member3json = new MemberProject();
                                member3json = JsonConvert.DeserializeObject<MemberProject>(reader["member_project_3"].ToString());
                                if (member3json != null)
                                {
                                    var member3 = await GetRegisterInforAsync(member3json.projecthead);
                                    if (member3 != null && member3.Count > 0)
                                        rptData1_2.co_research_fullname3 = member3.FirstOrDefault().name;
                                }

                            }
                        }
                        reader.Close();
                    }



                    using (SqlCommand cmd = new SqlCommand("sp_report_16", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData16.project_head = reader["project_head"].ToString();
                                rptData16.project_namethai = reader["project_name_thai"].ToString();
                                rptData16.project_nameeng = reader["project_name_eng"].ToString();
                                rptData16.fund_source = reader["money_supply"].ToString();
                                rptData16.fund_amount = Convert.ToDecimal(reader["budget"]);

                            }
                        }
                        reader.Close();
                    }

                    conn.Close();

                    conn.Close();
                }

                rptR1 rpt1 = new rptR1();

                ObjectDataSource ds1_2 = new ObjectDataSource();
                ds1_2.Constructor = new ObjectConstructorInfo();
                ds1_2.DataSource = rptData1_2;

                string report_nameR1_2 = "R1_2_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_pathR1_2 = _IEnvironmentConfig.PathReport + report_nameR1_2;

                rpt1.DataSource = ds1_2;
                rpt1.ExportToPdf(report_full_pathR1_2);

                string fBase64 = string.Empty;
                if (File.Exists(report_full_pathR1_2))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_pathR1_2));
                    fBase64 = readFileByte;
                }
                resp.filename1and2 = report_nameR1_2;
                resp.filebase1and264 = fBase64;



                rptR16 rpt16 = new rptR16();
                ObjectDataSource ds16 = new ObjectDataSource();
                ds16.Constructor = new ObjectConstructorInfo();
                ds16.DataSource = rptData16;

                string R16_title = "";
                if (rptData1_2 != null && rptData1_2.projecttype == "1") R16_title = "ระดับห้องปฏิบัติการ";
                if (rptData1_2 != null && rptData1_2.projecttype == "2") R16_title = "ระดับภาคสนาม";

                string report_nameR16 = "R16_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
                string report_full_pathR16 = _IEnvironmentConfig.PathReport + report_nameR16;
                rpt16.DataSource = ds16;
                rpt16.ExportToPdf(report_full_pathR16);
                if (File.Exists(report_full_pathR16))
                {
                    string readFileByte = "data:application/pdf;base64," + Convert.ToBase64String(File.ReadAllBytes(report_full_pathR16));
                    fBase64 = readFileByte;
                }
                resp.filename16 = report_nameR16;
                resp.filebase1664 = fBase64;


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
            string report_no = await CreateReportNumberAsync(2563, "R8", doc_id, "B1");

            try
            {
                model_rpt_3_report rptData = new model_rpt_3_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                //rptData.projecttype = reader["project_type"].ToString();
                                //rptData.Doc_head_2 = reader["doc_number"].ToString();
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.Presenter_name = reader["project_head_name"].ToString();
                                rptData.Position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.Position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.Position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.Position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.Position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData.Job_Position = "";
                                rptData.Faculty_name = reader["faculty_name"].ToString();
                                rptData.Research_name_thai = reader["project_name_thai"].ToString();
                                rptData.Research_name_eng = reader["project_name_eng"].ToString();
                                rptData.Faculty_name = reader["faculty_name"].ToString();
                                rptData.HeadofResearch_fullname = reader["project_head_name"].ToString();
                                rptData.certificate_date = reader["Certificate_date"].ToString();
                                rptData.certificate_month = reader["Certificate_month"].ToString();
                                rptData.certificate_year = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["Certificate_year"])));
                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR3 rpt = new rptR3();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R3_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
                model_rpt_4_report rptData = new model_rpt_4_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_4", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                //rptData.projecttype = reader["project_type"].ToString();
                                //rptData.Doc_head_2 = reader["doc_number"].ToString();
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.presenter_name = reader["project_head_name"].ToString();
                                rptData.position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData.Job_Position = "";
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.research_name_thai = reader["project_name_thai"].ToString();
                                rptData.research_name_eng = reader["project_name_eng"].ToString();
                                rptData.headofresearch_fullname = reader["project_head_name"].ToString();
                                rptData.certificate_date = reader["Certificate_date"].ToString();
                                rptData.certificate_month = reader["Certificate_month"].ToString();
                                rptData.certificate_year = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["Certificate_year"])));
                                rptData.certificate_type = reader["accept_type_name"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR4 rpt = new rptR4();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R4_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
                model_rpt_5_report rptData = new model_rpt_5_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                //rptData.projecttype = reader["project_type"].ToString();
                                //rptData.Doc_head_2 = reader["doc_number"].ToString();
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.presenter_name = reader["project_head_name"].ToString();
                                rptData.position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData.Job_Position = "";
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.research_name_thai = "ขอรายงานความก้าวหน้าโครงการวิจัยเรื่อง " + reader["project_name_thai"].ToString();
                                rptData.research_name_eng = reader["project_name_eng"].ToString();
                                rptData.headofresearch_fullname = reader["project_head_name"].ToString();
                                rptData.certificate_date = reader["Certificate_date"].ToString();
                                rptData.certificate_month = reader["Certificate_month"].ToString();
                                rptData.certificate_year = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["Certificate_year"])));
                                rptData.certificate_type = reader["accept_type_name"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR5 rpt = new rptR5();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R5_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
                model_rpt_6_report rptData = new model_rpt_6_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_6", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                //rptData.projecttype = reader["project_type"].ToString();
                                //rptData.Doc_head_2 = reader["doc_number"].ToString();
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.presenter_name = reader["project_head_name"].ToString();
                                rptData.position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData.Job_Position = "";
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.research_name_thai = "ขอปรับแก้โครงการวิจัยเรื่อง " + reader["project_name_thai"].ToString();
                                rptData.research_name_eng = reader["project_name_eng"].ToString();
                                rptData.headofresearch_fullname = reader["project_head_name"].ToString();
                                rptData.certificate_date = reader["conclusion_date"].ToString();
                                rptData.certificate_month = reader["conclusion_month"].ToString();
                                rptData.certificate_year = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["conclusion_year"])));
                                //rptData.certificate_type = reader["accept_type_name"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR6 rpt = new rptR6();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R6_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
            string report_no = "";
            int year = System.DateTime.Now.Year;

            try
            {
                model_rpt_7_report rptData = new model_rpt_7_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_7", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                //rptData.projecttype = reader["project_type"].ToString();
                                //rptData.Doc_head_2 = reader["doc_number"].ToString();
                                rptData.doc = report_no.PadLeft(4, '0');
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.presenter_name = reader["project_head_name"].ToString();
                                rptData.position_1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.position_2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.position_3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.position_4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.position_5 = (reader["check_value"].ToString()) == "5" ? true : false;
                                rptData.Job_Position = "";
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.research_name_thai = "ขอต่ออายุโครงการวิจัยเรื่อง " + reader["project_name_thai"].ToString();
                                rptData.research_name_eng = reader["project_name_eng"].ToString();
                                rptData.headofresearch_fullname = reader["project_head_name"].ToString();
                                rptData.projecttype = reader["accept_type_name"].ToString();
                                rptData.nuibc_no = reader["project_number"].ToString();
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.renew_round = reader["renew_round"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR7 rpt = new rptR7();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R7_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
            string report_no = "";
            int year = System.DateTime.Now.Year;

            try
            {
                model_rpt_8_report rptData = new model_rpt_8_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_8", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                report_no = await CreateReportNumberAsync(year, "R8", doc_id, "Doc_MenuB1");
                                rptData.docno = report_no.PadLeft(4, '0');
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.faculty_name = reader["faculty_name"].ToString();
                                rptData.research_name_eng = reader["project_name_thai"].ToString() + "   " + reader["project_name_eng"].ToString();
                                rptData.headofresearch_fullname = reader["project_head"].ToString();
                                rptData.nuibc_no = reader["project_key_number"].ToString();
                                rptData.renew_round = reader["round_of_meeting"].ToString() + '/' + reader["year_of_meeting"].ToString();
                                rptData.month_project = reader["meeting_month"].ToString();
                                rptData.year_project = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["meeting_year"])));

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR8 rpt = new rptR8();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R8_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
                model_rpt_9_report rptData = new model_rpt_9_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_9", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData.project_namethai = reader["project_name_thai"].ToString();
                                rptData.project_nameeng = reader["project_name_eng"].ToString();
                                rptData.researcher = reader["project_head_name"].ToString();
                                rptData.advisor = reader["advisorsNameThai"].ToString();
                                rptData.faculty = reader["faculty_name"].ToString();
                                rptData.project_no = reader["project_number"].ToString();
                                rptData.certificate_no = reader["acceptProjectNo"].ToString();
                                rptData.round = reader["RenewRound"].ToString();
                                rptData.certificate_date = reader["accept_date"].ToString();
                                rptData.certificate_month = reader["accept_month"].ToString();
                                rptData.certificate_year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["accept_year"]));
                                rptData.expire_date = reader["expire_date"].ToString();
                                rptData.expire_month = reader["expire_month"].ToString();
                                rptData.expire_year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["expire_year"]));
                                rptData.projecttype = reader["accept_type_name"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR9 rpt = new rptR9();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R9_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
            string report_no = await CreateReportNumberAsync(2563, "R8", doc_id, "B1");

            try
            {
                model_rpt_10_report rptData = new model_rpt_10_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_10", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {

                                rptData.projectnamethai = reader["project_name_thai"].ToString();
                                rptData.projectnameeng = reader["project_name_eng"].ToString();
                                rptData.projectheadname = reader["project_head_name"].ToString();
                                rptData.advisor = reader["consult_name"].ToString();
                                rptData.facultyname = reader["faculty_name"].ToString();
                                rptData.chkbox1 = (reader["check_value"].ToString()) == "1" ? true : false;
                                rptData.chkbox2 = (reader["check_value"].ToString()) == "2" ? true : false;
                                rptData.chkbox3 = (reader["check_value"].ToString()) == "3" ? true : false;
                                rptData.chkbox4 = (reader["check_value"].ToString()) == "4" ? true : false;
                                rptData.approvetype1 = (reader["approval_type"].ToString()) == "1" ? reader["approval_type"].ToString() : "-";
                                rptData.approvetype2 = (reader["approval_type"].ToString()) == "2" ? reader["approval_type"].ToString() : "-";
                                rptData.approvetype3 = (reader["approval_type"].ToString()) == "3" ? reader["approval_type"].ToString() : "-";
                                rptData.assignername = reader["assign_name"].ToString();
                                rptData.commentconsider1 = (reader["check_value"].ToString()) == "1" ? reader["comment_consider"].ToString() : "-";
                                rptData.commentconsider2 = (reader["check_value"].ToString()) == "2" ? reader["comment_consider"].ToString() : "-";
                                rptData.commentconsider3 = (reader["check_value"].ToString()) == "3" ? reader["comment_consider"].ToString() : "-";
                                rptData.commentconsider4 = (reader["check_value"].ToString()) == "4" ? reader["comment_consider"].ToString() : "-";
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                            }
                            reader.Close();
                        }
                        conn.Close();
                    }

                    rptR10 rpt = new rptR10();

                    ObjectDataSource dataSource = new ObjectDataSource();
                    dataSource.Constructor = new ObjectConstructorInfo();
                    dataSource.DataSource = rptData;

                    string report_name = "R10_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
            string report_no = "";
            int year = System.DateTime.Now.Year;

            try
            {
                model_rpt_11_report rptData = new model_rpt_11_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_report_11", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                report_no = await CreateReportNumberAsync(year, "R11", doc_id, "Doc_MenuC1");
                                rptData.docno = report_no.PadLeft(4, '0');
                                rptData.day = reader["doc_date"].ToString();
                                rptData.month = reader["doc_month"].ToString();
                                rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                rptData.round = (reader["round_of_meeting"].ToString() + '/' + reader["year_of_meeting"].ToString());
                                rptData.meet_date = reader["meet_date"].ToString();
                                rptData.meet_month = reader["meet_month"].ToString();
                                rptData.meet_year = Convert.ToString(CommonData.ConvertYearToThai(Convert.ToInt32(reader["meet_year"])));
                                rptData.assign = reader["assigname"].ToString();
                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR11 rpt = new rptR11();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R11_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
        public async Task<model_rpt_12_file> GetReportR12Async(int doc_id, int type)
        {
            model_rpt_12_file resp = new model_rpt_12_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            string report_no = "";
            int year = System.DateTime.Now.Year;

            string store_name = "";

            if (type == 3)
            {
                store_name = "sp_report_12";
            }
            else if (type == 4)
            {
                store_name = "sp_report_12_2";
            }


            try
            {
                model_rpt_12_report rptData = new model_rpt_12_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(store_name, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {

                                if (type == 3)
                                {

                                    report_no = await CreateReportNumberAsync(year, "R13", doc_id, "Doc_MenuC3_Tab3");
                                    rptData.docno = report_no.PadLeft(4, '0');
                                    rptData.day = reader["doc_date"].ToString();
                                    rptData.month = reader["doc_month"].ToString();
                                    rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                    rptData.research_name_thai = reader["agenda_3_project_name_thai"].ToString();
                                    rptData.research_name_eng = reader["agenda_3_project_name_eng"].ToString();
                                    rptData.round = reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();
                                    rptData.sender = reader["sender"].ToString();
                                    rptData.projectno = reader["agenda_3_project_number"].ToString();
                                    rptData.comment1 = reader["comment_1_note"].ToString();
                                    rptData.comment2 = reader["comment_2_note"].ToString();
                                }
                                else if (type == 4)
                                {
                                    report_no = await CreateReportNumberAsync(year, "R12", doc_id, "Doc_MenuC3_Tab4");
                                    rptData.docno = report_no.PadLeft(4, '0');
                                    rptData.day = reader["doc_date"].ToString();
                                    rptData.month = reader["doc_month"].ToString();
                                    rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                    rptData.researchname_thai = reader["agenda_4_project_name_1"].ToString();
                                    rptData.researchname_eng = reader["agenda_4_project_name_2"].ToString();
                                    rptData.round = reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();
                                    rptData.sender = reader["sender"].ToString();
                                    rptData.projectno = reader["agenda_4_project_number"].ToString();
                                    rptData.comment1 = reader["comment_1_note"].ToString();
                                    rptData.comment2 = reader["comment_2_note"].ToString();
                                }

                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR12 rpt = new rptR12();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R12_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
        public async Task<model_rpt_13_file> GetReportR13Async(int doc_id, int type)
        {
            model_rpt_13_file resp = new model_rpt_13_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            string report_no = "";
            int year = System.DateTime.Now.Year;

            string store_name = "";

            if (type == 3)
            {
                store_name = "sp_report_13";
            }
            else if (type == 4)
            {
                store_name = "sp_report_13_2";
            }

            try
            {
                model_rpt_13_report rptData = new model_rpt_13_report();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(store_name, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.VarChar, 50).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (type == 3)
                                {
                                    report_no = await CreateReportNumberAsync(year, "R13", doc_id, "Doc_MenuC3_Tab3");
                                    rptData.docno = report_no.PadLeft(4, '0');
                                    rptData.day = reader["doc_date"].ToString();
                                    rptData.month = reader["doc_month"].ToString();
                                    rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                    rptData.researcher = reader["sender"].ToString();
                                    rptData.nuibc = reader["agenda_3_project_number"].ToString();
                                    rptData.research_name_thai = reader["agenda_3_project_name_thai"].ToString();
                                    rptData.research_name_eng = reader["agenda_3_project_name_eng"].ToString();
                                    rptData.round = reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();
                                    rptData.approvetype = reader["agenda_3_conclusion_name"].ToString();
                                }
                                else if (type == 4)
                                {
                                    report_no = await CreateReportNumberAsync(year, "R13", doc_id, "Doc_MenuC3_Tab4");
                                    rptData.docno = report_no.PadLeft(4, '0');
                                    rptData.day = reader["doc_date"].ToString();
                                    rptData.month = reader["doc_month"].ToString();
                                    rptData.year = CommonData.ConvertYearToThai(Convert.ToInt32(reader["doc_year"]));
                                    rptData.researcher = reader["sender"].ToString();
                                    rptData.nuibc = reader["agenda_4_project_number"].ToString();
                                    rptData.research_name_thai = reader["agenda_4_project_name_1"].ToString();
                                    rptData.research_name_eng = reader["agenda_4_project_name_2"].ToString();
                                    rptData.round = reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();
                                    rptData.approvetype = reader["agenda_4_conclusion_name"].ToString();
                                }
                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                rptR13 rpt = new rptR13();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R13_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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

                //Default ----------------------------------------------------------
                rptData.Doc_head_1 = "ระเบียบวารการประชุมคณะกรรมการพิจารณาฯความปลอดภัยทาชีวะภาพ มหาวิทยาลัยนเรศวร";
                rptData.Doc_head_2 = "ครั้งที่ - ";
                rptData.Doc_head_3 = "วันที่ -";
                rptData.Doc_head_4 = "เวลา -";
                rptData.Doc_head_5 = "สถานที่ -";

                rptData.list_person_meeting = new List<model_list_person_meeting>();
                rptData.list_person_out_of_meeting = new List<model_list_person_meeting>();
                rptData.list_agenda_1_1 = new List<model_list_agenda_1_1>();
                rptData.list_agenda_1_2 = new List<model_list_agenda_1_2>();
                rptData.list_agenda_2 = new List<model_list_agenda_2>();
                rptData.list_agenda_3 = new List<model_list_agenda_3>();
                rptData.list_agenda_4_1 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_2 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_3 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_4 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_5 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_6 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_7 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_8 = new List<model_list_agenda_4>();
                rptData.list_agenda_4_9 = new List<model_list_agenda_4>();
                rptData.list_agenda_5 = new List<model_list_agenda_5>();
                //--------------------------------------------------------------------

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // ส่วนหัวเอกสาร
                    using (SqlCommand cmd = new SqlCommand("sp_report_14", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData.Doc_head_1 = reader["meeting_type_name"].ToString() + " มหาวิทยาลัยนเรศวร";
                                rptData.Doc_head_2 = "ครั้งที่ " + reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();

                                int year_meeting = Convert.ToInt32(Convert.ToDateTime(reader["meeting_date"]).ToString("yyyy"));
                                rptData.Doc_head_3 = "วัน" + Convert.ToDateTime(reader["meeting_date"]).ToString("dddd dd MMM ") + CommonData.ConvertYearToThai(year_meeting);

                                rptData.Doc_head_4 = "เวลา " + ParseDataHelper.ReportEmptyValue(reader["meeting_start"].ToString()) + " เป็นต้นไป";
                                rptData.Doc_head_5 = ParseDataHelper.ReportEmptyValue(reader["meeting_location"].ToString());
                                rptData.Doc_head_6 = "เวลา " + ParseDataHelper.ReportEmptyValue(reader["meeting_close"].ToString());

                                var temp_list_person_meeting = JsonConvert.DeserializeObject<IList<model_list_person_meeting>>(reader["committees_array"].ToString());
                                if (temp_list_person_meeting != null && temp_list_person_meeting.Count > 0)
                                {
                                    int seq = 0;
                                    rptData.list_person_meeting = temp_list_person_meeting;
                                    foreach (var item in rptData.list_person_meeting)
                                    {
                                        var personal = await GetRegisterInforAsync(item.value);
                                        item.seq = (seq + 1).ToString() + ".";
                                        item.position = personal != null ? personal.FirstOrDefault().position : "";
                                        item.department = personal != null ? personal.FirstOrDefault().department : "";
                                        seq++;
                                    }
                                }
                                else
                                {
                                    model_list_person_meeting null_item = new model_list_person_meeting();
                                    null_item.seq = "-";
                                    rptData.list_person_meeting.Add(null_item);
                                }

                                var temp_list_person_out_of_meeting = JsonConvert.DeserializeObject<IList<model_list_person_meeting>>(reader["attendees_array"].ToString());
                                if (temp_list_person_out_of_meeting != null && temp_list_person_out_of_meeting.Count > 0)
                                {
                                    int seq = 0;
                                    rptData.list_person_out_of_meeting = temp_list_person_out_of_meeting;
                                    foreach (var item in rptData.list_person_out_of_meeting)
                                    {
                                        var personal = await GetRegisterInforAsync(item.value);
                                        item.seq = (seq + 1).ToString() + ".";
                                        item.position = personal != null ? personal.FirstOrDefault().position : "";
                                        item.department = personal != null ? personal.FirstOrDefault().department : "";
                                        seq++;
                                    }
                                }
                                else
                                {
                                    model_list_person_meeting null_item = new model_list_person_meeting();
                                    null_item.seq = "-";
                                    rptData.list_person_out_of_meeting.Add(null_item);
                                }

                            }
                        }
                        reader.Close();
                    }

                    // ระเบียบวารที่ 1
                    using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_1", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                string seq = reader["seq"].ToString();
                                string title = "ระเบียบวาระที่ " + (reader["group_data"].ToString() == "1.1" ? "1.1." + seq : "1.2." + seq) + " ";

                                if (reader["group_data"].ToString() == "1.1")
                                {
                                    model_list_agenda_1_1 agenda_1_1 = new model_list_agenda_1_1()
                                    {
                                        subject = title + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString()),
                                        detail_summary = ParseDataHelper.ReportEmptyValue(reader["input2"].ToString()),
                                        detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["input3"].ToString()),
                                    };
                                    rptData.list_agenda_1_1.Add(agenda_1_1);
                                }

                                if (reader["group_data"].ToString() == "1.2")
                                {
                                    model_list_agenda_1_2 agenda_1_2 = new model_list_agenda_1_2()
                                    {
                                        subject = title + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString()),
                                        detail_summary = ParseDataHelper.ReportEmptyValue(reader["input2"].ToString()),
                                        detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["input3"].ToString()),
                                    };
                                    rptData.list_agenda_1_2.Add(agenda_1_2);
                                }
                            }
                        }
                        else
                        {
                            rptData.list_agenda_1_1.Add(new model_list_agenda_1_1());
                            rptData.list_agenda_1_2.Add(new model_list_agenda_1_2());
                        }

                        reader.Close();
                    }

                    // ระเบียบวารที่ 2
                    using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_2", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                string seq = reader["seq"].ToString();
                                string title = "ระเบียบวาระที่ 2.1." + seq + " ";

                                model_list_agenda_2 agenda_2 = new model_list_agenda_2()
                                {
                                    subject = title + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString()),
                                    detail_summary = ParseDataHelper.ReportEmptyValue(reader["input3"].ToString()),
                                    detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["input4"].ToString()),
                                };
                                if (reader["group_data"].ToString() == "2.1")
                                    rptData.list_agenda_2.Add(agenda_2);

                            }
                        }
                        else rptData.list_agenda_2.Add(new model_list_agenda_2());

                        reader.Close();

                    }

                    // ระเบียบวารที่ 3 (3.1)
                    using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            int seq = 0;
                            while (await reader.ReadAsync())
                            {
                                string title = "โครงการวิจัยที่รับรองหลังจากปรับปรุง/แก้ไข จำนวน ";

                                model_list_agenda_3 agenda_3 = new model_list_agenda_3();

                                agenda_3.title = title + ParseDataHelper.ReportEmptyValueInt(Convert.ToInt32(reader["count_3"])) + " โครงการ";
                                agenda_3.subject = (seq + 1).ToString() + ". เรื่อง :";
                                agenda_3.project_number = ParseDataHelper.ReportEmptyValue(reader["agenda_3_project_number"].ToString());
                                agenda_3.project_name_thai = ParseDataHelper.ReportEmptyValue(reader["agenda_3_project_name_thai"].ToString());
                                agenda_3.project_name_eng = ParseDataHelper.ReportEmptyValue(reader["agenda_3_project_name_eng"].ToString());
                                agenda_3.project_safety_type = ParseDataHelper.ReportEmptyValue(reader["safety_type"].ToString());
                                agenda_3.consultant_name = ParseDataHelper.ReportEmptyValue(reader["consultant_name"].ToString());
                                agenda_3.comment_1_name = "1. " + ParseDataHelper.ReportEmptyValue(reader["comment_1_title"].ToString());
                                agenda_3.comment_2_name = "2. " + ParseDataHelper.ReportEmptyValue(reader["comment_2_title"].ToString());
                                agenda_3.comment_3_name = "3. " + ParseDataHelper.ReportEmptyValue(reader["comment_3_title"].ToString());
                                agenda_3.detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["agenda_3_suggestion"].ToString());

                                agenda_3.list_agenda_3_2 = new List<model_list_agenda_3_2>();

                                if (!string.IsNullOrEmpty(reader["sequel_1_title"].ToString()))
                                {
                                    model_list_agenda_3_2 item_3_2_1 = new model_list_agenda_3_2()
                                    {
                                        sequel_title = "3.2.1 " + ParseDataHelper.ReportEmptyValue(reader["sequel_1_title"].ToString()),
                                        sequel_detail_summary = ParseDataHelper.ReportEmptyValue(reader["sequel_1_summary"].ToString()),
                                        sequel_detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["sequel_1_note"].ToString()),
                                    };
                                    agenda_3.list_agenda_3_2.Add(item_3_2_1);

                                    if (!string.IsNullOrEmpty(reader["sequel_2_title"].ToString()))
                                    {
                                        model_list_agenda_3_2 item_3_2_2 = new model_list_agenda_3_2()
                                        {
                                            sequel_title = "3.2.2 " + ParseDataHelper.ReportEmptyValue(reader["sequel_2_title"].ToString()),
                                            sequel_detail_summary = ParseDataHelper.ReportEmptyValue(reader["sequel_2_summary"].ToString()),
                                            sequel_detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["sequel_2_note"].ToString()),
                                        };
                                        agenda_3.list_agenda_3_2.Add(item_3_2_2);
                                    }

                                    if (!string.IsNullOrEmpty(reader["sequel_3_title"].ToString()))
                                    {
                                        model_list_agenda_3_2 item_3_2_3 = new model_list_agenda_3_2()
                                        {
                                            sequel_title = "3.2.3 " + ParseDataHelper.ReportEmptyValue(reader["sequel_3_title"].ToString()),
                                            sequel_detail_summary = ParseDataHelper.ReportEmptyValue(reader["sequel_3_summary"].ToString()),
                                            sequel_detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["sequel_3_note"].ToString()),
                                        };
                                        agenda_3.list_agenda_3_2.Add(item_3_2_3);
                                    }

                                }
                                else agenda_3.list_agenda_3_2.Add(new model_list_agenda_3_2());


                                if (!string.IsNullOrEmpty(reader["member_project_1"].ToString()))
                                    agenda_3.list_researchers = await GetResearchOfProject(reader);

                                rptData.list_agenda_3.Add(agenda_3);
                                seq++;
                            }
                        }
                        else rptData.list_agenda_3.Add(new model_list_agenda_3());

                        reader.Close();

                    }

                    // ระเบียบวารที่ 4
                    rptData.list_agenda_4_1 = await GetAgenda4Type1To4(conn, doc_id, 1);
                    rptData.list_agenda_4_2 = await GetAgenda4Type1To4(conn, doc_id, 2);
                    rptData.list_agenda_4_3 = await GetAgenda4Type1To4(conn, doc_id, 3);
                    rptData.list_agenda_4_4 = await GetAgenda4Type1To4(conn, doc_id, 4);
                    rptData.list_agenda_4_5 = await GetAgenda4Type5To9(conn, doc_id, 5);
                    rptData.list_agenda_4_6 = await GetAgenda4Type5To9(conn, doc_id, 6);
                    rptData.list_agenda_4_7 = await GetAgenda4Type5To9(conn, doc_id, 7);
                    rptData.list_agenda_4_8 = await GetAgenda4Type5To9(conn, doc_id, 8);
                    rptData.list_agenda_4_9 = await GetAgenda4Type5To9(conn, doc_id, 9);

                    // ระเบียบวารที่ 5
                    using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                string seq = reader["seq"].ToString();
                                string title = "ระเบียบวาระที่ 5. " + seq + " ";

                                model_list_agenda_5 agenda_5 = new model_list_agenda_5()
                                {
                                    subject = title + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString()),
                                    detail_summary = ParseDataHelper.ReportEmptyValue(reader["input2"].ToString()),
                                    detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["input3"].ToString()),
                                };
                                if (reader["group_data"].ToString() == "5.1")
                                    rptData.list_agenda_5.Add(agenda_5);

                            }
                        }
                        else rptData.list_agenda_5.Add(new model_list_agenda_5());

                        reader.Close();
                    }

                    conn.Close();
                }

                rptR14 rpt14 = new rptR14();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R14_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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

        private async Task<IList<model_list_agenda_4>> GetAgenda4Type1To4(SqlConnection conn, int doc_id, int safety_type)
        {
            IList<model_list_agenda_4> list_agenda_4 = new List<model_list_agenda_4>();

            using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_4_type_1_4", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;
                cmd.Parameters.Add("@safety_type", SqlDbType.Int).Value = safety_type;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                string title = "โครงการวิจัยใหม่ที่เข้าข่ายการพิจารณางานประเภทที่ " + safety_type + " จำนวน ";

                if (reader.HasRows)
                {
                    int seq = 0;
                    while (await reader.ReadAsync())
                    {
                        model_list_agenda_4 agenda_4 = new model_list_agenda_4();

                        agenda_4.title = title + ParseDataHelper.ReportEmptyValueInt(Convert.ToInt32(reader["count_4"])) + " โครงการ ดังนี้";
                        agenda_4.subject = (seq + 1).ToString() + ". เรื่อง :";
                        agenda_4.project_number = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_number"].ToString());
                        agenda_4.project_name_thai = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_name_1"].ToString());
                        agenda_4.project_name_eng = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_name_2"].ToString());
                        agenda_4.project_safety_type = ParseDataHelper.ReportEmptyValue(reader["safety_type"].ToString());
                        agenda_4.consultant_name = ParseDataHelper.ReportEmptyValue(reader["consultant_name"].ToString());
                        agenda_4.comment_1_name = "1. " + ParseDataHelper.ReportEmptyValue(reader["comment_1_title"].ToString());
                        agenda_4.comment_2_name = "2. " + ParseDataHelper.ReportEmptyValue(reader["comment_2_title"].ToString());
                        agenda_4.comment_3_name = "3. " + ParseDataHelper.ReportEmptyValue(reader["comment_3_title"].ToString());
                        agenda_4.detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["agenda_4_suggestion"].ToString());

                        if (!string.IsNullOrEmpty(reader["member_project_1"].ToString()))
                            agenda_4.list_researchers = await GetResearchOfProject(reader);

                        list_agenda_4.Add(agenda_4);
                        seq++;
                    }
                }
                else list_agenda_4.Add(new model_list_agenda_4() { title = title + "0 โครงการ ดังนี้" });

                reader.Close();
            }

            return list_agenda_4;
        }

        private async Task<IList<model_list_agenda_4>> GetAgenda4Type5To9(SqlConnection conn, int doc_id, int type)
        {
            IList<model_list_agenda_4> list_agenda_4 = new List<model_list_agenda_4>();

            using (SqlCommand cmd = new SqlCommand("sp_report_14_detail_4_type_5_9", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                string title = "";

                if (type == 5) title = "โครงการวิจัยที่แจ้งขอต่ออายุใบรับรอง จำนวน ";
                if (type == 6) title = "โครงการวิจัยที่ขอแก้ไขโครงการหลังผ่านการรับรองแล้ว จำนวน ";
                if (type == 7) title = "โครงการวิจัยที่ขอแจ้งปิดโครงการ จำนวน ";
                if (type == 8) title = "คําขอประเมิณห้องปฏิบัติการ จำนวน ";
                if (type == 9) title = "ผลการตรวจเยี่ยมติดตามโครงการ จำนวน ";

                if (reader.HasRows)
                {
                    int seq = 0;
                    while (await reader.ReadAsync())
                    {

                        model_list_agenda_4 agenda_4 = new model_list_agenda_4();

                        agenda_4.title = title + ParseDataHelper.ReportEmptyValueInt(Convert.ToInt32(reader["count_4"])) + " โครงการ ดังนี้";
                        agenda_4.subject = (seq + 1).ToString() + ". เรื่อง :";
                        agenda_4.project_number = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_number"].ToString());
                        agenda_4.project_name_thai = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_name_1"].ToString());
                        agenda_4.project_name_eng = ParseDataHelper.ReportEmptyValue(reader["agenda_4_project_name_2"].ToString());
                        agenda_4.project_safety_type = ParseDataHelper.ReportEmptyValue(reader["safety_type"].ToString());
                        agenda_4.consultant_name = ParseDataHelper.ReportEmptyValue(reader["consultant_name"].ToString());
                        agenda_4.comment_1_name = "1. " + ParseDataHelper.ReportEmptyValue(reader["comment_1_title"].ToString());
                        agenda_4.comment_2_name = "2. " + ParseDataHelper.ReportEmptyValue(reader["comment_2_title"].ToString());
                        agenda_4.comment_3_name = "3. " + ParseDataHelper.ReportEmptyValue(reader["comment_3_title"].ToString());
                        agenda_4.detail_conclusion = ParseDataHelper.ReportEmptyValue(reader["agenda_4_suggestion"].ToString());

                        if (!string.IsNullOrEmpty(reader["member_project_1"].ToString()))
                            agenda_4.list_researchers = await GetResearchOfProject(reader);

                        list_agenda_4.Add(agenda_4);
                        seq++;
                    }
                }
                else list_agenda_4.Add(new model_list_agenda_4() { title = title + "0 โครงการ ดังนี้" });

                reader.Close();
            }

            return list_agenda_4;
        }

        private async Task<string> GetResearchOfProject(SqlDataReader reader)
        {
            string list_researchers = "";

            var member1json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_1"].ToString());
            if (member1json != null)
            {
                var member1 = await GetRegisterInforAsync(member1json.projecthead);
                if (member1 != null && member1.Count > 0)
                    list_researchers += ("1. " + member1[0].name + Environment.NewLine);
            }

            var member2json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_2"].ToString());
            if (member2json != null)
            {
                var member2 = await GetRegisterInforAsync(member2json.projecthead);
                if (member2 != null && member2.Count > 0)
                    list_researchers += ("2. " + member2[0].name + Environment.NewLine);
            }

            var member3json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_3"].ToString());
            if (member3json != null)
            {
                var member3 = await GetRegisterInforAsync(member3json.projecthead);
                if (member3 != null && member3.Count > 0)
                    list_researchers += ("3. " + member3[0].name + Environment.NewLine);

            }

            var member4json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_4"].ToString());
            if (member4json != null)
            {
                var member4 = await GetRegisterInforAsync(member4json.projecthead);
                if (member4 != null && member4.Count > 0)
                    list_researchers += ("4. " + member4[0].name + Environment.NewLine);
            }

            var member5json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_5"].ToString());
            if (member5json != null)
            {
                var member5 = await GetRegisterInforAsync(member5json.projecthead);
                if (member5 != null && member5.Count > 0)
                    list_researchers += ("5. " + member5[0].name + Environment.NewLine);
            }

            var member6json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_6"].ToString());
            if (member6json != null)
            {
                var member6 = await GetRegisterInforAsync(member6json.projecthead);
                if (member6 != null && member6.Count > 0)
                    list_researchers += ("6. " + member6[0].name + Environment.NewLine);
            }

            var member7json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_7"].ToString());
            if (member7json != null)
            {
                var member7 = await GetRegisterInforAsync(member7json.projecthead);
                if (member7 != null && member7.Count > 0)
                    list_researchers += ("7. " + member7[0].name + Environment.NewLine);
            }

            var member8json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_8"].ToString());
            if (member8json != null)
            {
                var member8 = await GetRegisterInforAsync(member8json.projecthead);
                if (member8 != null && member8.Count > 0)
                    list_researchers += ("8. " + member8[0].name + Environment.NewLine);
            }

            var member9json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_9"].ToString());
            if (member9json != null)
            {
                var member9 = await GetRegisterInforAsync(member9json.projecthead);
                if (member9 != null && member9.Count > 0)
                    list_researchers += ("9. " + member9[0].name + Environment.NewLine);
            }

            var member10json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_10"].ToString());
            if (member10json != null)
            {
                var member10 = await GetRegisterInforAsync(member10json.projecthead);
                if (member10 != null && member10.Count > 0)
                    list_researchers += ("10. " + member10[0].name + Environment.NewLine);
            }

            var member11json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_11"].ToString());
            if (member11json != null)
            {
                var member11 = await GetRegisterInforAsync(member11json.projecthead);
                if (member11 != null && member11.Count > 0)
                    list_researchers += ("11. " + member11[0].name + Environment.NewLine);
            }

            var member12json = JsonConvert.DeserializeObject<model_personal>(reader["member_project_12"].ToString());
            if (member12json != null)
            {
                var member12 = await GetRegisterInforAsync(member12json.projecthead);
                if (member12 != null && member12.Count > 0)
                    list_researchers += ("12. " + member12[0].name + Environment.NewLine);
            }
            return list_researchers;
        }

        // Report 15 -----------------------------------------------------------------------------

        public async Task<model_rpt_15_file> GetReportR15Async(int doc_id)
        {
            model_rpt_15_file resp = new model_rpt_15_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                model_rpt_15_report rptData = new model_rpt_15_report();

                //Default ----------------------------------------------------------
                rptData.Doc_head_1 = "ระเบียบวารการประชุมคณะกรรมการพิจารณาฯความปลอดภัยทาชีวะภาพ มหาวิทยาลัยนเรศวร";
                rptData.Doc_head_2 = "ครั้งที่ - ";
                rptData.Doc_head_3 = "วันที่ -";
                rptData.Doc_head_4 = "เวลา -";
                rptData.Doc_head_5 = "สถานที่ -";

                rptData.subject_1_1 = "1.1 เรื่องที่ประธานแจ้งให้ที่ประชุมทราบ";
                rptData.item_1_1_1 = "1.1.1 -";
                rptData.item_1_1_2 = "1.1.2 -";
                rptData.item_1_1_3 = "1.1.3 -";

                rptData.subject_1_2 = "1.2 เรื่องที่ฝ่ายเลขานุการแจ้งให้ที่ประชุมทราบ";
                rptData.item_1_2_1 = "1.2.1 -";
                rptData.item_1_2_2 = "1.2.2 -";
                rptData.item_1_2_3 = "1.2.3 -";

                rptData.item_2_1_1 = "2.1 -";
                rptData.item_2_1_2 = "2.2 -";
                rptData.item_2_1_3 = "2.3 -";

                rptData.subject_3_1 = "3.1 โครงการวิจัยที่รับรองหลังจากปรับปรุง/แก้ไข";
                rptData.subject_3_1_qty = "จำนวน " + 0 + " โครงการ";
                rptData.subject_3_2 = "3.2 เรื่องสืบเนื่อง";
                rptData.item_3_2_1 = "3.2.1 -";
                rptData.item_3_2_2 = "3.2.2 -";
                rptData.item_3_2_3 = "3.2.3 -";

                rptData.item_4_1_1 = "4.1 โครงการวิจัยใหม่ที่เข้าข่ายการพิจารณางานประเภทที่ 1";
                rptData.item_4_1_2 = "4.2 โครงการวิจัยใหม่ที่เข้าข่ายการพิจารณางานประเภทที่ 2";
                rptData.item_4_1_3 = "4.3 โครงการวิจัยใหม่ที่เข้าข่ายการพิจารณางานประเภทที่ 3";
                rptData.item_4_1_4 = "4.4 โครงการวิจัยใหม่ที่เข้าข่ายการพิจารณางานประเภทที่ 4";
                rptData.item_4_1_5 = "4.5 โครงการวิจัยที่แจ้งขอต่ออายุใบรับรอง";
                rptData.item_4_1_6 = "4.6 โครงการวิจัยที่ขอแก้ไขโครงการหลังผ่านการรับรองแล้ว";
                rptData.item_4_1_7 = "4.7 โครงการวิจัยที่ขอแจ้งปิดโครงการ";
                rptData.item_4_1_8 = "4.8 คำขอประเมิณห้องปฏิบัติการ";
                rptData.item_4_1_9 = "4.9 ผลการตรวจเยี่ยมติดตามโครงการ";

                rptData.item_4_1_1_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_2_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_3_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_4_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_5_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_6_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_7_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_8_qty = "จำนวน " + 0 + " โครงการ";
                rptData.item_4_1_9_qty = "จำนวน " + 0 + " โครงการ";

                rptData.item_5_1_1 = "5.1 -";
                rptData.item_5_1_2 = "5.2 -";
                rptData.item_5_1_3 = "5.3 -";

                //--------------------------------------------------------------------

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // ส่วนหัวเอกสาร
                    using (SqlCommand cmd = new SqlCommand("sp_report_15", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@doc_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData.Doc_head_1 = reader["meeting_type_name"].ToString() + " มหาวิทยาลัยนเรศวร";
                                rptData.Doc_head_2 = "ครั้งที่ " + reader["meeting_round"].ToString() + "/" + reader["year_of_meeting"].ToString();

                                int year_meeting = Convert.ToInt32(Convert.ToDateTime(reader["meeting_date"]).ToString("yyyy"));
                                rptData.Doc_head_3 = "วัน" + Convert.ToDateTime(reader["meeting_date"]).ToString("dddd dd MMM ") + CommonData.ConvertYearToThai(year_meeting);

                                rptData.Doc_head_4 = "เวลา " + ParseDataHelper.ReportEmptyValue(reader["meeting_start"].ToString()) + " เป็นต้นไป";
                                rptData.Doc_head_5 = ParseDataHelper.ReportEmptyValue(reader["meeting_location"].ToString());

                            }
                        }
                        reader.Close();
                    }

                    // ระเบียบวารที่ 1
                    using (SqlCommand cmd = new SqlCommand("sp_report_15_detail_1", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                string string_value = ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                rptData.subject_1_1 = "1.1 เรื่องที่ประธานแจ้งให้ที่ประชุมทราบ";

                                if (reader["group_data"].ToString() == "1.1")
                                {
                                    if (reader["seq"].ToString() == "1")
                                        rptData.item_1_1_1 = "1.1.1 " + string_value;

                                    if (reader["seq"].ToString() == "2")
                                        rptData.item_1_1_2 = "1.1.2 " + string_value;

                                    if (reader["seq"].ToString() == "3")
                                        rptData.item_1_1_3 = "1.1.3 " + string_value;
                                }

                                rptData.subject_1_2 = "1.2 เรื่องที่ฝ่ายเลขานุการแจ้งให้ที่ประชุมทราบ";
                                if (reader["group_data"].ToString() == "1.2")
                                {
                                    if (reader["seq"].ToString() == "1")
                                        rptData.item_1_2_1 = "1.2.1 " + string_value;

                                    if (reader["seq"].ToString() == "2")
                                        rptData.item_1_2_2 = "1.2.2 " + string_value;

                                    if (reader["seq"].ToString() == "3")
                                        rptData.item_1_2_3 = "1.2.3 " + string_value;
                                }
                            }
                        }
                        reader.Close();
                    }

                    // ระเบียบวารที่ 2
                    using (SqlCommand cmd = new SqlCommand("sp_report_15_detail_2", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["group_data"].ToString() == "2.1")
                                {
                                    if (reader["seq"].ToString() == "1")
                                        rptData.item_2_1_1 = "2.1 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "2")
                                        rptData.item_2_1_2 = "2.2 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "3")
                                        rptData.item_2_1_3 = "2.3 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());
                                }
                            }
                        }
                        reader.Close();



                    }

                    // ระเบียบวารที่ 3
                    using (SqlCommand cmd = new SqlCommand("sp_report_15_detail_3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                rptData.subject_3_1 = "3.1 โครงการวิจัยที่รับรองหลังจากปรับปรุง/แก้ไข";
                                if (reader["group_data"].ToString() == "3.1")
                                {
                                    rptData.subject_3_1_qty = "จำนวน " + ParseDataHelper.ReportEmptyValueInt(Convert.ToInt32(reader["count_3"])) + " โครงการ";
                                }

                                rptData.subject_3_2 = "3.2 เรื่องสืบเนื่อง";
                                if (reader["group_data"].ToString() == "3.2")
                                {
                                    if (reader["seq"].ToString() == "1")
                                        rptData.item_3_2_1 = "3.2.1 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "2")
                                        rptData.item_3_2_2 = "3.2.2 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "3")
                                        rptData.item_3_2_3 = "3.2.3 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());
                                }
                            }
                        }
                        reader.Close();
                    }

                    // ระเบียบวารที่ 4
                    using (SqlCommand cmd = new SqlCommand("sp_report_15_detail_4", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                string string_qty = "จำนวน " + ParseDataHelper.ReportEmptyValueInt(Convert.ToInt32(reader["count_4"])) + " โครงการ";

                                switch (reader["agenda_4_term"].ToString())
                                {
                                    case "1":
                                        rptData.item_4_1_1_qty = string_qty;
                                        break;
                                    case "2":
                                        rptData.item_4_1_2_qty = string_qty;
                                        break;
                                    case "3":
                                        rptData.item_4_1_3_qty = string_qty;
                                        break;
                                    case "4":
                                        rptData.item_4_1_4_qty = string_qty;
                                        break;
                                    case "5":
                                        rptData.item_4_1_5_qty = string_qty;
                                        break;
                                    case "6":
                                        rptData.item_4_1_6_qty = string_qty;
                                        break;
                                    case "7":
                                        rptData.item_4_1_7_qty = string_qty;
                                        break;
                                    case "8":
                                        rptData.item_4_1_8_qty = string_qty;
                                        break;
                                    case "9":
                                        rptData.item_4_1_9_qty = string_qty;
                                        break;
                                }
                            }
                        }
                        reader.Close();
                    }

                    // ระเบียบวารที่ 5
                    using (SqlCommand cmd = new SqlCommand("sp_report_15_detail_5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = doc_id;

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["group_data"].ToString() == "5.1")
                                {
                                    if (reader["seq"].ToString() == "1")
                                        rptData.item_5_1_1 = "5.1 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "2")
                                        rptData.item_5_1_2 = "5.2 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());

                                    if (reader["seq"].ToString() == "3")
                                        rptData.item_5_1_3 = "5.3 " + ParseDataHelper.ReportEmptyValue(reader["input1"].ToString());
                                }
                            }
                        }
                        reader.Close();
                    }

                    conn.Close();
                }

                rptR15 rpt15 = new rptR15();

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Constructor = new ObjectConstructorInfo();
                dataSource.DataSource = rptData;

                string report_name = "R15_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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
            string report_no = await CreateReportNumberAsync(2563, "R8", doc_id, "B1");

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
                                rptData.assessment_agent = "";
                                rptData.labLocation = reader["build_location"].ToString();
                                rptData.labboy_name = reader["responsible_person"].ToString();
                                rptData.room_no = reader["room_tel"].ToString();
                                rptData.assessment_date = Convert.ToDateTime(reader["doc_date"]).ToString("dd/MM/yyyy");
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
                dataSource.DataSource = rptData;

                string report_name = "R17_18_" + doc_id + DateTime.Now.ToString("_yyyyMMddHHmmss").ToString() + ".pdf";
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

        public async Task<IList<ModelMenuR1RegisterInfo>> GetRegisterInforAsync(string user_id)
        {

            string sql = "SELECT A.register_id,A.email, " +
                        "(A.first_name + A.full_name) as full_name, B.name_thai as position, C.name_thai as faculty " +
                        "FROM RegisterUser A " +
                        "LEFT OUTER JOIN MST_Position B ON A.position = B.id " +
                        "LEFT OUTER JOIN MST_Faculty C ON A.faculty = C.id " +
                        "WHERE 1=1 ";

            if (!string.IsNullOrEmpty(user_id))
            {
                string userid = Encoding.UTF8.GetString(Convert.FromBase64String(user_id));

                sql += " AND A.register_id ='" + userid + "'";
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
                            e.department = reader["faculty"].ToString();
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

        // รายงานหลังจากบันทึกมติที่ประชุมครบเรียบร้อย -----------------------------------------------------

        public async Task<model_rpt_meeting_file> GetAllReportMeetingAsync(int doc_id)
        {
            model_rpt_meeting_file resp = new model_rpt_meeting_file();

            var cultureInfo = new CultureInfo("th-TH");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {

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

        private async Task<string> CreateReportNumberAsync(int year, string report_type, int doc_id, string doc_type)
        {

            string report_no = "-";

            switch (doc_type)
            {
                case "B1":
                case "C1":
                case "C3":
                case "C4":
                case "A6":
                    goto GenerateReportNo;
                default:
                    break;
            }

            switch (report_type)
            {
                case "R8":
                case "R11":
                case "R12":
                case "R13":
                case "R7":
                    goto GenerateReportNo;
                default:
                    break;
            }

            GenerateReportNo:
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_create_report_number", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@report_year", SqlDbType.Int).Value = year;
                    cmd.Parameters.Add("@report_type", SqlDbType.VarChar, 20).Value = report_type;
                    cmd.Parameters.Add("@doc_id", SqlDbType.Int).Value = doc_id;
                    cmd.Parameters.Add("@doc_type", SqlDbType.VarChar, 20).Value = doc_type;

                    SqlParameter rStatus = cmd.Parameters.Add("@rStatus", SqlDbType.Int);
                    rStatus.Direction = ParameterDirection.Output;
                    SqlParameter rMessage = cmd.Parameters.Add("@rMessage", SqlDbType.NVarChar, 500);
                    rMessage.Direction = ParameterDirection.Output;
                    SqlParameter rDocId = cmd.Parameters.Add("@rReportNo", SqlDbType.VarChar, 10);
                    rDocId.Direction = ParameterDirection.Output;

                    await cmd.ExecuteNonQueryAsync();

                    if ((int)cmd.Parameters["@rStatus"].Value > 0)
                    {
                        report_no = (string)cmd.Parameters["@rReportNo"].Value;
                    }
                }
                conn.Close();
            }

            return report_no;
        }
    }

}
