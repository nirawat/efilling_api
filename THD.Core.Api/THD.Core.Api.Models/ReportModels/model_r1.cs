using System;
using System.Collections.Generic;
using System.Text;

namespace THD.Core.Api.Models.ReportModels
{

    public class model_rpt_1_file
    {
        public string message { get; set; }
        public string filename1and2 { get; set; }
        public string filebase1and264 { get; set; }
        public string filename16 { get; set; }
        public string filebase1664 { get; set; }
    }

    public class model_rpt_1_report
    {
        public string projecttype { get; set; }
        public string title { get; set; }
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
        public string co_research_fullname3 { get; set; }

    }
}
