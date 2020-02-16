﻿using System;
using System.Collections.Generic;
using System.Text;

namespace THD.Core.Api.Models.ReportModels
{

    public class model_rpt_7_file
    {
        public string message { get; set; }
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

    public class model_rpt_7_report
    {
        public string projecttype { get; set; }
        public string doc_head_1 { get; set; }
        public string doc_head_2 { get; set; }
        public string doc_head_3 { get; set; }
        public string doc_head_4 { get; set; }
        public string presenter_name { get; set; }
        public bool position_1 { get; set; }
        public bool position_2 { get; set; }
        public bool position_3 { get; set; }
        public bool position_4 { get; set; }
        public bool position_5 { get; set; }
        public string Job_Position { get; set; }
        public string faculty_name { get; set; }
        public string research_name_thai { get; set; }
        public string research_name_eng { get; set; }
        public string advisor_signature { get; set; }
        public string advisor_fullname { get; set; }
        public string dept_comment { get; set; }
        public string dept_signature { get; set; }
        public string dept_fullname { get; set; }
        public string headoffaculty_comment { get; set; }
        public string headoffaculty_signature { get; set; }
        public string headoffaculty_fullname { get; set; }
        public string headofresearch_signature { get; set; }
        public string headofresearch_fullname { get; set; }
        public string nuibc_no { get; set; }
        public string renew_round { get; set; }

    }
}
