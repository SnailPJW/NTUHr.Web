using HtmlAgilityPack;
using NTUHr.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace NTUHr.Web.Controllers
{
    public class HrController : Controller
    {
        public ActionResult HrDetails()
        {
            WebClient url = new WebClient();
            MemoryStream ms = new MemoryStream(url.DownloadData("https://reg.ntuh.gov.tw/WebApplication/Administration/NtuhGeneralSelect/Entry.aspx"));

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            //doc.Load(ms, Encoding.Default);
            doc.Load(ms, Encoding.UTF8);

            // 裝載第一層查詢結果 
            HtmlDocument hdc = new HtmlDocument();

            //XPath 來解讀它 /html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1] 
            hdc.LoadHtml(doc.DocumentNode.SelectSingleNode("//table[@id='gvwSel']").InnerHtml);

            //取得標頭
            HtmlNodeCollection htnode = hdc.DocumentNode.SelectNodes("tr/th/font");
            //
            HtmlNodeCollection HrValue = hdc.DocumentNode.SelectNodes("tr/td/font");
            List<string> listHeader= new List<string>();
            foreach (HtmlNode nodeHeader in htnode)
            {
                listHeader.Add(nodeHeader.InnerHtml);
            }
            //var HrHeader = new Hr()
            //{
            //    PsnSelectNo
            //};
            ViewBag.Headers = listHeader;
            return View();
        }
    }
}
