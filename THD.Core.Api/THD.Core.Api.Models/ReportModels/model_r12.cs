using System;
using System.Collections.Generic;
using System.Text;

namespace THD.Core.Api.Models.ReportModels
{

    public class model_rpt_12_file
    {
        public string message { get; set; }
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

    public class model_rpt_12_report
    {
        public string doc_head_4 { get; set; }
        public string projectno { get; set; }
        public string researchname_thai { get; set; }
        public string researchname_eng { get; set; }
        public string sender { get; set; }
        public string round { get; set; }
        public string comment1 { get; set; }
        public string comment2 { get; set; }
    }
}
