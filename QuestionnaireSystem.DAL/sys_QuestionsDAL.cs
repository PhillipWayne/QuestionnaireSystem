using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuestionnaireSystem.Model;
using System.Data.SqlClient;
using System.Data;
using QuestionnaireSystem.DBUility;

namespace QuestionnaireSystem.DAL
{
    public class sys_QuestionsDAL
    {
        #region  添加问卷调查
        /// <summary>
        /// 将问卷调查实体对象存入到数据库中 大于0 成功 0 失败 小于0 出错
        /// </summary>
        /// <param name="questionsModel">实体对象</param>
        /// <returns></returns>
        public int AddQuestion(sys_QuestionsModel questionsModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into sys_Questions (CompanyId,StudentNumber,StudentName,StudentPro,Example1,");
            strSql.Append("Example2,Example3,Example4,Example5,Example6,Example7,Example8,");
            strSql.Append("Example9,Example10,Example11,OtherOpinion,CreateTime,CreateUser ) ");
            strSql.Append(" values (@CompanyId,@StudentNumber,@StudentName,@StudentPro,@Example1,");
            strSql.Append("@Example2,@Example3,@Example4,@Example5,@Example6,@Example7,@Example8,");
            strSql.Append("@Example9,@Example10,@Example11,@OtherOpinion,@CreateTime,@CreateUser) ");
            SqlParameter[] paras ={
                                      new SqlParameter("@CompanyId",SqlDbType.Int,4),
                                      new SqlParameter("@StudentNumber",SqlDbType.VarChar,50),
                                      new SqlParameter("@StudentName",SqlDbType.VarChar,50),
                                      new SqlParameter("@StudentPro",SqlDbType.Int,4),
                                      new SqlParameter("@Example1",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example2",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example3",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example4",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example5",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example6",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example7",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example8",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example9",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example10",SqlDbType.VarChar,10),
                                      new SqlParameter("@Example11",SqlDbType.VarChar,10),
                                      new SqlParameter("@OtherOpinion",SqlDbType.VarChar,500),
                                      new SqlParameter("@CreateTime",SqlDbType.DateTime),
                                      new SqlParameter("@CreateUser",SqlDbType.Int,4)
                                 };
            paras[0].Value = questionsModel.CompanyId;
            paras[1].Value = questionsModel.StudentNumber;
            paras[2].Value = questionsModel.StudentName;
            paras[3].Value = questionsModel.StudentPro;
            paras[4].Value = questionsModel.Example1;
            paras[5].Value = questionsModel.Example2;
            paras[6].Value = questionsModel.Example3;
            paras[7].Value = questionsModel.Example4;
            paras[8].Value = questionsModel.Example5;
            paras[9].Value = questionsModel.Example6;
            paras[10].Value = questionsModel.Example7;
            paras[11].Value = questionsModel.Example8;
            paras[12].Value = questionsModel.Example9;
            paras[13].Value = questionsModel.Example10;
            paras[14].Value = questionsModel.Example11;
            paras[15].Value = questionsModel.OtherOpinion;
            paras[16].Value = questionsModel.CreateDatetime;
            paras[17].Value = questionsModel.CreateUser;
            int number = DBHelper.ExecuteNonQuery(strSql.ToString(),paras);
            return number;
        }
        #endregion

        #region  查询问卷调查
        public sys_QuestionsModel GetQuestionByQuestionSysId(int questionSysId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 QuestionSysId,CompanyId,CompanyName,StudentNumber,StudentName,");
            strSql.Append("StudentPro,ProfessionalName,Example1,Example2,Example3,Example4,Example5,");
            strSql.Append("Example6,Example7,Example8,Example9,Example10,Example11,OtherOpinion,");
            strSql.Append(" CreateTime,CreateUser from v_QuestionsInfo ");
            strSql.Append(" where QuestionSysId=@QuestionSysId");
            SqlParameter[] paras ={
                                      new SqlParameter("@QuestionSysId",SqlDbType.Int,4)
                                  };
            paras[0].Value = questionSysId;
            SqlDataReader reader = DBHelper.ExecuteReader(strSql.ToString(), paras);
            sys_QuestionsModel questionModel = new sys_QuestionsModel();
            if (reader != null)
            {
                while (reader.Read())
                {
                    questionModel.QuestionSysId = reader.GetInt32(0);
                    questionModel.CompanyId = reader.GetInt32(1);
                    questionModel.CompanyName = reader.GetString(2);
                    questionModel.StudentNumber = reader.GetString(3);
                    questionModel.StudentName = reader.GetString(4);
                    questionModel.StudentPro = reader.GetInt32(5);
                    questionModel.ProfessionalName = reader.GetString(6);
                    questionModel.Example1 = reader.GetString(7);
                    questionModel.Example2 = reader.GetString(8);
                    questionModel.Example3 = reader.GetString(9);
                    questionModel.Example4 = reader.GetString(10);
                    questionModel.Example5 = reader.GetString(11);
                    questionModel.Example6 = reader.GetString(12);
                    questionModel.Example7 = reader.GetString(13);
                    questionModel.Example8 = reader.GetString(14);
                    questionModel.Example9 = reader.GetString(15);
                    questionModel.Example10 = reader.GetString(16);
                    questionModel.Example11 = reader.GetString(17);
                    questionModel.OtherOpinion = reader.GetString(18);
                    questionModel.CreateDatetime = reader.GetDateTime(19);
                    questionModel.CreateUser = reader.GetInt32(20);
                }
            }
            reader.Close();
            reader.Dispose();
            return questionModel;
        }
        /// <summary>
        /// 使用分页存储过程查询问卷数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordTotal">总记录数</param>
        /// <returns></returns>
        public DataTable GetQuestionsByProc(string strWhere,int pageNo,int pageSize,out int recordTotal)
        {
            recordTotal = 0;
            string procName = "ProcViewPager";
            SqlParameter[] parameters ={
                                          new SqlParameter("@recordTotal",SqlDbType.Int,4),
                                          new SqlParameter("@viewName",SqlDbType.VarChar,800),
                                          new SqlParameter("@fieldName",SqlDbType.VarChar,800),
                                          new SqlParameter("@keyName",SqlDbType.VarChar,200),
                                          new SqlParameter("@pageSize",SqlDbType.Int,4),
                                          new SqlParameter("@pageNo",SqlDbType.Int,4),
                                          new SqlParameter("@orderString",SqlDbType.VarChar,200),
                                          new SqlParameter("@whereString",SqlDbType.VarChar,800)
                                      };
            parameters[0].Value = recordTotal;
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = " v_QuestionsInfo ";
            parameters[2].Value ="*";
            parameters[3].Value = "QuestionSysId";
            parameters[4].Value = pageSize;
            parameters[5].Value = pageNo;
            parameters[6].Value = "QuestionSysId asc ";
            parameters[7].Value = strWhere;
            DataTable dt = DBHelper.RunProcedure(procName, parameters, out recordTotal);
            return dt;
        }
        #endregion

        public DataTable GetQuestionsChart(int qid, int num, int year, int pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            for (int i = 1; i <= num; i++)
            {
                if (i == num)
                {
                    strSql.Append(" count ( case when Example" + qid + " like '%" + i + "%' then Example" + qid + " else null end ) as answer" + i);
                }
                else
                {
                    strSql.Append(" count ( case when Example" + qid + " like '%" + i + "%' then Example" + qid + " else null end ) as answer" + i + ",");
                }
            }
            strSql.Append(" from sys_Questions where Year(CreateTime)="+year);
            if (pid != 0)
            {
                strSql.Append(" and StudentPro="+pid);
            }
            DataTable dt = DBHelper.Query(strSql.ToString()).Tables[0];
            return dt;

        }
    }
}
