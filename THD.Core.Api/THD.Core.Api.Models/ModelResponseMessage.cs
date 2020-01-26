using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using THD.Core.Api.Models.ReportModels;

namespace THD.Core.Api.Models
{
    public class ModelResponseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int DocId { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
        public string filename1and2 { get; set; }
        public string filebase1and264 { get; set; }
        public string filename12 { get; set; }
        public string filebase1264 { get; set; }
        public string filename13 { get; set; }
        public string filebase1364 { get; set; }
        public string filename16 { get; set; }
        public string filebase1664 { get; set; }
    }

    public class ModelResponseMessageRegisterUser
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string RegisterId { get; set; }
        public string TokenKey { get; set; }
    }

    public class ModelResponseMessageRegisterActive
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ModelResponseMessageUpdateUserRegister
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ModelResponseMessageLogin
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ModelResponseMessageLoginData Data { get; set; }
    }

    public class ModelResponseMessageLoginData
    {
        public string Guid { get; set; }
        public string Token { get; set; }
        public string RegisterId { get; set; } //Base64
        public string FullName { get; set; }
        public string PositionName { get; set; }
    }

    public class ModelResponseMessageAddDocB1
    {
        public bool Status { get; set; }
        public string DocNumber { get; set; }
        public string Message { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

    public class ModelResponseMessageAddDocB2
    {
        public bool Status { get; set; }
        public string DocNumber { get; set; }
        public string Message { get; set; }
    }

    public class ModelResponseC1Message
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string EmailArray { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }

    public class ModelResponseC12Message
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string EmailArray { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }


    public class ModelResponseC2Message
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string EmailArray { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }


    public class ModelResponseC22Message
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string EmailArray { get; set; }
        // Report Return --------------
        public string filename { get; set; }
        public string filebase64 { get; set; }
    }


}
