using System;
using System.Collections.Generic;
using THD.Core.Api.Business.Interface;
using System.Threading.Tasks;
using THD.Core.Api.Entities.Tables;
using THD.Core.Api.Models;
using THD.Core.Api.Repository.Interface;
using System.Text;
using THD.Core.Api.Helpers;
using System.IO;
using static THD.Core.Api.Helpers.ServerDirectorys;
using THD.Core.Api.Models.Config;

namespace THD.Core.Api.Business
{
    public class DocMenuAService : IDocMenuAService
    {
        private readonly IEnvironmentConfig _IEnvironmentConfig;
        private readonly IDocMenuA1Repository _IDocMenuA1Repository;
        private readonly IDocMenuA2Repository _IDocMenuA2Repository;
        private readonly IDocMenuA3Repository _IDocMenuA3Repository;
        private readonly IDocMenuA4Repository _IDocMenuA4Repository;
        private readonly IDocMenuA5Repository _IDocMenuA5Repository;
        private readonly IDocMenuA6Repository _IDocMenuA6Repository;
        private readonly IDocMenuA7Repository _IDocMenuA7Repository;

        public DocMenuAService(
            IEnvironmentConfig EnvironmentConfig,
            IDocMenuA1Repository DocMenuA1Repository,
            IDocMenuA2Repository DocMenuA2Repository,
            IDocMenuA3Repository DocMenuA3Repository,
            IDocMenuA4Repository DocMenuA4Repository,
            IDocMenuA5Repository DocMenuA5Repository,
            IDocMenuA6Repository DocMenuA6Repository,
            IDocMenuA7Repository DocMenuA7Repository)
        {
            _IEnvironmentConfig = EnvironmentConfig;
            _IDocMenuA1Repository = DocMenuA1Repository;
            _IDocMenuA2Repository = DocMenuA2Repository;
            _IDocMenuA3Repository = DocMenuA3Repository;
            _IDocMenuA4Repository = DocMenuA4Repository;
            _IDocMenuA5Repository = DocMenuA5Repository;
            _IDocMenuA6Repository = DocMenuA6Repository;
            _IDocMenuA7Repository = DocMenuA7Repository;
        }

        #region MenuA1

        public async Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataAsync(string userid, string username)
        {
            return await _IDocMenuA1Repository.MenuA1InterfaceDataAsync(userid, username);
        }

