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
            HtmlNodeCollection hrValue = hdc.DocumentNode.SelectNodes("tr/td/font/span");
            List<string> listHeader= new List<string>();
            List<Hr> listContent = new List<Hr>();
            foreach (HtmlNode nodeHeader in htnode)
            {
                listHeader.Add(nodeHeader.InnerHtml);
            }
            for(int i = 0; i < hrValue.Count; i+=12)
            {
                if(i+12> hrValue.Count)
                {
                    break;
                }
                Hr hrTemp = new Hr {
                    PsnSelectNo = hrValue[i].InnerHtml,
                    Unit = hrValue[i+1].InnerHtml,
                    Title = hrValue[i+2].InnerHtml,
                    PsnCnt = hrValue[i+3].InnerHtml,
                    SelectBeginDate = hrValue[i+4].InnerHtml,
                    SelectEndDate = hrValue[i+5].InnerHtml,
                    SelectTypeName = hrValue[i+6].InnerHtml,
                    RegisterDocUri = hrValue[i+7].InnerHtml,
                    RegisterUri = hrValue[i+8].InnerHtml,
                    Register1 = hrValue[i+9].InnerHtml,
                    Register2 = hrValue[i+10].InnerHtml,
                    Register3 = hrValue[i+11].InnerHtml,
                };
                listContent.Add(hrTemp);
            }
            //var HrHeader = new Hr()
            //{
            //    PsnSelectNo
            //};
            ViewBag.Headers = listHeader;
            ViewBag.Contents = listContent;
            return View();
        }
    }
}
