using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THD.Core.Api.Models
{
    public class ModelMenuA1_InterfaceData
    {
        public string defaultuserid { get; set; }
        public string defaultusername { get; set; }
        public string facultyname { get; set; }
        public string workphone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string email { get; set; }

        public IList<ModelSelectOption> ListCommittees { get; set; }
        public IList<ModelSelectOption> ListMembers { get; set; }

        public string editdatamessage { get; set; }
        public ModelMenuA1 editdata { get; set; }
        public ModelPermissionPage UserPermission { get; set; }
    }
    public class ModelMenuA1
    {
        public string docid { get; set; }
        public DateTime docdate { get; set; }
        public string docnumber { get; set; }
        public string projectconsultant { get; set; }
        public string projecttype { get; set; }
        public string projecttypename { get; set; }
        public string projecthead { get; set; }
        public string projectheadname { get; set; }
        public string facultyname { get; set; }
        public string workphone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        [Required(ErrorMessage = "userid is required.")]
        public string projectnamethai { get; set; }
        [Required(ErrorMessage = "userid is required.")]
        public string projectnameeng { get; set; }
        [Required(ErrorMessage = "userid is required.")]
        public string budget { get; set; }
        public string moneysupply { get; set; }
        public string laboratoryused { get; set; }
        public string laboratoryusedname { get; set; }
        public string file1name { get; set; }
        public string file1base64 { get; set; }
        public string file2name { get; set; }
        public string file2base64 { get; set; }
        public string file3name { get; set; }
        public string file3base64 { get; set; }
        public string file4name { get; set; }
        public string file4base64 { get; set; }
        public string file5name { get; set; }
        public string file5base64 { get; set; }
        public string accordingtypemethod { get; set; }
        public string accordingtypemethodname { get; set; }
        public string projectother { get; set; }
        public string projectaccordingtypemethod { get; set; }
        public string projectaccordingtypemethodname { get; set; }
        public string reachother { get; set; }
        public bool riskgroup1 { get; set; }
        public bool riskgroup11 { get; set; }
        public bool riskgroup12 { get; set; }
        public bool riskgroup13 { get; set; }
        public bool riskgroup14 { get; set; }
        public bool riskgroup15 { get; set; }
        public string riskgroup15other { get; set; }
        public bool riskgroup2 { get; set; }
        public bool riskgroup21 { get; set; }
        public bool riskgroup22 { get; set; }
        public bool riskgroup23 { get; set; }
        public bool riskgroup24 { get; set; }
        public bool riskgroup25 { get; set; }
        public bool riskgroup3 { get; set; }
        public bool riskgroup31 { get; set; }
        public bool riskgroup32 { get; set; }
        public bool riskgroup33 { get; set; }
        public bool riskgroup34 { get; set; }
        public bool riskgroup35 { get; set; }
        public bool riskgroup4 { get; set; }
        public bool riskgroup41 { get; set; }
        public bool riskgroup42 { get; set; }
        public bool riskgroup43 { get; set; }
        public bool riskgroup44 { get; set; }
        public bool riskgroup45 { get; set; }

        public string projeckeynumber { get; set; }


        //Member Project
        public MemberProject member1json { get; set; }
        public string member1projecthead { get; set; }
        public string member1facultyname { get; set; }
        public string member1workphone { get; set; }
        public string member1mobile { get; set; }
        public string member1fax { get; set; }
        public string member1email { get; set; }

        public MemberProject member2json { get; set; }
        public string member2projecthead { get; set; }
        public string member2facultyname { get; set; }
        public string member2workphone { get; set; }
        public string member2mobile { get; set; }
        public string member2fax { get; set; }
        public string member2email { get; set; }

        public MemberProject member3json { get; set; }
        public string member3projecthead { get; set; }
        public string member3facultyname { get; set; }
        public string member3workphone { get; set; }
        public string member3mobile { get; set; }
        public string member3fax { get; set; }
        public string member3email { get; set; }

        public MemberProject member4json { get; set; }
        public string member4projecthead { get; set; }
        public string member4facultyname { get; set; }
        public string member4workphone { get; set; }
        public string member4mobile { get; set; }
        public string member4fax { get; set; }
        public string member4email { get; set; }

        public MemberProject member5json { get; set; }
        public string member5projecthead { get; set; }
        public string member5facultyname { get; set; }
        public string member5workphone { get; set; }
        public string member5mobile { get; set; }
        public string member5fax { get; set; }
        public string member5email { get; set; }
        public string labothername { get; set; }


        public string createby { get; set; }
    }

    public class MemberProject
    {
        public string projecthead { get; set; }
        public string facultyname { get; set; }
        public string workphone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class ModelMenuA1_FileDownload
    {
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

}
