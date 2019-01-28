using System.Web.Mvc;

namespace NTUHr.Web.Models
{
    public class Hr : Controller
    {
        //甄選編號
        //用人單位
        //甄選類別
        //招聘人數
        //報名起日
        //報名迄日
        //狀態
        //簡則
        //報名
        //符合初選
        //通過初選
        //複選結果
        public string PsnSelectNo { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string PsnCnt { get; set; }
        public string SelectBeginDate { get; set; }
        public string SelectEndDate { get; set; }
        public string SelectTypeName { get; set; }
        public string RegisterDocUri { get; set; }
        public string RegisterUri { get; set; }
        public string Register1 { get; set; }
        public string Register2 { get; set; }
        public string Register3 { get; set; }
    }
}
