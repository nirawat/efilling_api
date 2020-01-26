﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THD.Core.Api.Models
{
    public class ModelMenuC31_InterfaceData
    {
        public string meetingId { get; set; }
        public string meetingName { get; set; }
        public IList<ModelSelectOption> ListMeetingId { get; set; }
        public ModelPermissionPage UserPermission { get; set; }
    }
    public class ModelMenuC31
    {
        public int meetingid { get; set; } //เอกสารอ้างอิงจากหน้าบันทึกประชุม

        //Tab 1 Group 1
        public string tab1Group1Seq1Input1 { get; set; }
        public string tab1Group1Seq1Input2 { get; set; }
        public string tab1Group1Seq1Input3 { get; set; }
        public string tab1Group1Seq2Input1 { get; set; }
        public string tab1Group1Seq2Input2 { get; set; }
        public string tab1Group1Seq2Input3 { get; set; }
        public string tab1Group1Seq3Input1 { get; set; }
        public string tab1Group1Seq3Input2 { get; set; }
        public string tab1Group1Seq3Input3 { get; set; }
        public string tab1Group1Seq4Input1 { get; set; }
        public string tab1Group1Seq4Input2 { get; set; }
        public string tab1Group1Seq4Input3 { get; set; }
        public string tab1Group1Seq5Input1 { get; set; }
        public string tab1Group1Seq5Input2 { get; set; }
        public string tab1Group1Seq5Input3 { get; set; }
        public string tab1Group1Seq6Input1 { get; set; }
        public string tab1Group1Seq6Input2 { get; set; }
        public string tab1Group1Seq6Input3 { get; set; }
        public string tab1Group1Seq7Input1 { get; set; }
        public string tab1Group1Seq7Input2 { get; set; }
        public string tab1Group1Seq7Input3 { get; set; }
        public string tab1Group1Seq8Input1 { get; set; }
        public string tab1Group1Seq8Input2 { get; set; }
        public string tab1Group1Seq8Input3 { get; set; }
        public string tab1Group1Seq9Input1 { get; set; }
        public string tab1Group1Seq9Input2 { get; set; }
        public string tab1Group1Seq9Input3 { get; set; }
        public string tab1Group1Seq10Input1 { get; set; }
        public string tab1Group1Seq10Input2 { get; set; }
        public string tab1Group1Seq10Input3 { get; set; }


        //Tab 1 Group 2 
        public string tab1Group2Seq1Input1 { get; set; }
        public string tab1Group2Seq1Input2 { get; set; }
        public string tab1Group2Seq1Input3 { get; set; }
        public string tab1Group2Seq2Input1 { get; set; }
        public string tab1Group2Seq2Input2 { get; set; }
        public string tab1Group2Seq2Input3 { get; set; }
        public string tab1Group2Seq3Input1 { get; set; }
        public string tab1Group2Seq3Input2 { get; set; }
        public string tab1Group2Seq3Input3 { get; set; }
        public string tab1Group2Seq4Input1 { get; set; }
        public string tab1Group2Seq4Input2 { get; set; }
        public string tab1Group2Seq4Input3 { get; set; }
        public string tab1Group2Seq5Input1 { get; set; }
        public string tab1Group2Seq5Input2 { get; set; }
        public string tab1Group2Seq5Input3 { get; set; }
        public string tab1Group2Seq6Input1 { get; set; }
        public string tab1Group2Seq6Input2 { get; set; }
        public string tab1Group2Seq6Input3 { get; set; }
        public string tab1Group2Seq7Input1 { get; set; }
        public string tab1Group2Seq7Input2 { get; set; }
        public string tab1Group2Seq7Input3 { get; set; }
        public string tab1Group2Seq8Input1 { get; set; }
        public string tab1Group2Seq8Input2 { get; set; }
        public string tab1Group2Seq8Input3 { get; set; }
        public string tab1Group2Seq9Input1 { get; set; }
        public string tab1Group2Seq9Input2 { get; set; }
        public string tab1Group2Seq9Input3 { get; set; }
        public string tab1Group2Seq10Input1 { get; set; }
        public string tab1Group2Seq10Input2 { get; set; }
        public string tab1Group2Seq10Input3 { get; set; }

    }

    public class ModelMenuC31Tab1GroupAll
    {
        public string groupdata { get; set; }
        public string seq { get; set; }
        public string input1 { get; set; }
        public string input2 { get; set; }
        public string input3 { get; set; }
    }
}