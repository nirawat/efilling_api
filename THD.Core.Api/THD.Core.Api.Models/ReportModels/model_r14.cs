using System;
using System.Collections.Generic;
using System.Text;

namespace THD.Core.Api.Models.ReportModels
{
    public class model_rpt_14_file
    {
        public string message { get; set; }
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

    public class model_rpt_14_report
    {
        public string projecttype { get; set; }
        public string Doc_head_1 { get; set; }
        public string Doc_head_2 { get; set; }
        public string Doc_head_3 { get; set; }
        public string Doc_head_4 { get; set; }
        public string Presenter_name { get; set; }
        public bool Position_1 { get; set; }
        public bool Position_2 { get; set; }
        public bool Position_3 { get; set; }
        public bool Position_4 { get; set; }
        public bool Position_5 { get; set; }
        public string Job_Position { get; set; }
        public string Faculty_name { get; set; }
        public string Research_name_thai { get; set; }
        public string Research_name_eng { get; set; }
        public string Advisor_signature { get; set; }
        public string Advisor_fullname { get; set; }
        public string Dept_comment { get; set; }
        public string Dept_signature { get; set; }
        public string Dept_fullname { get; set; }
        public string Headoffaculty_comment { get; set; }
        public string Headoffaculty_signature { get; set; }
        public string Headoffaculty_fullname { get; set; }
        public string HeadofResearch_signature { get; set; }
        public string HeadofResearch_fullname { get; set; }
        public string co_research_signature1 { get; set; }
        public string co_research_fullname1 { get; set; }
        public string co_research_signature2 { get; set; }
        public string co_research_fullname2 { get; set; }

    }

    public class model_r14
    {
        public string head_line_1 { get; set; }
        public string head_line_2 { get; set; }
        public string head_line_3 { get; set; }
        public string head_line_4 { get; set; }
        public string head_line_5 { get; set; }
        public string head_line_6 { get; set; }
        public string head_line_7 { get; set; }
        public string head_line_8 { get; set; }
        public string head_line_9 { get; set; }

        public IList<model_ListMeetingName> ListMeetingData { get; set; }
        public IList<model_ListMeetingName> ListMemberData { get; set; }
        public IList<model_ListAgendaData_1> ListAgendaData_1_1 { get; set; }
        public IList<model_ListAgendaData_1> ListAgendaData_1_2 { get; set; }
        public IList<model_ListAgendaData_2> ListAgendaData_2 { get; set; }
        public IList<model_ListAgendaData_3_4> ListAgendaData_3 { get; set; }
        public IList<model_ListAgendaData_3_4> ListAgendaData_4 { get; set; }
        public IList<model_ListAgendaData_5> ListAgendaData_5 { get; set; }

        public string meeting_close { get; set; }
        public model_signature signature_1 { get; set; }
        public model_signature signature_2 { get; set; }
        public model_signature signature_3 { get; set; }
    }
    public class model_ListMeetingName
    {
        public string seq { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string department { get; set; }
    }

    public class model_ListAgendaData_1
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string subject_line_1 { get; set; }
        public string detail_summary { get; set; }
        public string detail_conclusion { get; set; }
    }

    public class model_ListAgendaData_2
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string detail_summary { get; set; }
        public string detail_conclusion { get; set; }
        public string attachmen_name { get; set; }
    }

    public class model_ListAgendaData_3_4
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string subject_line_1 { get; set; }
        public string subject_project_name_thai { get; set; } // ชื่อภาษาไทย
        public string subject_project_name_eng { get; set; } // ชื่อภาษาอังกฤษ
        public string subject_project_number { get; set; } // เลขสำคัญโครงการ
        public IList<model_ListResearcher_3_4> List_Researcher { get; set; } // นักวิจัย
        public string subject_project_type { get; set; }
        public string subject_attachment_name { get; set; }
        public string subject_advisors_name { get; set; }
        public IList<model_ListConsider_3_4> List_Consider { get; set; } // ความเห็นประกออบการพิจารณา
        public string detail_conclusion { get; set; } // มติ
    }

    public class model_ListResearcher_3_4
    {
        public string seq { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string department { get; set; }
    }

    public class model_ListConsider_3_4
    {
        public string seq { get; set; }
        public string name { get; set; }
        public string detail_line_1 { get; set; }
        public string detail_line_2 { get; set; }
    }

    public class model_ListAgendaData_5
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string detail_summary { get; set; }
        public string detail_conclusion { get; set; }
    }

    public class model_signature
    {
        public string name { get; set; }
        public string position_name { get; set; }
        public string assign_name { get; set; }
    }

}
