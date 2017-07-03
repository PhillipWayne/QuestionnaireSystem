using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//model在三层结构里面称之为实体，在mvc设计模式中称为模型
//实体类层，不属于三层结构，是用来进行数据传输的辅助手段
//而MVC中模型，是起到数据格式验证，以及数据提供与操作的作用
namespace QuestionnaireSystem.Model
{
    [Serializable]
    public class sys_UsersModel
    {
        public sys_UsersModel() { }
        /// <summary>
        /// 带参的构造函数，用初始化用户对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">用户密码</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="sid">所属企业编号</param>
        /// <param name="userType">用户类型</param>
        /// <param name="userState">用户状态</param>
        public sys_UsersModel(string userName, string userPassword, string realName, int sid, int userType, int userState)
        {
            this.userName = userName;
            this.userPassword = userPassword;
            this.realName = realName;
            this.subordinateEnterprise = sid;
            this.userType = userType;
            this.userState = userState;
        }
        private int userId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userPassword;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
        private string realName;
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }
        private int subordinateEnterprise;
        /// <summary>
        /// 用户所属企业编号
        /// </summary>
        public int SubordinateEnterprise
        {
            get { return subordinateEnterprise; }
            set { subordinateEnterprise = value; }
        }
        private int userType;
        /// <summary>
        /// 用户类型 1表示管理员 2表示企业用户
        /// </summary>
        public int UserType
        {
            get { return userType; }
            set { userType = value; }
        }
        private int userState;
        /// <summary>
        /// 用户状态 1表示可用 2表示停用
        /// </summary>
        public int UserState
        {
            get { return userState; }
            set { userState = value; }
        }
    }
}
