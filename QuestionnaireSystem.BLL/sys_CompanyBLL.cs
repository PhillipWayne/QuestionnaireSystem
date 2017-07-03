using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuestionnaireSystem.DAL;
using QuestionnaireSystem.Model;
using QuestionnaireSystem.Common;
using System.Data;

namespace QuestionnaireSystem.BLL
{
   public  class sys_CompanyBLL
    {
       private readonly sys_CompanyDAL companyDAL = new sys_CompanyDAL();

       #region  添加公司
       public string AddCompany(sys_CompanyModel companyModel)
       {
           JsonMessage json = new JsonMessage();
           int number = companyDAL.AddCompany(companyModel);
           if (number > 0)
           {
               json.Success = true;
               json.Msg = "企业信息添加成功";
           }
           else if (number == 0)
           {
               json.Success = false;
               json.Msg = "企业信息添加失败";
           }
           else
           {
               json.Success = false;
               json.Msg = "企业信息添加出错";
           }
           return JsonHelper.ToJson(json);
       }
       #endregion

       #region 修改公司信息

       public string UpdateCompany(sys_CompanyModel companyModel)
       {
           JsonMessage json = new JsonMessage();
           string strWhere = "CompanyId!=" + companyModel.CompanyId + "  and CompanyName  ='" + companyModel.CompanyName + "'";
           if (companyDAL.GetCompanyByWhere(strWhere).Rows.Count == 0)
           {
               int number = companyDAL.UpdateCompany(companyModel);
               if (number > 0)
               {
                   json.Success = true;
                   json.Msg = "企业信息修改成功";
               }
               else if (number == 0)
               {
                   json.Success = false;
                   json.Msg = "企业信息修改失败";
               }
               else
               {
                   json.Success = false;
                   json.Msg = "企业信息修改出错";
               }
           }
           else
           {
               json.Success = false;
               json.Msg = "该企业名已存在";
           }
           return JsonHelper.ToJson(json);
       }

       public string UpdateCompanyState(int companyId)
       {
           JsonMessage json = new JsonMessage();
           int number = companyDAL.UpdateCompanyStateByCompanyId(companyId);
           if (number > 0)
           {
               json.Success = true;
               json.Msg = "企业状态操作成功";
           }
           else if (number == 0)
           {
               json.Success = false;
               json.Msg = "企业状态操作失败";
           }
           else
           {
               json.Success = false;
               json.Msg = "企业状态操作出错";
           }
           return JsonHelper.ToJson(json);
       }
       #endregion

       #region 查询公司

       public string GetCompanyByCompanyId(int companyId)
       {
           sys_CompanyModel companyModel = companyDAL.GetCompanyByCompanyId(companyId);
           return JsonHelper.ToJson(companyModel);
       }

       public string GetCompanyByWhere(string strWhere)
       {
           DataTable dt = companyDAL.GetCompanyByWhere(strWhere);
           StringBuilder strJson = new StringBuilder();
           strJson.Append("{\"total\":"+dt.Rows.Count+",\"rows\":");
           strJson.Append(JsonHelper.ToJson(dt));
           strJson.Append("}");
           return strJson.ToString();
       }

       public string GetCompanyByProc(string strWhere, int pageNo, int pageSize)
       {
           int recordTotal = 0;
           DataTable dt = companyDAL.GetCompanyByProc(strWhere, pageNo, pageSize, out recordTotal);
           StringBuilder strJson = new StringBuilder();
           strJson.Append("{\"total\":"+recordTotal+",\"rows\":");
           strJson.Append(JsonHelper.ToJson(dt));
           strJson.Append("}");
           return strJson.ToString();
       }
       #endregion

    }
}
