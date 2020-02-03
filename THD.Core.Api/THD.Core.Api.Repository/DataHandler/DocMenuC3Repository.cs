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
using THD.Core.Api.Models.ReportModels;

namespace THD.Core.Api.Repository.DataHandler
{
    public class DocMenuC3Repository : IDocMenuC3Repository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly IDropdownListRepository _IDropdownListRepository;
        private readonly IRegisterUserRepository _IRegisterUserRepository;
        private readonly IDocMeetingRoundRepository _IDocMeetingRoundRepository;
        private readonly IDocMenuReportRepository _IDocMenuReportRepository;

        public DocMenuC3Repository(
            IConfiguration configuration,
            IDropdownListRepository DropdownListRepository,
            IRegisterUserRepository IRegisterUserRepository,
            IDocMenuReportRepository DocMenuReportRepository,
            IDocMeetingRoundRepository DocMeetingRoundRepository)
        {
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));
            _IDropdownListRepository = DropdownListRepository;
            _IRegisterUserRepository = IRegisterUserRepository;
            _IDocMeetingRoundRepository = DocMeetingRoundRepository;
            _IDocMenuReportRepository = DocMenuReportRepository;
        }



        // บันทึกการประชุม ------------------------------------------------------------------------------
        public async Task<ModelMenuC3_InterfaceData> MenuC3InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC3_InterfaceData resp = new ModelMenuC3_InterfaceData();

            //คณะกรรมการ
            resp.ListCommittees = new List<ModelSelectOption>();

            resp.ListCommittees = await GetAllCommitteesAsync();

            //ผู้ร่วมประชุม
            resp.ListAttendees = new List<ModelSelectOption>();

            resp.ListAttendees = await GetAllAttendeesAsync();

            int thai_year = CommonData.GetYearOfPeriod();

            resp.ListYearOfProject = new List<ModelSelectOption>();
            //ModelSelectOption year_current = new ModelSelectOption();
            //year_current.value = (thai_year).ToString();
            //year_current.label = (thai_year).ToString();
            //resp.defaultyear = (thai_year);
            //resp.ListYearOfProject.Add(year_current);

            //for (int i = 1; i < 5; i++)
            //{
            //    ModelSelectOption year_next = new ModelSelectOption();
            //    year_next.value = (thai_year + i).ToString();
            //    year_next.label = (thai_year + i).ToString();
            //    resp.ListYearOfProject.Add(year_next);
            //}

            resp.ListYearOfProject = await GetListYearOfC3Async();
            ModelSelectOption year_current = new ModelSelectOption() { value = "", label = "" };
            resp.ListYearOfProject.Add(year_current);
            resp.defaultyear = (resp.ListYearOfProject != null) ? resp.ListYearOfProject[0].value : "";

            ModelCountOfYear count_of_year = new ModelCountOfYear();
            count_of_year = await _IDocMeetingRoundRepository.GetMeetingRoundOfProjectAsync(Convert.ToInt32(resp.defaultyear));
            resp.defaultround = count_of_year.count.ToString();
            resp.defaultmeetingdate = "16/" + DateTime.Now.ToString("MM/yyyy");

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M013");

            return resp;
        }

        public async Task<IList<ModelSelectOption>> GetAllCommitteesAsync()
        {

            string sql = "SELECT register_id, first_name, full_name FROM RegisterUser WHERE IsActive='1' AND Character IN ('2') ORDER BY full_name ASC";

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

        public async Task<IList<ModelSelectOption>> GetAllAttendeesAsync()
        {

            string sql = "SELECT register_id, first_name, full_name FROM RegisterUser WHERE IsActive='1' AND Character IN ('2') ORDER BY full_name ASC";

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

        public async Task<IList<ModelMenuC3_History>> GetAllHistoryDataC3Async()
        {

            string sql = "SELECT doc_id,year_of_meeting,meeting_round,B.name_thai, " +
                        "meeting_date, meeting_location,committees_array " +
                        "FROM Doc_MenuC3 A " +
                        "LEFT OUTER JOIN MST_MeetingRecordType B " +
                        "ON A.meeting_record_id = B.id " +
                        "ORDER BY doc_id DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelMenuC3_History> e = new List<ModelMenuC3_History>();
                        while (await reader.ReadAsync())
                        {
                            ModelMenuC3_History item = new ModelMenuC3_History();
                            item.docid = reader["doc_id"].ToString();
                            item.meetingdate = Convert.ToDateTime(reader["meeting_date"]).ToString("dd/MM/yyyy");
                            item.meetinglocation = reader["meeting_location"].ToString();
                            item.meetingrecordname = reader["name_thai"].ToString();
                            item.meetinground = reader["meeting_round"].ToString();
                            item.yearofmeeting = reader["year_of_meeting"].ToString();
                            item.committeesarray = "";
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;


        }

        private async Task<IList<ModelSelectOption>> GetListYearOfC3Async()
        {
            string sql = "SELECT meeting_year " +
                        "FROM Doc_MeetingRound_Project " +
                        "WHERE isClose = 0 " +
                        "ORDER BY meeting_round ASC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelSelectOption> e = new List<ModelSelectOption>();
                        while (await reader.ReadAsync())
                        {
                            ModelSelectOption item = new ModelSelectOption();
                            item.value = reader["meeting_year"].ToString();
                            item.label = reader["meeting_year"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelCountOfYearC3> GetDefaultRoundC3Async(int yearof)
        {
            ModelCountOfYearC3 rest = new ModelCountOfYearC3() { count = "" };

            string sql = "SELECT round_of_meeting, meeting_date " +
                         "FROM Doc_MenuC1 " +
                         "WHERE round_of_closed=0 " +
                         "AND year_of_meeting='" + yearof + "'" +
                         "GROUP BY round_of_meeting, meeting_date ";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            rest.count = reader["round_of_meeting"].ToString();
                            rest.meetingdate = Convert.ToDateTime(reader["meeting_date"]).ToString("dd/MM/yyyy");
                        }
                    }
                }
                conn.Close();
            }
            return rest;

        }

        public async Task<ModelResponseMessage> AddDocMenuC3Async(ModelMenuC3 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_date", SqlDbType.DateTime).Value = Convert.ToDateTime(model.meetingdate);
                        cmd.Parameters.Add("@meeting_record_id", SqlDbType.Int).Value = model.meetingrecordid;
                        cmd.Parameters.Add("@meeting_round", SqlDbType.Int).Value = model.meetinground;
                        cmd.Parameters.Add("@year_of_meeting", SqlDbType.Int).Value = model.yearofmeeting;
                        cmd.Parameters.Add("@meeting_location", SqlDbType.NVarChar).Value = ParseDataHelper.ConvertDBNull(model.meetinglocation);
                        cmd.Parameters.Add("@meeting_start", SqlDbType.NVarChar).Value = ParseDataHelper.ConvertDBNull(model.meetingstart);
                        cmd.Parameters.Add("@meeting_close", SqlDbType.NVarChar).Value = ParseDataHelper.ConvertDBNull(model.meetingclose);
                        cmd.Parameters.Add("@committees_array", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.committeesarray);
                        cmd.Parameters.Add("@attendees_array", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model.attendeesarray);

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
                            resp.DocId = (int)cmd.Parameters["@rDocId"].Value;

                            model_rpt_15_file rpt = await _IDocMenuReportRepository.GetReportR15Async((int)cmd.Parameters["@rDocId"].Value);

                            resp.filename = rpt.filename;
                            resp.filebase64 = rpt.filebase64;
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


        // พิมพ์ร่างและปิดการประชุม -------------------------------------------------------------------------

        public async Task<ModelResponseMessage> CloseMeetingAsync(ModelCloseMeeting model)
        {
            ModelResponseMessage resp = new ModelResponseMessage();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_close_meeting", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@YearOfClose", SqlDbType.Int).Value = model.meetingofyear;
                        cmd.Parameters.Add("@RoundOfClose", SqlDbType.Int).Value = model.meetingofround;

                        int current_year = CommonData.GetYearOfPeriod();
                        cmd.Parameters.Add("@YearOfNew", SqlDbType.Int).Value = current_year;

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





        // ระเบียบวาระที่ 1 ------------------------------------------------------------------------------
        public async Task<ModelMenuC31_InterfaceData> MenuC31InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC31_InterfaceData resp = new ModelMenuC31_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            if (resp.ListMeetingId != null && resp.ListMeetingId.Count > 0)
            {
                resp.meetingId = resp.ListMeetingId.FirstOrDefault().value;
                resp.meetingName = resp.ListMeetingId.FirstOrDefault().label;
            }

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M014");

            return resp;
        }

        public async Task<ModelResponseMessage> AddDocMenuC31Async(ModelMenuC31 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3_1", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = model.meetingid;

                        //Tab 1 Group All
                        IList<ModelMenuC31Tab1GroupAll> list_Tab1_group_all = new List<ModelMenuC31Tab1GroupAll>();

                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq1Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq2Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq3Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq4Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq5Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq6Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq6Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq6Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq6Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 7:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq7Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq7Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq7Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq7Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 8:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq8Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq8Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq8Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq8Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 9:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq9Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq9Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq9Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq9Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 10:
                                    if (!string.IsNullOrEmpty(model.tab1Group1Seq10Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq10Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq10Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group1Seq10Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }

                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq1Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq2Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq3Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq4Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq5Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq6Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq6Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq6Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq6Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 7:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq7Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq7Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq7Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq7Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 8:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq8Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq8Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq8Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq8Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 9:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq9Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq9Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq9Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq9Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 10:
                                    if (!string.IsNullOrEmpty(model.tab1Group2Seq10Input1))
                                    {
                                        list_Tab1_group_all.Add(new ModelMenuC31Tab1GroupAll
                                        {
                                            groupdata = "1.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq10Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq10Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab1Group2Seq10Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }

                        string tab_1_group_all_json = JsonConvert.SerializeObject(list_Tab1_group_all);

                        cmd.Parameters.Add("@tab_1_group_all_json", SqlDbType.VarChar).Value = tab_1_group_all_json;

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



        // ระเบียบวาระที่ 2 ------------------------------------------------------------------------------
        public async Task<ModelMenuC32_InterfaceData> MenuC32InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC32_InterfaceData resp = new ModelMenuC32_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            resp.isFileAttachment = false;

            if (resp.ListMeetingId != null && resp.ListMeetingId.Count > 0)
            {
                resp.meetingId = resp.ListMeetingId.FirstOrDefault().value;
                resp.meetingName = resp.ListMeetingId.FirstOrDefault().label;
                resp.isFileAttachment = await MenuC32CheckAttachmentAsync(Convert.ToInt32(resp.ListMeetingId.FirstOrDefault().value));
            }

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M015");

            return resp;
        }

        public async Task<bool> MenuC32CheckAttachmentAsync(int meetingid)
        {
            string sql = "SELECT COUNT(input2) AS IsAttachment FROM Doc_MenuC3_Tab2 WHERE meeting_id = '" + meetingid + "'";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            if ((int)reader["IsAttachment"] >= 1) return true;
                        }
                    }
                }
                conn.Close();
            }
            return false;
        }

        public async Task<ModelMenuC32_DownloadFileName> MenuC32DownloadAttachmentNameAsync(int meetingid)
        {

            string sql = "SELECT id,input2 FROM Doc_MenuC3_Tab2 WHERE meeting_id='" + meetingid + "' ORDER BY id ASC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuC32_DownloadFileName e = new ModelMenuC32_DownloadFileName();
                        int rows = 1;
                        while (await reader.ReadAsync())
                        {
                            if (rows == 1 && !string.IsNullOrEmpty(reader["input2"].ToString()))
                            {
                                e.file1name = reader["input2"].ToString();
                            }
                            if (rows == 2 && !string.IsNullOrEmpty(reader["input2"].ToString()))
                            {
                                e.file2name = reader["input2"].ToString();
                            }
                            if (rows == 3 && !string.IsNullOrEmpty(reader["input2"].ToString()))
                            {
                                e.file3name = reader["input2"].ToString();
                            }
                            rows += 1;
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<ModelResponseMessage> AddDocMenuC32Async(ModelMenuC32 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3_2", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = model.meetingid;

                        IList<ModelMenuC32Tab2Group1> list_Tab2_group_1 = new List<ModelMenuC32Tab2Group1>();
                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab2Group1Seq1Input1))
                                    {
                                        list_Tab2_group_1.Add(new ModelMenuC32Tab2Group1
                                        {
                                            groupdata = "2.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq1FileInput2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq1Input3).ToString(),
                                            input4 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq1Input4).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab2Group1Seq2Input1))
                                    {
                                        list_Tab2_group_1.Add(new ModelMenuC32Tab2Group1
                                        {
                                            groupdata = "2.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq2FileInput2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq2Input3).ToString(),
                                            input4 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq2Input4).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab2Group1Seq3Input1))
                                    {
                                        list_Tab2_group_1.Add(new ModelMenuC32Tab2Group1
                                        {
                                            groupdata = "2.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq3FileInput2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq3Input3).ToString(),
                                            input4 = ParseDataHelper.ConvertDBNull(model.tab2Group1Seq3Input4).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }
                        string tab_2_group_1_json = JsonConvert.SerializeObject(list_Tab2_group_1);

                        cmd.Parameters.Add("@tab_2_group_1_json", SqlDbType.VarChar).Value = tab_2_group_1_json;

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



        // ระเบียบวาระที่ 3 ------------------------------------------------------------------------------
        public async Task<ModelMenuC33_InterfaceData> MenuC33InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC33_InterfaceData resp = new ModelMenuC33_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            if (resp.ListMeetingId != null && resp.ListMeetingId.Count > 0)
            {
                resp.meetingId = resp.ListMeetingId.FirstOrDefault().value;
                resp.meetingName = resp.ListMeetingId.FirstOrDefault().label;
            }

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M016");

            if (resp.UserPermission != null && resp.UserPermission.alldata == true)
            {
                resp.ListProjectNumberTab3 = await GetAllProjectForC3Tab3Async("", "C2");
            }
            else
            {
                resp.ListProjectNumberTab3 = await GetAllProjectForC3Tab3Async(RegisterId, "C2");
            }

            return resp;
        }

        public async Task<ModelResponseMessage> AddDocMenuC33Async(ModelMenuC33 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3_3", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = model.meetingid;
                        cmd.Parameters.Add("@agenda_3_project_count", SqlDbType.Int).Value = model.agenda3projectcount;
                        cmd.Parameters.Add("@agenda_3_project_number", SqlDbType.VarChar, 50).Value = ParseDataHelper.ConvertDBNull(model.agenda3projectnumber);
                        cmd.Parameters.Add("@agenda_3_project_name_thai", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda3projectnamethai);
                        cmd.Parameters.Add("@agenda_3_project_name_eng", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda3projectnameeng);
                        cmd.Parameters.Add("@agenda_3_conclusion", SqlDbType.Int).Value = model.agenda3Conclusion;
                        cmd.Parameters.Add("@agenda_3_conclusion_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda3ConclusionName);
                        cmd.Parameters.Add("@agenda_3_suggestion", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda3Suggestion);
                        cmd.Parameters.Add("@isClose", SqlDbType.Bit).Value = (model.agenda3Conclusion == "4") ? true : false;

                        //Tab 3 Group All
                        IList<ModelMenuC33Tab3GroupAll> list_Tab3_group_all = new List<ModelMenuC33Tab3GroupAll>();
                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab3Group1Seq1Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab3Group1Seq2Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab3Group1Seq3Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab3Group1Seq4Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab3Group1Seq5Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group1Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }
                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab3Group2Seq1Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab3Group2Seq2Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab3Group2Seq3Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab3Group2Seq4Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab3Group2Seq5Input1))
                                    {
                                        list_Tab3_group_all.Add(new ModelMenuC33Tab3GroupAll
                                        {
                                            groupdata = "3.2",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab3Group2Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }
                        string tab_3_group_all_json = JsonConvert.SerializeObject(list_Tab3_group_all);

                        cmd.Parameters.Add("@tab_3_group_all_json", SqlDbType.VarChar).Value = tab_3_group_all_json;

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

        public async Task<IList<ModelSelectOption>> GetAllProjectForC3Tab3Async(string AssignerCode, string DocProcess)
        {

            string sql = "SELECT * FROM [dbo].[Doc_Process] " +
                          "WHERE project_type='PROJECT' AND doc_process_to='" + DocProcess + "' AND revert_type='Edit.A4' ";

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

        public async Task<ModelMenuC33Data> GetProjectNumberWithDataC3Tab3Async(string project_number)
        {

            string sql = "SELECT * FROM [dbo].[Doc_Process] " +
                         "WHERE project_number ='" + project_number + "'";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuC33Data e = new ModelMenuC33Data();
                        while (await reader.ReadAsync())
                        {
                            e.projectnamethai = reader["project_name_thai"].ToString();
                            e.projectnameeng = reader["project_name_eng"].ToString();
                        }
                        GetProjectDataMenuC2ForTab3Async(ref e, project_number);
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public void GetProjectDataMenuC2ForTab3Async(ref ModelMenuC33Data e, string project_number)
        {
            string sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                             "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                            "FROM Doc_Process Doc " +
                            "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a4_revision = C2.project_revision " +
                            "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                            "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                            "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                            "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'Edit.A4' " +
                            "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                            "ORDER BY C2.doc_id ASC ";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(sql_data, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    int ir = 1;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            switch (ir)
                            {
                                case 1:
                                    e.tab3Group1Seq1Input1 = reader["approve_name"].ToString();
                                    e.tab3Group1Seq1Input2 = reader["name_thai"].ToString();
                                    e.tab3Group1Seq1Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 2:
                                    e.tab3Group1Seq2Input1 = reader["approve_name"].ToString();
                                    e.tab3Group1Seq2Input2 = reader["name_thai"].ToString();
                                    e.tab3Group1Seq2Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 3:
                                    e.tab3Group1Seq3Input1 = reader["approve_name"].ToString();
                                    e.tab3Group1Seq3Input2 = reader["name_thai"].ToString();
                                    e.tab3Group1Seq3Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 4:
                                    e.tab3Group1Seq4Input1 = reader["approve_name"].ToString();
                                    e.tab3Group1Seq4Input2 = reader["name_thai"].ToString();
                                    e.tab3Group1Seq4Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 5:
                                    e.tab3Group1Seq5Input1 = reader["approve_name"].ToString();
                                    e.tab3Group1Seq5Input2 = reader["name_thai"].ToString();
                                    e.tab3Group1Seq5Input3 = reader["comment_consider"].ToString();
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }
                            ir++;
                        }
                    }
                }
                conn.Close();
            }
        }

        public async Task<IList<ModelSelectOption>> GetAllApprovalTypeByProjectC2ForTab3Async(string project_number)
        {
            string sql = "SELECT A.project_number, A.approval_type, (B.name_thai + ' ' + A.safety_type + ' ' + B.name_thai_sub) AS approval_name " +
                         "FROM Doc_MenuC2 A INNER JOIN MST_ApprovalType B ON A.approval_type = B.id " +
                         "WHERE A.project_number='" + project_number + "'";

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
                            item.value = reader["approval_type"].ToString();
                            item.label = reader["approval_name"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<IList<ModelMenuC33HistoryData>> GetAllHistoryDataC3Tab3Async()
        {

            string sql = "SELECT meeting_id, " +
                "('วันที่ ' + CONVERT(VARCHAR, meeting_date, 103) + ' ครั้งที่ ' + CONVERT(VARCHAR, meeting_round) + ' ปี ' + CONVERT(VARCHAR, year_of_meeting)) as rptMeetingTitle, " +
                "('3.1.' + CONVERT(VARCHAR, ROW_NUMBER() OVER(PARTITION BY meeting_id ORDER BY id ASC))) AS rptAgenda31, " +
                 "agenda_3_project_count, agenda_3_project_number, " +
                 "agenda_3_project_name_thai, agenda_3_project_name_eng, " +
                 "agenda_3_conclusion_name, agenda_3_suggestion " +
                "FROM Doc_MenuC3_Tab3 A " +
                "INNER JOIN Doc_MenuC3 B " +
                "ON A.meeting_id = B.doc_id " +
                "WHERE group_data = '3.1' " +
                "GROUP BY id,meeting_id, " +
                "agenda_3_project_count,agenda_3_project_number, " +
                "agenda_3_project_name_thai,agenda_3_project_name_eng, " +
                "agenda_3_conclusion_name,agenda_3_suggestion, " +
                "B.doc_id, B.meeting_date, B.meeting_round, B.year_of_meeting " +
                "ORDER BY meeting_id DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelMenuC33HistoryData> e = new List<ModelMenuC33HistoryData>();
                        while (await reader.ReadAsync())
                        {
                            ModelMenuC33HistoryData item = new ModelMenuC33HistoryData();
                            item.rptMeetingId = reader["meeting_id"].ToString();
                            item.rptMeetingTitle = reader["rptMeetingTitle"].ToString();
                            item.rptAgenda31 = reader["rptAgenda31"].ToString();
                            item.rptProjectCount = reader["agenda_3_project_count"].ToString();
                            item.rptProjectNumber = reader["agenda_3_project_number"].ToString();
                            item.rptProjectNameThai = reader["agenda_3_project_name_thai"].ToString();
                            item.rptProjectNameEng = reader["agenda_3_project_name_eng"].ToString();
                            item.rptConclusionName = reader["agenda_3_conclusion_name"].ToString();
                            item.rptSuggestionName = reader["agenda_3_suggestion"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }



        // ระเบียบวาระที่ 4 ------------------------------------------------------------------------------
        public async Task<ModelMenuC34_InterfaceData> MenuC34InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC34_InterfaceData resp = new ModelMenuC34_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            if (resp.ListMeetingId != null && resp.ListMeetingId.Count > 0)
            {
                resp.meetingId = resp.ListMeetingId.FirstOrDefault().value;
                resp.meetingName = resp.ListMeetingId.FirstOrDefault().label;
            }
            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M017");

            return resp;
        }

        public async Task<ModelResponseMessage> AddDocMenuC34Async(ModelMenuC34 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3_4", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = model.meetingid;
                        cmd.Parameters.Add("@agenda_4_term", SqlDbType.VarChar, 2).Value = ParseDataHelper.ConvertDBNull(model.agenda4term);
                        cmd.Parameters.Add("@agenda_4_project_number", SqlDbType.VarChar, 50).Value = ParseDataHelper.ConvertDBNull(model.agenda4projectnumber);
                        cmd.Parameters.Add("@agenda_4_project_name_1", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda4projectname1);
                        cmd.Parameters.Add("@agenda_4_project_name_2", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda4projectname2);
                        cmd.Parameters.Add("@agenda_4_conclusion", SqlDbType.Int).Value = model.agenda4Conclusion;
                        cmd.Parameters.Add("@agenda_4_conclusion_name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda4ConclusionName);
                        cmd.Parameters.Add("@agenda_4_suggestion", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.agenda4Suggestion);
                        cmd.Parameters.Add("@file1name", SqlDbType.VarChar, 200).Value = ParseDataHelper.ConvertDBNull(model.file1name);
                        cmd.Parameters.Add("@isClose", SqlDbType.Bit).Value = (model.agenda4Conclusion == "4") ? true : false;

                        DateTime dtAlertDate = Convert.ToDateTime(DateTime.Now).AddDays(335);
                        cmd.Parameters.Add("@alert_date", SqlDbType.VarChar, 50).Value = dtAlertDate.ToString("dd/MM/yyyy");

                        DateTime dtExpireDate = Convert.ToDateTime(DateTime.Now).AddDays(365);
                        cmd.Parameters.Add("@certificate_expire_date", SqlDbType.VarChar, 50).Value = dtExpireDate.ToString("dd/MM/yyyy");

                        //Tab 4 Group All
                        IList<ModelMenuC34Tab4GroupAll> list_Tab4_group_all = new List<ModelMenuC34Tab4GroupAll>();

                        //list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                        //{
                        //    groupdata = "4.1",
                        //    seq = 1.ToString(),
                        //    input1 = "A",
                        //    input2 = "B",
                        //    input3 = "C",
                        //});

                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab4Group1Seq1Input1))
                                    {
                                        list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                                        {
                                            groupdata = "4.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab4Group1Seq2Input1))
                                    {
                                        list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                                        {
                                            groupdata = "4.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab4Group1Seq3Input1))
                                    {
                                        list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                                        {
                                            groupdata = "4.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab4Group1Seq4Input1))
                                    {
                                        list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                                        {
                                            groupdata = "4.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab4Group1Seq5Input1))
                                    {
                                        list_Tab4_group_all.Add(new ModelMenuC34Tab4GroupAll
                                        {
                                            groupdata = "4.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab4Group1Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }
                        string tab_4_group_all_json = JsonConvert.SerializeObject(list_Tab4_group_all);

                        cmd.Parameters.Add("@tab_4_group_all_json", SqlDbType.VarChar).Value = tab_4_group_all_json;

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

        public async Task<IList<ModelSelectOption>> GetAllProjectNumberTab4Async(int type)
        {
            string sql = "";

            switch (type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    sql = "SELECT MIN(doc_id) AS first_approval, A.project_number, " +
                         "A.project_name_thai, A.project_name_eng,B.safety_type " +
                         "FROM [dbo].[Doc_Process] A " +
                         "LEFT OUTER JOIN [dbo].[Doc_MenuC2] B " +
                         "ON A.project_number = B.project_number " +
                         "WHERE A.doc_process_to='C2' " +
                         "AND A.project_type='PROJECT' " +
                         "AND A.revert_type='New' " +
                         "AND B.safety_type=" + type + " " +
                         "GROUP BY A.project_number, A.project_name_thai, A.project_name_eng, B.safety_type";
                    break;
                case 8:
                    sql = "SELECT MIN(doc_id) AS first_approval, A.project_number, " +
                          "A.project_name_thai, A.project_name_eng,B.safety_type " +
                          "FROM [dbo].[Doc_Process] A " +
                          "LEFT OUTER JOIN [dbo].[Doc_MenuC2] B " +
                          "ON A.project_number = B.project_number " +
                          "WHERE A.doc_process_to='C22' " +
                          "AND A.project_type='LAB' " +
                          "GROUP BY A.project_number, A.project_name_thai, A.project_name_eng, B.safety_type";
                    break;
                case 5:
                    sql = "SELECT * FROM [dbo].[Doc_Process] " +
                          "WHERE doc_process_from='A6' AND doc_process_to='C2' AND revert_type='Renew.A6' " + // A6 ขอต่ออายุ
                          "AND project_type='PROJECT' ";
                    break;
                case 6:
                    sql = "SELECT * FROM [dbo].[Doc_Process] " +
                          "WHERE doc_process_from='A5' AND doc_process_to='C2' AND revert_type='Renew.A5' " + // A5 แก้ไขโครงการที่ผ่านการรับรอง
                          "AND project_type='PROJECT' ";
                    break;
                case 7:
                    sql = "SELECT * FROM [dbo].[Doc_Process] " +
                          "WHERE doc_process_from='A7' AND doc_process_to='C2' AND revert_type='Renew.A7' " + // A7 ปิดโครงการ
                          "AND project_type='PROJECT' ";
                    break;
                case 9:
                    sql = "SELECT * FROM [dbo].[Doc_Process] " +
                          "WHERE doc_process_to='C2' " + // 9.
                          "AND project_type='PROJECT' ";
                    break;
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

        public async Task<ModelMenuC34Tab4Data> GetProjectNumberWithDataC3Tab4Async(int type, string project_number)
        {

            string sql = "SELECT * FROM [dbo].[Doc_Process] WHERE project_number ='" + project_number + "'";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelMenuC34Tab4Data e = new ModelMenuC34Tab4Data();

                        string revert_type = "";
                        int a4_revision = 0;
                        int a5_revision = 0;
                        int a6_revision = 0;
                        int a7_revision = 0;
                        while (await reader.ReadAsync())
                        {
                            e.agenda4ProjectName1 = reader["project_name_thai"].ToString();
                            e.agenda4ProjectName2 = reader["project_name_eng"].ToString();

                            revert_type = reader["revert_type"].ToString();
                            a4_revision = Convert.ToInt32(reader["a4_revision"]);
                            a5_revision = Convert.ToInt32(reader["a5_revision"]);
                            a6_revision = Convert.ToInt32(reader["a6_revision"]);
                            a7_revision = Convert.ToInt32(reader["a7_revision"]);
                        }
                        GetProjectDataMenuC22ForTab4Async(ref e, type, project_number, revert_type, a4_revision, a5_revision, a6_revision, a7_revision);
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public void GetProjectDataMenuC22ForTab4Async(ref ModelMenuC34Tab4Data e, int type, string project_number, string revert_type, int a4_revision, int a5_revision, int a6_revision, int a7_revision)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql_data = "";

                switch (type)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                                        "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                                       "FROM Doc_Process Doc " +
                                       "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a4_revision = C2.project_revision " +
                                       "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                                       "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                                       "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                                       "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'New' " +
                                       "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                                       "ORDER BY C2.doc_id ASC ";
                        break;
                    case 5:
                        sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                                        "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                                       "FROM Doc_Process Doc " +
                                       "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a6_revision = C2.project_revision " +
                                       "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                                       "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                                       "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                                       "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'Renew.A6' AND C2.project_revision='" + a6_revision + "' " +
                                       "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                                       "ORDER BY C2.doc_id ASC ";
                        break;
                    case 6:
                        sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                                        "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                                       "FROM Doc_Process Doc " +
                                       "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a5_revision = C2.project_revision " +
                                       "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                                       "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                                       "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                                       "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'Renew.A5' AND C2.project_revision='" + a5_revision + "' " +
                                       "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                                       "ORDER BY C2.doc_id ASC ";
                        break;
                    case 7:
                        sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                                        "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                                       "FROM Doc_Process Doc " +
                                       "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a7_revision = C2.project_revision " +
                                       "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                                       "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                                       "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                                       "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'Renew.A7' AND C2.project_revision='" + a7_revision + "' " +
                                       "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                                       "ORDER BY C2.doc_id ASC ";
                        break;
                    case 8:
                        sql_data = "SELECT D.full_name as approve_name, (C.name_thai + ' ' + C.name_thai_sub) AS name_thai, A.comment_consider " +
                                "FROM Doc_MenuC2_2 A " +
                                "INNER JOIN MST_ApprovalType C ON A.approval_type = C.id " +
                                "INNER JOIN RegisterUser D ON A.assigner_code = D.register_id " +
                                "WHERE project_number='" + project_number + "' ORDER BY doc_id ASC ";
                        break;
                    case 9:
                        sql_data = "SELECT MAX(C2.project_revision),D.full_name as approve_name, " +
                                    "(C.name_thai + ' ' + B.name_thai + ' ' + C.name_thai_sub) AS name_thai,C2.comment_consider " +
                                   "FROM Doc_Process Doc " +
                                   "INNER JOIN Doc_MenuC2 C2 ON Doc.project_number = C2.project_number AND Doc.a4_revision = C2.project_revision " +
                                   "INNER JOIN MST_Safety B ON C2.safety_type = B.id " +
                                   "INNER JOIN MST_ApprovalType C ON C2.approval_type = C.id " +
                                   "INNER JOIN RegisterUser D ON C2.assigner_code = D.register_id " +
                                   "WHERE C2.project_number = '" + project_number + "' AND C2.project_revert_type = 'New' " +
                                   "GROUP BY C2.doc_id,C2.project_revision,D.full_name,C.name_thai,B.name_thai,C.name_thai_sub,C2.comment_consider " +
                                   "ORDER BY C2.doc_id ASC ";
                        break;
                }

                using (SqlCommand command = new SqlCommand(sql_data, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    int ir = 1;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            switch (ir)
                            {
                                case 1:
                                    e.tab4Group1Seq1Input1 = reader["approve_name"].ToString();
                                    e.tab4Group1Seq1Input2 = reader["name_thai"].ToString();
                                    e.tab4Group1Seq1Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 2:
                                    e.tab4Group1Seq2Input1 = reader["approve_name"].ToString();
                                    e.tab4Group1Seq2Input2 = reader["name_thai"].ToString();
                                    e.tab4Group1Seq2Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 3:
                                    e.tab4Group1Seq3Input1 = reader["approve_name"].ToString();
                                    e.tab4Group1Seq3Input2 = reader["name_thai"].ToString();
                                    e.tab4Group1Seq3Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 4:
                                    e.tab4Group1Seq4Input1 = reader["approve_name"].ToString();
                                    e.tab4Group1Seq4Input2 = reader["name_thai"].ToString();
                                    e.tab4Group1Seq4Input3 = reader["comment_consider"].ToString();
                                    break;
                                case 5:
                                    e.tab4Group1Seq5Input1 = reader["approve_name"].ToString();
                                    e.tab4Group1Seq5Input2 = reader["name_thai"].ToString();
                                    e.tab4Group1Seq5Input3 = reader["comment_consider"].ToString();
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }
                            ir++;
                        }
                    }
                }
                conn.Close();
            }
        }


        // ระเบียบวาระที่ 5 ------------------------------------------------------------------------------
        public async Task<ModelMenuC35_InterfaceData> MenuC35InterfaceDataAsync(string RegisterId)
        {
            ModelMenuC35_InterfaceData resp = new ModelMenuC35_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            if (resp.ListMeetingId != null && resp.ListMeetingId.Count > 0)
            {
                resp.meetingId = resp.ListMeetingId.FirstOrDefault().value;
                resp.meetingName = resp.ListMeetingId.FirstOrDefault().label;
            }
            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M018");

            return resp;
        }

        public async Task<ModelResponseMessage> AddDocMenuC35Async(ModelMenuC35 model)
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
                    using (SqlCommand cmd = new SqlCommand("sp_doc_menu_c3_5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@meeting_id", SqlDbType.Int).Value = model.meetingid;

                        //Tab 5 Group 1
                        IList<ModelMenuC35Tab5Group1> list_tab5_group_1 = new List<ModelMenuC35Tab5Group1>();
                        for (int i = 0; i < 10; i++)
                        {
                            string seq = (i + 1).ToString();
                            switch (i + 1)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq1Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq1Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq1Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq1Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq2Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq2Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq2Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq2Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq3Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq3Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq3Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq3Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq4Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq4Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq4Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq4Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq5Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq5Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq5Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq5Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq6Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq6Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq6Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq6Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 7:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq7Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq7Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq7Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq7Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 8:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq8Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq8Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq8Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq8Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 9:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq9Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq9Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq9Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq9Input3).ToString(),
                                        });
                                    }
                                    break;
                                case 10:
                                    if (!string.IsNullOrEmpty(model.tab5Group1Seq10Input1))
                                    {
                                        list_tab5_group_1.Add(new ModelMenuC35Tab5Group1
                                        {
                                            groupdata = "5.1",
                                            seq = seq,
                                            input1 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq10Input1).ToString(),
                                            input2 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq10Input2).ToString(),
                                            input3 = ParseDataHelper.ConvertDBNull(model.tab5Group1Seq10Input3).ToString(),
                                        });
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }
                        string tab_5_group_1_json = JsonConvert.SerializeObject(list_tab5_group_1);

                        cmd.Parameters.Add("@tab_5_group_1_json", SqlDbType.VarChar).Value = tab_5_group_1_json;

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



        // ใช้ร่วมกัน --------------------------------------------------------------------------

        public async Task<IList<ModelSelectOption>> GetAllMeetingIdAsync()
        {

            string sql = "SELECT TOP(5) doc_id as meeting_id, " +
                        "'ครั้งที่ ' + CONVERT(VARCHAR, meeting_round) + " +
                        "' ปี ' + CONVERT(VARCHAR, year_of_meeting) + " +
                        "' วันที่ ' + CONVERT(VARCHAR, meeting_date, 103) as meeting_name " +
                        "FROM Doc_MenuC3 " +
                        "WHERE isClosed=0 " +
                        "ORDER BY meeting_id DESC";

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
                            item.value = reader["meeting_id"].ToString();
                            item.label = reader["meeting_name"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

    }

}
