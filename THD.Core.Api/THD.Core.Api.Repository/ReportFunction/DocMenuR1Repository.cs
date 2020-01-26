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

namespace THD.Core.Api.Repository.DataHandler
{
    public class DocMenuR1Repository : IDocMenuR1Repository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly IDropdownListRepository _IDropdownListRepository;
        private readonly IRegisterUserRepository _IRegisterUserRepository;
        public DocMenuR1Repository(IConfiguration configuration, IDropdownListRepository DropdownListRepository, IRegisterUserRepository IRegisterUserRepository)
        {
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));
            _IDropdownListRepository = DropdownListRepository;
            _IRegisterUserRepository = IRegisterUserRepository;
        }




        // ระเบียบวาระที่ 4 ------------------------------------------------------------------------------
        public async Task<ModelMenuR1_InterfaceData> MenuR1InterfaceDataAsync(string RegisterId)
        {
            ModelMenuR1_InterfaceData resp = new ModelMenuR1_InterfaceData();

            resp.ListMeetingId = new List<ModelSelectOption>();

            resp.ListMeetingId = await GetAllMeetingIdAsync();

            ModelSelectOption all_meeting = new ModelSelectOption() { value = "", label = "เลือก..." };
            resp.ListMeetingId.Add(all_meeting);

            resp.ListMeetingType = await GetAllMeetingTypeAsync();

            ModelSelectOption all_meeting_type = new ModelSelectOption() { value = "", label = "เลือก..." };
            resp.ListMeetingType.Add(all_meeting_type);

            resp.ListReportData = await GetAllReportHistoryDataR1Async(null);

            resp.UserPermission = await _IRegisterUserRepository.GetPermissionPageAsync(RegisterId, "M019");

            return resp;
        }

        public async Task<IList<ModelSelectOption>> GetAllMeetingTypeAsync()
        {
            string sql = "SELECT id, name_thai FROM MST_MeetingRecordType ORDER BY id ASC";

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
                            item.value = reader["id"].ToString();
                            item.label = reader["name_thai"].ToString();
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<IList<ModelMenuR1Data>> GetAllReportHistoryDataR1Async(ModelMenuR1_InterfaceData search)
        {

            string sql = "SELECT doc_id,year_of_meeting,meeting_round,B.name_thai, " +
                        "meeting_date, meeting_location,committees_array, A.isClosed " +
                        "FROM Doc_MenuC3 A " +
                        "LEFT OUTER JOIN MST_MeetingRecordType B " +
                        "ON A.meeting_record_id = B.id " +
                        "WHERE 1=1 ";

            if (search != null && !string.IsNullOrEmpty(search.meetingid)) sql += " AND doc_id='" + search.meetingid + "' ";

            if (search != null && !string.IsNullOrEmpty(search.meetingTypeId)) sql += " AND meeting_record_id='" + search.meetingTypeId + "' ";

            sql += " ORDER BY doc_id DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        IList<ModelMenuR1Data> e = new List<ModelMenuR1Data>();
                        while (await reader.ReadAsync())
                        {
                            ModelMenuR1Data item = new ModelMenuR1Data();
                            item.docid = reader["doc_id"].ToString();
                            item.meetingdate = Convert.ToDateTime(reader["meeting_date"]).ToString("dd/MM/yyyy");
                            item.meetinglocation = reader["meeting_location"].ToString();
                            item.meetingrecordname = reader["name_thai"].ToString();
                            item.meetinground = reader["meeting_round"].ToString();
                            item.yearofmeeting = reader["year_of_meeting"].ToString();
                            item.committeesarray = "";
                            item.isclosed = Convert.ToBoolean(reader["isClosed"]);
                            e.Add(item);
                        }
                        return e;
                    }
                }
                conn.Close();
            }
            return null;

        }

        public async Task<IList<ModelSelectOption>> GetAllMeetingIdAsync()
        {

            string sql = "SELECT TOP(5) doc_id as meeting_id, " +
                        "'ครั้งที่ ' + CONVERT(VARCHAR, meeting_round) + " +
                        "' ปี ' + CONVERT(VARCHAR, year_of_meeting) + " +
                        " 'วันที่ ' + CONVERT(VARCHAR, meeting_date, 103) as meeting_name " +
                        "FROM Doc_MenuC3 " +
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
