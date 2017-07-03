using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.Model;
using QuestionnaireSystem.BLL;

namespace QuestionnaireSystem.UI.Ajax
{
    /// <summary>
    /// sys_QuestionsManager 的摘要说明
    /// </summary>
    public class sys_QuestionsManager : IHttpHandler
    {
        private readonly sys_QuestionsBLL questionBLL = new sys_QuestionsBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["type"].ToString();
            string result = "";
            switch (type)
            {
                case "add":
                    result = AddQuestion(context);
                    break;
                case "get":
                    result = GetQuestionsByProc(context);
                    break;
                case "select":
                    result = GetQuestionByQuestionSysId(context);
                    break;
                case "chart":
                    result = GetQuestionsChart(context);
                    break;

            }
            context.Response.Write(result);
        }

        private string GetQuestionsChart(HttpContext context)
        {
            int qid = int.Parse(context.Request.Form["qid"].ToString());
            int num = int.Parse(context.Request.Form["num"].ToString());
            int year = int.Parse(context.Request.Form["year"].ToString());
            int pid = int.Parse(context.Request.Form["pid"].ToString());
            string result = questionBLL.GetQuestionsChart(qid, num, year, pid);
            return result;
        }

        private string AddQuestion(HttpContext context)
        {
            try
            {
                sys_QuestionsModel questionModel = new sys_QuestionsModel();
                questionModel.CompanyId = int.Parse(context.Request.Form["cid"].ToString());
                questionModel.StudentNumber = context.Request.Form["stunumber"].ToString();
                questionModel.StudentName = context.Request.Form["stuname"].ToString();
                questionModel.StudentPro = int.Parse(context.Request.Form["stupro"].ToString());
                questionModel.Example1 = context.Request.Form["Example1"].ToString();
                questionModel.Example2 = context.Request.Form["Example2"].ToString();
                questionModel.Example3 = context.Request.Form["Example3"].ToString();
                questionModel.Example4 = context.Request.Form["Example4"].ToString();
                questionModel.Example5 = context.Request.Form["Example5"].ToString();
                questionModel.Example6 = context.Request.Form["Example6"].ToString();
                questionModel.Example7 = context.Request.Form["Example7"].ToString();
                questionModel.Example8 = context.Request.Form["Example8"].ToString();
                questionModel.Example9 = context.Request.Form["Example9"].ToString();
                questionModel.Example10 = context.Request.Form["Example10"].ToString();
                questionModel.Example11 = context.Request.Form["Example11"].ToString();
                questionModel.OtherOpinion = context.Request.Form["suggestion"].ToString();
                questionModel.CreateDatetime = DateTime.Now;
                questionModel.CreateUser = int.Parse(context.Request.Form["uid"].ToString());
                string result = questionBLL.AddQuestion(questionModel);
                return result;
            }
            catch (Exception)
            {
                return "{\"Success\":\"False\",\"Msg\":\"问卷调查数据格式不正确\"}";
                throw;
            }
        }

        private string GetQuestionByQuestionSysId(HttpContext context)
        {
            int qid = int.Parse(context.Request.QueryString["qid"].ToString());
            string result = questionBLL.GetQuestionByQuestionSysId(qid);
            return result;
        }

        private string GetQuestionsByProc(HttpContext context)
        {
            int pageNo = int.Parse(context.Request.QueryString["pno"].ToString());
            int pageSize = int.Parse(context.Request.QueryString["psize"].ToString());
            DateTime NowTime = DateTime.Now;
            int Year = DateTime.Now.Year;
            string strWhere = "1=1";

            //2017-05-04 ==>2016-06-01~2016-07-31
            //2016-09-03 ==>

            //如果当前时间小于本年度6月1日 查询上一年 2017-05-04
            if (NowTime < DateTime.Parse(Year + "-06-01 00:00:00") && NowTime > DateTime.Parse((Year - 1) + "-07-31 23:59:59"))
            {
                strWhere += " and YEAR(CreateTime)=" + (Year - 1);
            }
            else if (NowTime > DateTime.Parse(Year + "-07-31 23:59:59") && NowTime < DateTime.Parse((Year + 1) + "-06-01 00:00:00"))
            {
                //如果当前时间大于本年度七月31日 查询本年 2017-08-03
                strWhere += " and YEAR(CreateTime)=" +Year;
            }
            else
            {
                strWhere += " and YEAR(CreateTime)="+Year;
            }


            string result = questionBLL.GetQuestionsByProc(strWhere, pageNo, pageSize);
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