        public async Task<ModelResponseA1Message> AddDocMenuA1Async(ModelMenuA1 model)
        {

            model.docdate = DateTime.Now;
            model.projecthead = Encoding.UTF8.GetString(Convert.FromBase64String(model.projecthead));
            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.file2name = string.IsNullOrEmpty(model.file2name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file2name);
            model.file3name = string.IsNullOrEmpty(model.file3name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file3name);
            model.file4name = string.IsNullOrEmpty(model.file4name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file4name);
            model.file5name = string.IsNullOrEmpty(model.file5name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file5name);

            //Risk Group 1
            if (!model.riskgroup1)
            {
                model.riskgroup11 = false;
                model.riskgroup12 = false;
                model.riskgroup13 = false;
                model.riskgroup14 = false;
                model.riskgroup15 = false;
                model.riskgroup15other = string.Empty;
            }

            //Risk Group 2
            if (!model.riskgroup1)
            {
                model.riskgroup11 = false;
                model.riskgroup22 = false;
                model.riskgroup23 = false;
                model.riskgroup24 = false;
                model.riskgroup25 = false;
            }

            //Risk Group 3
            if (!model.riskgroup1)
            {
                model.riskgroup31 = false;
                model.riskgroup32 = false;
                model.riskgroup33 = false;
                model.riskgroup34 = false;
                model.riskgroup35 = false;
            }

            //Risk Group 4
            if (!model.riskgroup1)
            {
                model.riskgroup41 = false;
                model.riskgroup42 = false;
                model.riskgroup43 = false;
                model.riskgroup44 = false;
                model.riskgroup45 = false;
            }



            //Member Project 1
            MemberProject member1 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member1projecthead))
            {
                member1.projecthead = model.member1projecthead;
                member1.facultyname = model.member1facultyname;
                member1.workphone = model.member1workphone;
                member1.mobile = model.member1mobile;
                member1.fax = model.member1fax;
                member1.email = model.member1email;
                model.member1json = member1;
            }
            //Member Project 2
            MemberProject member2 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member2projecthead))
            {
                member2.projecthead = model.member2projecthead;
                member2.facultyname = model.member2facultyname;
                member2.workphone = model.member2workphone;
                member2.mobile = model.member2mobile;
                member2.fax = model.member2fax;
                member2.email = model.member2email;
                model.member2json = member2;
            }
            //Member Project 3
            MemberProject member3 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member3projecthead))
            {
                member3.projecthead = model.member3projecthead;
                member3.facultyname = model.member3facultyname;
                member3.workphone = model.member3workphone;
                member3.mobile = model.member3mobile;
                member3.fax = model.member3fax;
                member3.email = model.member3email;
                model.member3json = member3;
            }
            //Member Project 4
            MemberProject member4 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member4projecthead))
            {
                member4.projecthead = model.member4projecthead;
                member4.facultyname = model.member4facultyname;
                member4.workphone = model.member4workphone;
                member4.mobile = model.member4mobile;
                member4.fax = model.member4fax;
                member4.email = model.member4email;
                model.member4json = member4;
            }
            //Member Project 5
            MemberProject member5 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member5projecthead))
            {
                member5.projecthead = model.member5projecthead;
                member5.facultyname = model.member5facultyname;
                member5.workphone = model.member5workphone;
                member5.mobile = model.member5mobile;
                member5.fax = model.member5fax;
                member5.email = model.member5email;
                model.member5json = member5;
            }
            //Member Project 6
            MemberProject member6 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member6projecthead))
            {
                member6.projecthead = model.member6projecthead;
                member6.facultyname = model.member6facultyname;
                member6.workphone = model.member6workphone;
                member6.mobile = model.member6mobile;
                member6.fax = model.member6fax;
                member6.email = model.member6email;
                model.member6json = member6;
            }
            //Member Project 7
            MemberProject member7 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member7projecthead))
            {
                member7.projecthead = model.member7projecthead;
                member7.facultyname = model.member7facultyname;
                member7.workphone = model.member7workphone;
                member7.mobile = model.member7mobile;
                member7.fax = model.member7fax;
                member7.email = model.member7email;
                model.member7json = member7;
            }
            //Member Project 8
            MemberProject member8 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member8projecthead))
            {
                member8.projecthead = model.member8projecthead;
                member8.facultyname = model.member8facultyname;
                member8.workphone = model.member8workphone;
                member8.mobile = model.member8mobile;
                member8.fax = model.member8fax;
                member8.email = model.member8email;
                model.member8json = member8;
            }
            //Member Project 9
            MemberProject member9 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member9projecthead))
            {
                member9.projecthead = model.member9projecthead;
                member9.facultyname = model.member9facultyname;
                member9.workphone = model.member9workphone;
                member9.mobile = model.member9mobile;
                member9.fax = model.member9fax;
                member9.email = model.member9email;
                model.member9json = member9;
            }
            //Member Project 10
            MemberProject member10 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member10projecthead))
            {
                member10.projecthead = model.member10projecthead;
                member10.facultyname = model.member10facultyname;
                member10.workphone = model.member10workphone;
                member10.mobile = model.member10mobile;
                member10.fax = model.member10fax;
                member10.email = model.member10email;
                model.member10json = member10;
            }
            //Member Project 11
            MemberProject member11 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member11projecthead))
            {
                member11.projecthead = model.member11projecthead;
                member11.facultyname = model.member11facultyname;
                member11.workphone = model.member11workphone;
                member11.mobile = model.member11mobile;
                member11.fax = model.member11fax;
                member11.email = model.member11email;
                model.member11json = member11;
            }
            //Member Project 12
            MemberProject member12 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member12projecthead))
            {
                member12.projecthead = model.member12projecthead;
                member12.facultyname = model.member12facultyname;
                member12.workphone = model.member12workphone;
                member12.mobile = model.member12mobile;
                member12.fax = model.member12fax;
                member12.email = model.member12email;
                model.member12json = member12;
            }


            var resp = await _IDocMenuA1Repository.AddDocMenuA1Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file1name, model.file1base64);
                if (!string.IsNullOrEmpty(model.file2base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file2name, model.file2base64);
                if (!string.IsNullOrEmpty(model.file3base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file3name, model.file3base64);
                if (!string.IsNullOrEmpty(model.file4base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file4name, model.file4base64);
                if (!string.IsNullOrEmpty(model.file5base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file5name, model.file5base64);
            }

            return resp;
        }

        #endregion

        #region MenuA1 Edit

        public async Task<ModelMenuA1_InterfaceData> MenuA1InterfaceDataEditAsync(int doc_id, string userid, string username)
        {
            return await _IDocMenuA1Repository.MenuA1InterfaceDataEditAsync(doc_id, userid, username);
        }

        public async Task<ModelMenuA1_FileDownload> GetA1DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA1Repository.GetA1DownloadFileByIdAsync(DocId, Id);
        }

        public async Task<ModelResponseA1Message> UpdateDocMenuA1EditAsync(ModelMenuA1 model)
        {

            model.docdate = DateTime.Now;
            model.projecthead = Encoding.UTF8.GetString(Convert.FromBase64String(model.projecthead));

            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.file2name = (string.IsNullOrEmpty(model.file2base64)) ? model.file2name : GenerateToken.GetGuid() + Path.GetExtension(model.file2name);
            model.file3name = (string.IsNullOrEmpty(model.file3base64)) ? model.file3name : GenerateToken.GetGuid() + Path.GetExtension(model.file3name);
            model.file4name = (string.IsNullOrEmpty(model.file4base64)) ? model.file4name : GenerateToken.GetGuid() + Path.GetExtension(model.file4name);
            model.file5name = (string.IsNullOrEmpty(model.file5base64)) ? model.file5name : GenerateToken.GetGuid() + Path.GetExtension(model.file5name);

            //Risk Group 1
            if (!model.riskgroup1)
            {
                model.riskgroup11 = false;
                model.riskgroup12 = false;
                model.riskgroup13 = false;
                model.riskgroup14 = false;
                model.riskgroup15 = false;
                model.riskgroup15other = string.Empty;
            }

            //Risk Group 2
            if (!model.riskgroup1)
            {
                model.riskgroup11 = false;
                model.riskgroup22 = false;
                model.riskgroup23 = false;
                model.riskgroup24 = false;
                model.riskgroup25 = false;
            }

            //Risk Group 3
            if (!model.riskgroup1)
            {
                model.riskgroup31 = false;
                model.riskgroup32 = false;
                model.riskgroup33 = false;
                model.riskgroup34 = false;
                model.riskgroup35 = false;
            }

            //Risk Group 4
            if (!model.riskgroup1)
            {
                model.riskgroup41 = false;
                model.riskgroup42 = false;
                model.riskgroup43 = false;
                model.riskgroup44 = false;
                model.riskgroup45 = false;
            }

            //Member Project 1
            MemberProject member1 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member1projecthead))
            {
                member1.projecthead = model.member1projecthead;
                member1.facultyname = model.member1facultyname;
                member1.workphone = model.member1workphone;
                member1.mobile = model.member1mobile;
                member1.fax = model.member1fax;
                member1.email = model.member1email;
                model.member1json = member1;
            }
            //Member Project 2
            MemberProject member2 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member2projecthead))
            {
                member2.projecthead = model.member2projecthead;
                member2.facultyname = model.member2facultyname;
                member2.workphone = model.member2workphone;
                member2.mobile = model.member2mobile;
                member2.fax = model.member2fax;
                member2.email = model.member2email;
                model.member2json = member2;
            }
            //Member Project 3
            MemberProject member3 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member3projecthead))
            {
                member3.projecthead = model.member3projecthead;
                member3.facultyname = model.member3facultyname;
                member3.workphone = model.member3workphone;
                member3.mobile = model.member3mobile;
                member3.fax = model.member3fax;
                member3.email = model.member3email;
                model.member3json = member3;
            }
            //Member Project 4
            MemberProject member4 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member4projecthead))
            {
                member4.projecthead = model.member4projecthead;
                member4.facultyname = model.member4facultyname;
                member4.workphone = model.member4workphone;
                member4.mobile = model.member4mobile;
                member4.fax = model.member4fax;
                member4.email = model.member4email;
                model.member4json = member4;
            }
            //Member Project 5
            MemberProject member5 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member5projecthead))
            {
                member5.projecthead = model.member5projecthead;
                member5.facultyname = model.member5facultyname;
                member5.workphone = model.member5workphone;
                member5.mobile = model.member5mobile;
                member5.fax = model.member5fax;
                member5.email = model.member5email;
                model.member5json = member5;
            }
            //Member Project 6
            MemberProject member6 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member6projecthead))
            {
                member6.projecthead = model.member6projecthead;
                member6.facultyname = model.member6facultyname;
                member6.workphone = model.member6workphone;
                member6.mobile = model.member6mobile;
                member6.fax = model.member6fax;
                member6.email = model.member6email;
                model.member6json = member6;
            }
            //Member Project 7
            MemberProject member7 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member7projecthead))
            {
                member7.projecthead = model.member7projecthead;
                member7.facultyname = model.member7facultyname;
                member7.workphone = model.member7workphone;
                member7.mobile = model.member7mobile;
                member7.fax = model.member7fax;
                member7.email = model.member7email;
                model.member7json = member7;
            }
            //Member Project 8
            MemberProject member8 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member8projecthead))
            {
                member8.projecthead = model.member8projecthead;
                member8.facultyname = model.member8facultyname;
                member8.workphone = model.member8workphone;
                member8.mobile = model.member8mobile;
                member8.fax = model.member8fax;
                member8.email = model.member8email;
                model.member8json = member8;
            }
            //Member Project 9
            MemberProject member9 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member9projecthead))
            {
                member9.projecthead = model.member9projecthead;
                member9.facultyname = model.member9facultyname;
                member9.workphone = model.member9workphone;
                member9.mobile = model.member9mobile;
                member9.fax = model.member9fax;
                member9.email = model.member9email;
                model.member9json = member9;
            }
            //Member Project 10
            MemberProject member10 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member10projecthead))
            {
                member10.projecthead = model.member10projecthead;
                member10.facultyname = model.member10facultyname;
                member10.workphone = model.member10workphone;
                member10.mobile = model.member10mobile;
                member10.fax = model.member10fax;
                member10.email = model.member10email;
                model.member10json = member10;
            }
            //Member Project 11
            MemberProject member11 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member11projecthead))
            {
                member11.projecthead = model.member11projecthead;
                member11.facultyname = model.member11facultyname;
                member11.workphone = model.member11workphone;
                member11.mobile = model.member11mobile;
                member11.fax = model.member11fax;
                member11.email = model.member11email;
                model.member11json = member11;
            }
            //Member Project 12
            MemberProject member12 = new MemberProject();
            if (!string.IsNullOrEmpty(model.member12projecthead))
            {
                member12.projecthead = model.member12projecthead;
                member12.facultyname = model.member12facultyname;
                member12.workphone = model.member12workphone;
                member12.mobile = model.member12mobile;
                member12.fax = model.member12fax;
                member12.email = model.member12email;
                model.member12json = member12;
            }

            var resp = await _IDocMenuA1Repository.UpdateDocMenuA1EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file1name, model.file1base64);
                if (!string.IsNullOrEmpty(model.file2base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file2name, model.file2base64);
                if (!string.IsNullOrEmpty(model.file3base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file3name, model.file3base64);
                if (!string.IsNullOrEmpty(model.file4base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file4name, model.file4base64);
                if (!string.IsNullOrEmpty(model.file5base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA1, model.file5name, model.file5base64);
            }

            return resp;
        }

        #endregion

        #region MenuA2
        public async Task<ModelMenuA2_InterfaceData> MenuA2InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA2Repository.MenuA2InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelResponseA2Message> AddDocMenuA2Async(ModelMenuA2 model)
        {

            model.docdate = DateTime.Now;
            model.filename1 = string.IsNullOrEmpty(model.filename1) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.filename1);
            model.filename2 = string.IsNullOrEmpty(model.filename2) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.filename2);

            var resp = await _IDocMenuA2Repository.AddDocMenuA2Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.filename1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA2, model.filename1, model.filename1base64);
                if (!string.IsNullOrEmpty(model.filename2base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA2, model.filename2, model.filename2base64);
            }

            return resp;
        }

        #endregion

        #region MenuA3

        public async Task<ModelMenuA3_InterfaceData> MenuA3InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA3Repository.MenuA3InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuA3ProjectNumberData> GetProjectNumberWithDataA3Async(string project_number)
        {
            return await _IDocMenuA3Repository.GetProjectNumberWithDataA3Async(project_number);
        }

        public async Task<ModelResponseA3Message> AddDocMenuA3Async(ModelMenuA3 model)
        {

            model.docdate = DateTime.Now;
            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA3Repository.AddDocMenuA3Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA3, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region "MenuA3 Edit"

        public async Task<ModelMenuA3_InterfaceData> MenuA3EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuA3Repository.MenuA3EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelMenuA3_FileDownload> GetA3DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA3Repository.GetA3DownloadFileByIdAsync(DocId, Id);
        }

        public async Task<ModelResponseA3Message> UpdateDocMenuA3EditAsync(ModelMenuA3 model)
        {

            model.docdate = DateTime.Now;
            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA3Repository.UpdateDocMenuA3EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA3, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region MenuA4

        public async Task<ModelMenuA4_InterfaceData> MenuA4InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA4Repository.MenuA4InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuA4ProjectNumberData> GetProjectNumberWithDataA4Async(string project_number)
        {
            return await _IDocMenuA4Repository.GetProjectNumberWithDataA4Async(project_number);
        }

        public async Task<ModelResponseA4Message> AddDocMenuA4Async(ModelMenuA4 model)
        {

            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA4Repository.AddDocMenuA4Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA4, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region "MenuA4 Edit"

        public async Task<ModelMenuA4_InterfaceData> MenuA4EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuA4Repository.MenuA4EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelMenuA4_FileDownload> GetA4DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA4Repository.GetA4DownloadFileByIdAsync(DocId, Id);

        }

        public async Task<ModelResponseA4Message> UpdateDocMenuA4EditAsync(ModelMenuA4 model)
        {

            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA4Repository.UpdateDocMenuA4EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA4, model.file1name, model.file1base64);
            }

            return resp;

        }
        #endregion

        #region MenuA5

        public async Task<ModelMenuA5_InterfaceData> MenuA5InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA5Repository.MenuA5InterfaceDataAsync(RegisterId);
        }
        public async Task<ModelMenuA5ProjectNumberData> GetProjectNumberWithDataA5Async(string project_number)
        {
            return await _IDocMenuA5Repository.GetProjectNumberWithDataA5Async(project_number);
        }

        public async Task<ModelResponseA5Message> AddDocMenuA5Async(ModelMenuA5 model)
        {

            model.docdate = DateTime.Now;
            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA5Repository.AddDocMenuA5Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA5, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region "MenuA5 Edit"

        public async Task<ModelMenuA5_InterfaceData> MenuA5EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuA5Repository.MenuA5EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelMenuA5_FileDownload> GetA5DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA5Repository.GetA5DownloadFileByIdAsync(DocId, Id);
        }

        public async Task<ModelResponseA5Message> UpdateDocMenuA5EditAsync(ModelMenuA5 model)
        {
            model.docdate = DateTime.Now;
            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA5Repository.UpdateDocMenuA5EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA5, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region MenuA6

        public async Task<ModelMenuA6_InterfaceData> MenuA6InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA6Repository.MenuA6InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuA6ProjectNumberData> GetProjectNumberWithDataA6Async(string project_number)
        {
            return await _IDocMenuA6Repository.GetProjectNumberWithDataA6Async(project_number);
        }

        public async Task<ModelResponseA6Message> AddDocMenuA6Async(ModelMenuA6 model)
        {

            model.docdate = DateTime.Now;
            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA6Repository.AddDocMenuA6Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA6, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region "MenuA6 Edit"

        public async Task<ModelMenuA6_InterfaceData> MenuA6EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuA6Repository.MenuA6EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelMenuA6_FileDownload> GetA6DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA6Repository.GetA6DownloadFileByIdAsync(DocId, Id);
        }

        public async Task<ModelResponseA6Message> UpdateDocMenuA6EditAsync(ModelMenuA6 model)
        {

            model.docdate = DateTime.Now;
            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA6Repository.UpdateDocMenuA6EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA6, model.file1name, model.file1base64);
            }

            return resp;
        }

        #endregion

        #region MenuA7

        public async Task<ModelMenuA7_InterfaceData> MenuA7InterfaceDataAsync(string RegisterId)
        {
            return await _IDocMenuA7Repository.MenuA7InterfaceDataAsync(RegisterId);
        }

        public async Task<ModelMenuA7ProjectNumberData> GetProjectNumberWithDataA7Async(string project_number)
        {
            return await _IDocMenuA7Repository.GetProjectNumberWithDataA7Async(project_number);
        }

        public async Task<ModelResponseA7Message> AddDocMenuA7Async(ModelMenuA7 model)
        {

            model.file1name = string.IsNullOrEmpty(model.file1name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.file2name = string.IsNullOrEmpty(model.file2name) ? "" : GenerateToken.GetGuid() + Path.GetExtension(model.file2name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA7Repository.AddDocMenuA7Async(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA7, model.file1name, model.file1base64);
                if (!string.IsNullOrEmpty(model.file2base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA7, model.file2name, model.file2base64);
            }

            return resp;
        }

        #endregion

        #region "MenuA7 Edit"

        public async Task<ModelMenuA7_InterfaceData> MenuA7EditInterfaceDataAsync(string UserId, string ProjectNumber)
        {
            return await _IDocMenuA7Repository.MenuA7EditInterfaceDataAsync(UserId, ProjectNumber);
        }

        public async Task<ModelMenuA7_FileDownload> GetA7DownloadFileByIdAsync(int DocId, int Id)
        {
            return await _IDocMenuA7Repository.GetA7DownloadFileByIdAsync(DocId, Id);
        }

        public async Task<ModelResponseA7Message> UpdateDocMenuA7EditAsync(ModelMenuA7 model)
        {
            model.file1name = (string.IsNullOrEmpty(model.file1base64)) ? model.file1name : GenerateToken.GetGuid() + Path.GetExtension(model.file1name);
            model.file2name = (string.IsNullOrEmpty(model.file2base64)) ? model.file2name : GenerateToken.GetGuid() + Path.GetExtension(model.file2name);
            model.conclusiondate = Convert.ToDateTime(model.conclusiondate.Substring(0, 10)).ToString("yyyy-MM-dd");

            var resp = await _IDocMenuA7Repository.UpdateDocMenuA7EditAsync(model);

            if (resp.Status)
            {
                if (!string.IsNullOrEmpty(model.file1base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA7, model.file1name, model.file1base64);
                if (!string.IsNullOrEmpty(model.file2base64)) ServerDirectorys.SaveFileFromBase64(_IEnvironmentConfig.PathDocument, FolderDocument.menuA7, model.file2name, model.file2base64);
            }

            return resp;
        }

        #endregion


    }
}
