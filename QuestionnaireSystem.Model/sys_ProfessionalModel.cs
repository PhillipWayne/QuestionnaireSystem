using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionnaireSystem.Model
{
    [Serializable]
    public class sys_ProfessionalModel
    {
        public sys_ProfessionalModel() { }
        public sys_ProfessionalModel(string professionalName)
        {
            this.professionalName = professionalName;
            this.professionalState = 1;
        }
        public sys_ProfessionalModel(string professionalName, int professionalState)
        {
            this.professionalName = professionalName;
            this.professionalState = professionalState;
        }
        private int professionalId;
        /// <summary>
        /// 专业编号
        /// </summary>
        public int ProfessionalId
        {
            get { return professionalId; }
            set { professionalId = value; }
        }
        private string professionalName;
        /// <summary>
        /// 专业名称
        /// </summary>
        public string ProfessionalName
        {
            get { return professionalName; }
            set { professionalName = value; }
        }
        private int professionalState;
        /// <summary>
        /// 专业状态
        /// </summary>
        public int ProfessionalState
        {
            get { return professionalState; }
            set { professionalState = value; }
        }
    }
}
