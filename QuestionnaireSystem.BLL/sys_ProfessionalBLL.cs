using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuestionnaireSystem.Common;
using QuestionnaireSystem.DAL;
using QuestionnaireSystem.Model;

namespace QuestionnaireSystem.BLL
{
    public class sys_ProfessionalBLL
    {
        private readonly sys_ProfessionalDAL professDAL = new sys_ProfessionalDAL();

        #region 添加专业
        public string AddProfessional(sys_ProfessionalModel professModel)
        {
            JsonMessage json = new JsonMessage();
            int number = professDAL.AddProfessional(professModel);
            if (number > 0)
            {
                json.Success = true;
                json.Msg = "专业添加成功";
            }
            else if (number == 0)
            {
                json.Success = false;
                json.Msg = "专业添加失败";
            }
            else
            {
                json.Success = false;
                json.Msg = "专业添加出错";
            }
            return JsonHelper.ToJson(json);

        }
        #endregion

        #region 修改专业
        public string UpdateProfessional(sys_ProfessionalModel professModel)
        {
            JsonMessage json = new JsonMessage();
            string strWhere = "ProfessionalId !="+professModel.ProfessionalId +" and ProfessionalName ='"+professModel.ProfessionalName+"'";
            if (professDAL.GetProfessionalByWhere(strWhere).Rows.Count == 0)
            {
                int number = professDAL.UpdateProfessional(professModel);
                if (number > 0)
                {
                    json.Success = true;
                    json.Msg = "专业修改成功";
                }
                else if (number == 0)
                {
                    json.Success = false;
                    json.Msg = "专业修改失败";
                }
                else
                {
                    json.Success = false;
                    json.Msg = "专业修改出错";
                }
            }
            else
            {
                json.Success = false;
                json.Msg = "该专业已经存在";
            }
            return JsonHelper.ToJson(json);
        }

        public string UpdateProfessionalState(int professionalId)
        {
            JsonMessage json = new JsonMessage();
            int number = professDAL.UpdateProfessionalStateByProfessId(professionalId);
            if (number > 0)
            {
                json.Success = true;
                json.Msg = "专业修改成功";
            }
            else if (number == 0)
            {
                json.Success = false;
                json.Msg = "专业修改失败";
            }
            else
            {
                json.Success = false;
                json.Msg = "专业修改出错";
            }
            return JsonHelper.ToJson(json);
        }
        #endregion

        #region 查询专业

        public string GetProfessionById(int professId)
        {
            sys_ProfessionalModel professModel = professDAL.GetProfessionalByProfessId(professId);
            return JsonHelper.ToJson(professModel);
        }

        public string GetProfessionalByWhere(string strWhere)
        {
            DataTable dt = professDAL.GetProfessionalByWhere(strWhere);
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":"+dt.Rows.Count+",\"rows\":");
            strJson.Append(JsonHelper.ToJson(dt));
            strJson.Append("}");
            return strJson.ToString();
        }

        /// <summary>
        /// 使用分页存储过程，查询对应分页的专业信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public string GetProfessionalByProc(string strWhere, int pageNo, int pageSize)
        {
            int recordTotal = 0;
            DataTable dt = professDAL.GetProfessionalByProc(strWhere,pageNo,pageSize,out recordTotal);
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{");
            strJson.Append(" \"total\":" + recordTotal + ",\"rows\":");
            strJson.Append(JsonHelper.ToJson(dt));
            strJson.Append("}");
            return strJson.ToString();
        }
        #endregion
    }
}
