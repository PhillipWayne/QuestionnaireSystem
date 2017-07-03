using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionnaireSystem.Model
{
    [Serializable]
    public class sys_CompanyModel
    {
        public sys_CompanyModel() { }
        public sys_CompanyModel(string companyName,string companyAddress,string companyPerson,string companyTelPhone,string companyOthInfo)
        {
            this.companyName = companyName;
            this.companyAddress = companyAddress;
            this.companyPerson = companyPerson;
            this.companyTelPhone = companyTelPhone;
            this.companyOthInfo = companyOthInfo;
            this.companyState = 1;
        }
        public sys_CompanyModel(string companyName, string companyAddress, string companyPerson, string companyTelPhone, string companyOthInfo,int companyState)
        {
            this.companyName = companyName;
            this.companyAddress = companyAddress;
            this.companyPerson = companyPerson;
            this.companyTelPhone = companyTelPhone;
            this.companyOthInfo = companyOthInfo;
            this.companyState = companyState;
        }
        private int companyId;
        /// <summary>
        /// 企业编号
        /// </summary>
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
        private string companyName;
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        private string companyAddress;
        /// <summary>
        /// 企业地址
        /// </summary>
        public string CompanyAddress
        {
            get { return companyAddress; }
            set { companyAddress = value; }
        }
        private string companyPerson;
        /// <summary>
        /// 企业联系人
        /// </summary>
        public string CompanyPerson
        {
            get { return companyPerson; }
            set { companyPerson = value; }
        }
        private string companyTelPhone;
        /// <summary>
        /// 企业联系方式
        /// </summary>
        public string CompanyTelPhone
        {
            get { return companyTelPhone; }
            set { companyTelPhone = value; }
        }
        private string companyOthInfo;
        /// <summary>
        /// 企业其他信息
        /// </summary>
        public string CompanyOthInfo
        {
            get { return companyOthInfo; }
            set { companyOthInfo = value; }
        }
        private int companyState;
        /// <summary>
        /// 企业状态
        /// </summary>
        public int CompanyState
        {
            get { return companyState; }
            set { companyState = value; }
        }
    }
}
