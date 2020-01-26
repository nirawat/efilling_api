using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THD.Core.Api.Models
{
    public class ModelMenuC3_InterfaceData
    {
        public IList<ModelSelectOption> ListYearOfProject { get; set; }
        public string defaultround { get; set; }
        public string defaultyear { get; set; }
        public string defaultmeetingdate { get; set; }
        public IList<ModelSelectOption> ListCommittees { get; set; }
        public IList<ModelSelectOption> ListAttendees { get; set; }
        public IList<ModelSelectOption> listDownloadFile { get; set; }
        public ModelPermissionPage UserPermission { get; set; }
    }
    public class ModelMenuC3
    {
        public string docid { get; set; } // คือ Meeting Id
        public int meetingrecordid { get; set; }
        public string meetinground { get; set; }
        public string yearofmeeting { get; set; }
        public string meetingdate { get; set; }
        public string meetinglocation { get; set; }
        public string meetingstart { get; set; }
        public string meetingclose { get; set; }
        public IList<ModelSelectOption> committeesarray { get; set; }
        public IList<ModelSelectOption> attendeesarray { get; set; }

    }

    public class ModelMenuC3_History
    {
        public string docid { get; set; }
        public string meetingrecordname { get; set; }
        public string meetinground { get; set; }
        public string yearofmeeting { get; set; }
        public string meetingdate { get; set; }
        public string meetinglocation { get; set; }
        public string committeesarray { get; set; }

    }

    public class ModelCountOfYearC3
    {
        public string count { get; set; }
        public string meetingdate { get; set; }
    }


    public class ModelCloseMeeting
    {
        public string meetingofround { get; set; }
        public string meetingofyear { get; set; }
    }

}
