using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionnaireSystem.Model
{
    [Serializable]
    public class sys_QuestionsModel
    {
        public sys_QuestionsModel() { }
        private int questionSysId;
        /// <summary>
        /// 问卷编号
        /// </summary>
        public int QuestionSysId
        {
            get { return questionSysId; }
            set { questionSysId = value; }
        }
        /// <summary>
        /// 所属企业编号
        /// </summary>
        private int companyId;
        /// <summary>
        /// 所属企业编号
        /// </summary>
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        private string companyName;

        /// <summary>
        /// 所属企业名称
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        private string studentNumber;
        /// <summary>
        /// 学生学号
        /// </summary>
        public string StudentNumber
        {
            get { return studentNumber; }
            set { studentNumber = value; }
        }
        private string studentName;
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }
        private int studentPro;
        /// <summary>
        /// 学生专业编号
        /// </summary>
        public int StudentPro
        {
            get { return studentPro; }
            set { studentPro = value; }
        }

        private string professionalName;
        /// <summary>
        /// 所属专业名称
        /// </summary>
        public string ProfessionalName
        {
            get { return professionalName; }
            set { professionalName = value; }
        }

        private string example1;
        /// <summary>
        /// 问题1答案
        /// </summary>
        public string Example1
        {
            get { return example1; }
            set { example1 = value; }
        }
        private string example2;
        /// <summary>
        /// 问题2答案
        /// </summary>
        public string Example2
        {
            get { return example2; }
            set { example2 = value; }
        }
        private string example3;
        /// <summary>
        /// 问题3答案
        /// </summary>
        public string Example3
        {
            get { return example3; }
            set { example3 = value; }
        }
        private string example4;
        /// <summary>
        /// 问题4答案
        /// </summary>
        public string Example4
        {
            get { return example4; }
            set { example4 = value; }
        }
        private string example5;
        /// <summary>
        /// 问题5答案
        /// </summary>
        public string Example5
        {
            get { return example5; }
            set { example5 = value; }
        }
        private string example6;
        /// <summary>
        /// 问题6答案
        /// </summary>
        public string Example6
        {
            get { return example6; }
            set { example6 = value; }
        }
        private string example7;
        /// <summary>
        /// 问题7答案
        /// </summary>
        public string Example7
        {
            get { return example7; }
            set { example7 = value; }
        }
        private string example8;
        /// <summary>
        /// 问题8答案
        /// </summary>
        public string Example8
        {
            get { return example8; }
            set { example8 = value; }
        }
        private string example9;
        /// <summary>
        /// 问题9答案
        /// </summary>
        public string Example9
        {
            get { return example9; }
            set { example9 = value; }
        }
        private string example10;
        /// <summary>
        /// 问题10答案
        /// </summary>
        public string Example10
        {
            get { return example10; }
            set { example10 = value; }
        }
        private string example11;
        /// <summary>
        /// 问题11答案
        /// </summary>
        public string Example11
        {
            get { return example11; }
            set { example11 = value; }
        }
        private string otherOpinion;
        /// <summary>
        /// 其他意见
        /// </summary>
        public string OtherOpinion
        {
            get { return otherOpinion; }
            set { otherOpinion = value; }
        }
        private DateTime createDatetime;
        /// <summary>
        /// 填写时间
        /// </summary>
        public DateTime CreateDatetime
        {
            get { return createDatetime; }
            set { createDatetime = value; }
        }
        private int createUser;
        /// <summary>
        /// 填写人
        /// </summary>
        public int CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

    }
}
