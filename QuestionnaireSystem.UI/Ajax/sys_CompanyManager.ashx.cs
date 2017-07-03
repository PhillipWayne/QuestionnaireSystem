using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.BLL;
using QuestionnaireSystem.Model;
namespace QuestionnaireSystem.UI.Ajax
{
    /// <summary>
    /// sys_CompanyManager 的摘要说明
    /// </summary>
    public class sys_CompanyManager : IHttpHandler
    {
        private readonly sys_CompanyBLL companyBLL = new sys_CompanyBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            string type = context.Request.QueryString["type"].ToString();
            switch (type)
            {
                case "get":
                    result = GetCompanyByProc(context);
                    break;
                case "del":
                    result = UpdateCompanyState(context);
                    break;
                case "add":
                    result = AddCompany(context);
                    break;
                case "edit":
                    result = GetCompanyById(context);
                    break;
                case "update":
                    result = UpdateCompany(context);
                    break;
                case "select":
                    result = GetCompanyByWhere(context);
                    break;
            }

            context.Response.Write(result);
        }
        
        private string AddCompany(HttpContext context)
        {
            try
            {
                sys_CompanyModel companyModel = new sys_CompanyModel();
                companyModel.CompanyName = context.Request.Form["cname"].ToString();
                companyModel.CompanyAddress = context.Request.Form["caddress"].ToString();
                companyModel.CompanyPerson = context.Request.Form["cperson"].ToString();
                companyModel.CompanyTelPhone = context.Request.Form["ctelphone"].ToString();
                companyModel.CompanyOthInfo = context.Request.Form["cothinfo"].ToString();
                companyModel.CompanyState = int.Parse(context.Request.Form["cstate"].ToString());
                string result = companyBLL.AddCompany(companyModel);
                return result;
            }
            catch (Exception)
            {
                return "{\"Success\":\"False\",\"Msg\":\"企业信息中类型转换出错\"}";
                throw;
            }
        }

        private string UpdateCompany(HttpContext context)
        {
            sys_CompanyModel companyModel = new sys_CompanyModel();
            companyModel.CompanyId = int.Parse(context.Request.Form["cid"].ToString());
            companyModel.CompanyName = context.Request.Form["cname"].ToString();
            companyModel.CompanyAddress = context.Request.Form["caddress"].ToString();
            companyModel.CompanyPerson = context.Request.Form["cperson"].ToString();
            companyModel.CompanyTelPhone = context.Request.Form["ctelphone"].ToString();
            companyModel.CompanyOthInfo = context.Request.Form["cothinfo"].ToString();
            companyModel.CompanyState = int.Parse(context.Request.Form["cstate"].ToString());
            string result = companyBLL.UpdateCompany(companyModel);
            return result;
        }

        private string UpdateCompanyState(HttpContext context)
        {
            int companyId = int.Parse(context.Request.QueryString["cid"].ToString());
            string result = companyBLL.UpdateCompanyState(companyId);
            return result;
        }

        private string GetCompanyById(HttpContext context)
        {
            int companyId = int.Parse(context.Request.QueryString["cid"].ToString());
            string result = companyBLL.GetCompanyByCompanyId(companyId);
            return result;
        }

        private string GetCompanyByWhere(HttpContext context)
        {
            string strWhere = "CompanyState=1";
            string result = companyBLL.GetCompanyByWhere(strWhere);
            return result;
        }

        private string GetCompanyByProc(HttpContext context)
        {
            int pageNo = int.Parse(context.Request.QueryString["pno"].ToString());
            int pageSize = int.Parse(context.Request.QueryString["psize"].ToString());
            string result = companyBLL.GetCompanyByProc("1=1", pageNo, pageSize);
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