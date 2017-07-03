using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.BLL;
using QuestionnaireSystem.Model;
namespace QuestionnaireSystem.UI.Ajax
{
    /// <summary>
    /// sys_ProfessionalManager 的摘要说明
    /// </summary>
    public class sys_ProfessionalManager : IHttpHandler
    {
        private readonly sys_ProfessionalBLL professBLL = new sys_ProfessionalBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            string type = context.Request.QueryString["type"].ToString();
            switch (type)
            {
                case "get":
                    result = GetProfessionalByProc(context);
                    break;
                case "add":
                    result = AddProfessional(context);
                    break;
                case "edit":
                    result = GetProfessionalById(context);
                    break;
                case "update":
                    result =UpdateProfessional(context);
                    break;
                case "del":
                    result = UpdateProfessionalState(context);
                    break;
                case "select":
                    result = GetProfessionalByWhere(context);
                    break;
            }
            context.Response.Write(result);
        }
                
        private string AddProfessional(HttpContext context)
        {
            sys_ProfessionalModel professModel = new sys_ProfessionalModel();
            professModel.ProfessionalName = context.Request.Form["pname"].ToString();
            professModel.ProfessionalState = int.Parse(context.Request.Form["pstate"].ToString());
            string result = professBLL.AddProfessional(professModel);
            return result;
        }

        private string UpdateProfessional(HttpContext context)
        {
            sys_ProfessionalModel professModel = new sys_ProfessionalModel();
            professModel.ProfessionalId = int.Parse(context.Request.Form["pid"].ToString());
            professModel.ProfessionalName = context.Request.Form["pname"].ToString();
            professModel.ProfessionalState = int.Parse(context.Request.Form["pstate"].ToString());
            string result = professBLL.UpdateProfessional(professModel);
            return result;

        }

        private string UpdateProfessionalState(HttpContext context)
        {
            int professionalId = int.Parse(context.Request.QueryString["pid"].ToString());
            string result = professBLL.UpdateProfessionalState(professionalId);
            return result;
        }

        private string GetProfessionalById(HttpContext context)
        {
            int professId = int.Parse(context.Request.QueryString["pid"].ToString());
            string result = professBLL.GetProfessionById(professId);
            return result;
        }

        private string GetProfessionalByWhere(HttpContext context)
        {
            string strWhere = "ProfessionalState=1";
            string result = professBLL.GetProfessionalByWhere(strWhere);
            return result;
        }

        private string GetProfessionalByProc(HttpContext context)
        {
            int pageNo = int.Parse(context.Request.QueryString["pno"].ToString());
            int pageSize = int.Parse(context.Request.QueryString["psize"].ToString());
            string result = professBLL.GetProfessionalByProc("1=1",pageNo,pageSize);
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}