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
    public class MailTemplateRepository : IMailTemplateRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public MailTemplateRepository(
            IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetConnectionString("SqlConnection")));
        }

        public async Task<bool> MailTemplate1Async(ModelMenuB1 model)
        {
            string subject = "กองการวิจัยและนวัตกรรม ขอแจ้งผลการพิจารณาเอกสารโครงการวิจัยเรื่อง ";

            ModelMenuA1 data = new ModelMenuA1();






            ModelRegisterActive userInfo = await GetUserInfoByIdAsync(data.createby);

            string mail_body = "<h3>เรียน " + (userInfo.firstname + " " + userInfo.fullname) + "</h3>" + Environment.NewLine +
                               "<h2>" + subject + " " + model.projectnamethai + "</h2>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>พร้อมพร้อมแจ้งผลการพิจารณาตามบันทึกแนบนี้</p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";

            await EmailHelper.SentGmail(userInfo.email, "eFilling : " + subject, mail_body);

            return true;
        }

        public async Task<ModelRegisterActive> GetUserInfoByIdAsync(string RegisterId)
        {

            string sql = "SELECT A.*, (B.name_thai) AS position_name_thai, " +
                        "(C.name_thai) AS faculty_name_thai, (D.name_thai) AS education_name_thai, " +
                        "(E.name_thai) AS character_name_thai " +
                        "FROM RegisterUser A " +
                        "LEFT OUTER JOIN MST_Position B ON A.position = B.id " +
                        "LEFT OUTER JOIN MST_Faculty C ON A.faculty = C.id " +
                        "LEFT OUTER JOIN MST_Education D ON A.education = D.id " +
                        "LEFT OUTER JOIN MST_Character E ON A.character = E.id " +
                        "WHERE register_id ='" + RegisterId + "' ORDER BY full_name ASC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        ModelRegisterActive item = new ModelRegisterActive();
                        while (await reader.ReadAsync())
                        {
                            item.confirmpassw = reader["full_name"].ToString();
                            item.email = reader["email"].ToString();
                            item.firstname = reader["first_name"].ToString();
                            item.fullname = reader["full_name"].ToString();
                            item.faculty = reader["faculty"].ToString();
                            item.facultyname = reader["faculty_name_thai"].ToString();
                            item.position = reader["position"].ToString();
                            item.positionname = reader["position_name_thai"].ToString();
                            item.workphone = reader["work_phone"].ToString();
                            item.mobile = reader["mobile"].ToString();
                            item.fax = reader["fax"].ToString();
                            item.education = reader["education"].ToString();
                            item.educationname = reader["education_name_thai"].ToString();
                            item.character = reader["character"].ToString();
                            item.charactername = reader["character_name_thai"].ToString();
                            item.note1 = reader["note1"].ToString();
                            item.note2 = reader["note2"].ToString();
                            item.note3 = reader["note3"].ToString();
                        }
                        return item;
                    }
                }
                conn.Close();
            }
            return null;

        }


        //Mail Template 
        public async static Task<string> MailTemplate1(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "<h2>กองการวิจัยและนวัตกรรม ขอแจ้งผลการพิจารณาเอกสารโครงการวิจัยเรื่อง " + param2 + "</h2>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>พร้อมพร้อมแจ้งผลการพิจารณาตามบันทึกแนบนี้</p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate2(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "<h2>กองการวิจัยและนวัตกรรม ขอส่งสำเนาใบรับรองโครงการวิจัยเรื่อง " + param2 + "</h2>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>แนบมาพร้อม e-mail นี้  ท่านสามารถรับต้นฉบับจริงได้ด้วยตนเอง ณ งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม (อาคารเอกาทศรถ) มหาวิทยาลัยนเรศวร</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate3(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "<h2>กองการวิจัยและนวัตกรรม ขอนำส่งเอกสารโครงการวิจัยเรื่อง " + param2 + "</h2>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>เพื่อขอความอนุเคราะห์จากท่านได้อ่านและพิจารณาการรับรองโครงการดังกล่าว ตามบันทึกแนบนี้  ซึ่งท่านสามารถล็อกอินเข้า “ระบบรับรองโครงการ”เพื่อดาวน์โหลดเอกสารที่เกี่ยวข้องได้ตั้งแต่บัดนี้เป็นต้นไป</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate4(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>กองการวิจัยและนวัตกรรม ขอแจ้งผลการพิจารณาของคณะกรรมการเพื่อความปลอดภัยทางชีวภาพ โดยมีมติในโครงการวิจัยของท่านเรื่อง <h2>" + param2 + "</h2> พร้อมแจ้งผลการพิจารณาตามบันทึกแนบนี้</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate5(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>กองการวิจัยและนวัตกรรม ขอแจ้งผลการพิจารณาของคณะกรรมการเพื่อความปลอดภัยทางชีวภาพ โดยมีมติในโครงการวิจัยของท่านเรื่อง <h2>" + param2 + "</h2> พร้อมแจ้งผลการพิจารณาตามบันทึกแนบนี้</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate6(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>กองการวิจัยและนวัตกรรม ขอส่งระเบียบวาระการประชุมครั้งที่ <h2>" + param2 + "</h2> ตามระเบียบวาระการประชุมแนบ ท่านสามารถล็อกอินเข้า “ระบบรับรองโครงการ” เพื่อดาวน์โหลดเอกสารที่เกี่ยวข้องกับการประชุมได้ตั้งแต่บัดนี้เป็นต้นไป</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }

        public async static Task<string> MailTemplate7(string param1, string param2)
        {
            string mail_body = "<h3>เรียน " + param1 + "</h3>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<p>กองการวิจัยและนวัตกรรม ขอส่งรายงานการประชุมครั้งที่ <h2>" + param2 + "</h2> ตามรายงานการประชุมแนบ</ p>" + Environment.NewLine +
                               "</br>" + Environment.NewLine +
                               "<h3>งานจัดการมาตรฐานและเครือข่าย กองการวิจัยและนวัตกรรม</h3>" + Environment.NewLine +
                               "<h3>มหาวิทยาลัยนเรศวร</h3>";
            return mail_body;
        }


    }
}
