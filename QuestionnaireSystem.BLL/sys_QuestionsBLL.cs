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
    public class sys_QuestionsBLL
    {
        private readonly sys_QuestionsDAL questiondDAL = new sys_QuestionsDAL();
        #region 添加问卷调查
        public string AddQuestion(sys_QuestionsModel questionModel)
        {
            JsonMessage json = new JsonMessage();
            int number = questiondDAL.AddQuestion(questionModel);
            if (number > 0)
            {
                json.Success = true;
                json.Msg = "对[" + questionModel.StudentName + "]同学的问卷调查保存成功";
            }
            else if (number == 0)
            {
                json.Success = false;
                json.Msg = "对[" + questionModel.StudentName + "]同学的问卷调查保存失败";
            }
            else
            {
                json.Success = false;
                json.Msg = "对[" + questionModel.StudentName + "]同学的问卷调查保存出错";
            }
            return JsonHelper.ToJson(json);
        }
        #endregion

        #region 查询问卷调查
        public string GetQuestionsByProc(string strWhere, int pageNo, int pageSize)
        {
            int recordTotal = 0;
            DataTable dt = questiondDAL.GetQuestionsByProc(strWhere,pageNo,pageSize,out recordTotal);
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":"+recordTotal+",\"rows\":");
            strJson.Append(JsonHelper.ToJson(dt));
            strJson.Append("}");
            return strJson.ToString();
        }

        public string GetQuestionByQuestionSysId(int questionSysId)
        {
            sys_QuestionsModel questionModel = questiondDAL.GetQuestionByQuestionSysId(questionSysId);
            return JsonHelper.ToJson(questionModel);
        }
        #endregion

        public string GetQuestionsChart(int qid, int num, int year, int pid)
        {
            DataTable dt = questiondDAL.GetQuestionsChart(qid, num, year, pid);
            string json = JsonHelper.ToJson(dt);
            return json;
            //{ "total":3,"rows":["answer1":1]}

        }
    }
}
