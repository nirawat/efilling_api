using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THD.Core.Api.Models
{
    public class ModelMenuC33_InterfaceData
    {
        public string meetingId { get; set; }
        public string meetingName { get; set; }
        public IList<ModelSelectOption> ListMeetingId { get; set; }
        public IList<ModelSelectOption> ListProjectNumberTab3 { get; set; }
        public ModelPermissionPage UserPermission { get; set; }
    }
    public class ModelMenuC33
    {
        public int meetingid { get; set; } //เอกสารอ้างอิงจากหน้าบันทึกประชุม
        public int agenda3projectcount { get; set; }
        public string agenda3projectnumber { get; set; }
        public string agenda3projectnamethai { get; set; }
        public string agenda3projectnameeng { get; set; }
        public string agenda3Conclusion { get; set; }
        public string agenda3ConclusionName { get; set; }
        public string agenda3Suggestion { get; set; }


        //Tab 3 Group 1
        public string tab3Group1Seq1Input1 { get; set; }
        public string tab3Group1Seq1Input2 { get; set; }
        public string tab3Group1Seq1Input3 { get; set; }
        public string tab3Group1Seq2Input1 { get; set; }
        public string tab3Group1Seq2Input2 { get; set; }
        public string tab3Group1Seq2Input3 { get; set; }
        public string tab3Group1Seq3Input1 { get; set; }
        public string tab3Group1Seq3Input2 { get; set; }
        public string tab3Group1Seq3Input3 { get; set; }
        public string tab3Group1Seq4Input1 { get; set; }
        public string tab3Group1Seq4Input2 { get; set; }
        public string tab3Group1Seq4Input3 { get; set; }
        public string tab3Group1Seq5Input1 { get; set; }
        public string tab3Group1Seq5Input2 { get; set; }
        public string tab3Group1Seq5Input3 { get; set; }

        //Tab 3 Group 2
        public string tab3Group2Seq1Input1 { get; set; }
        public string tab3Group2Seq1Input2 { get; set; }
        public string tab3Group2Seq1Input3 { get; set; }
        public string tab3Group2Seq2Input1 { get; set; }
        public string tab3Group2Seq2Input2 { get; set; }
        public string tab3Group2Seq2Input3 { get; set; }
        public string tab3Group2Seq3Input1 { get; set; }
        public string tab3Group2Seq3Input2 { get; set; }
        public string tab3Group2Seq3Input3 { get; set; }
        public string tab3Group2Seq4Input1 { get; set; }
        public string tab3Group2Seq4Input2 { get; set; }
        public string tab3Group2Seq4Input3 { get; set; }
        public string tab3Group2Seq5Input1 { get; set; }
        public string tab3Group2Seq5Input2 { get; set; }
        public string tab3Group2Seq5Input3 { get; set; }



    }

    public class ModelMenuC33Data
    {
        public bool isprojectgroup { get; set; }
        public string projectnamethai { get; set; }
        public string projectnameeng { get; set; }
        public IList<ModelSelectOption> ListAssignerUser { get; set; }
        public IList<ModelSelectOption> ListApprovalType { get; set; }


        public string tab3Group1Seq1Input1 { get; set; }
        public string tab3Group1Seq1Input2 { get; set; }
        public string tab3Group1Seq1Input3 { get; set; }
        public string tab3Group1Seq2Input1 { get; set; }
        public string tab3Group1Seq2Input2 { get; set; }
        public string tab3Group1Seq2Input3 { get; set; }
        public string tab3Group1Seq3Input1 { get; set; }
        public string tab3Group1Seq3Input2 { get; set; }
        public string tab3Group1Seq3Input3 { get; set; }
        public string tab3Group1Seq4Input1 { get; set; }
        public string tab3Group1Seq4Input2 { get; set; }
        public string tab3Group1Seq4Input3 { get; set; }
        public string tab3Group1Seq5Input1 { get; set; }
        public string tab3Group1Seq5Input2 { get; set; }
        public string tab3Group1Seq5Input3 { get; set; }
    }

    public class ModelMenuC33Tab3GroupAll
    {
        public string groupdata { get; set; }
        public string seq { get; set; }
        public string input1 { get; set; }
        public string input2 { get; set; }
        public string input3 { get; set; }
    }

    public class ModelMenuC33HistoryData
    {
        public string rptMeetingId { get; set; }
        public string rptMeetingTitle { get; set; }
        public string rptAgenda31 { get; set; }
        public string rptProjectCount { get; set; }
        public string rptProjectNumber { get; set; }
        public string rptProjectNameThai { get; set; }
        public string rptProjectNameEng { get; set; }
        public string rptConclusionName { get; set; }
        public string rptSuggestionName { get; set; }
    }



}
