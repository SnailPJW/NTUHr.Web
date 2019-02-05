using HtmlAgilityPack;
using NTUHr.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace NTUHr.Web.Controllers
{
    public class HrController : Controller
    {
        public ActionResult HrDetails()
        {
            ViewBag.DataTable = GetHrDetailsDataTable();
            return View();
        }
        private DataTable GetHrDetailsDataTable()
        {
            DataTable dataTable = new DataTable();
            string xpathContainerRoot = "//*[@id='gvwSel']/tbody";
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://reg.ntuh.gov.tw/WebApplication/Administration/NtuhGeneralSelect/Entry.aspx");
                //1.擷取DOM
                var containerElement = driver.FindElement(By.XPath(xpathContainerRoot));
                //取得表格標頭(只有第一次需要)
                GetTableHeaders(driver, ref dataTable);
                //2.取得分頁表元素
                var paginationElement = GetPagination(containerElement);                
                //3.取得表格內容
                GetTableRows(containerElement, ref dataTable);

                for(int indexPage = 2; indexPage <= 11; indexPage++)
                {
                    //移動到下一頁(第一次結束需要，若為最後一頁則否)
                    Move2NextPage(paginationElement, indexPage);
                    //重新擷取DOM
                    containerElement = driver.FindElement(By.XPath(xpathContainerRoot));
                    //取得分頁表元素
                    paginationElement = GetPagination(containerElement);
                    //取得表格內容
                    GetTableRows(containerElement, ref dataTable);
                }
            }
            return dataTable;
        }
        private IWebElement GetPagination(IWebElement iElement)
        {
            string xpathPagination = "//tr[1]/td/table/tbody/tr";
            var paginationElement = iElement.FindElement(By.XPath(xpathPagination));

            return paginationElement;
        }
        private void Move2NextPage(IWebElement iElement, int pageIndex)
        {
            var pageN = iElement.FindElement(By.XPath($"//td[{pageIndex}]/a"));
            pageN.Click();
        }
        private void GetTableHeaders(IWebDriver iDriver, ref DataTable dataTable)
        {
            string xpathTableHeaders = "//tr[2]/th";
            //表格標頭
            var tableHeaders = iDriver.FindElements(By.XPath(xpathTableHeaders));
            foreach (var header in tableHeaders)
            {
                dataTable.Columns.Add(header.Text); // create columns from th
            }
        }
        private void GetTableRows(IWebElement iElement, ref DataTable dataTable)
        {
            string xpathTableRows = "(tr[td])[position()>1 and position()<last()]";
            //表格內容
            var rootTable = iElement.FindElements(By.XPath(xpathTableRows));

            foreach (var row in rootTable)
            {
                dataTable.Rows.Add(row.FindElements(By.XPath("td")).Select(td => td.Text).ToArray());
            }
        }
        //public ActionResult HrDetails()
        //{
        //    //string htmlUri = "";
        //    MemoryStream ms;
        //    using (WebClient client = new WebClient())
        //    {
        //        client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
        //        //htmlUri = client.DownloadString("https://reg.ntuh.gov.tw/WebApplication/Administration/NtuhGeneralSelect/Entry.aspx");
        //        ms = new MemoryStream(client.DownloadData("https://reg.ntuh.gov.tw/WebApplication/Administration/NtuhGeneralSelect/Entry.aspx"));
        //    }
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.Load(ms, Encoding.UTF8);

        //    // 裝載第一層查詢結果 
        //    HtmlDocument rootDoc = new HtmlDocument();

        //    //XPath 來解讀它 /html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1] 
        //    rootDoc.LoadHtml(doc.DocumentNode.SelectSingleNode("//table[@id='gvwSel']").InnerHtml);

        //    var headers = rootDoc.DocumentNode.SelectNodes("tr/th/font");

        //    DataTable dataTable = new DataTable();
        //    foreach (HtmlNode header in headers)
        //        dataTable.Columns.Add(header.InnerText); // create columns from th
        //    // select rows with td elements 
        //    foreach (var row in rootDoc.DocumentNode.SelectNodes("(//tr[td])[position()>2 and position()<last()-1]"))
        //        dataTable.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

        //    ViewBag.DataTable = dataTable;

        //    return View();
        //}
        //public ActionResult HrDetails()
        //{
        //    WebClient url = new WebClient();
        //    MemoryStream ms = new MemoryStream(url.DownloadData("https://reg.ntuh.gov.tw/WebApplication/Administration/NtuhGeneralSelect/Entry.aspx"));

        //    //string[] strHeader = new string[] { "甄選編號", "用人單位", "甄選類別"
        //    //                                  , "招聘人數", "報名起日", "報名迄日"
        //    //                                  , "狀態", "簡則", "報名"
        //    //                                  , "符合初選", "通過初選", "複選結果" };

        //    // 使用預設編碼讀入 HTML 
        //    HtmlDocument doc = new HtmlDocument();
        //    //doc.Load(ms, Encoding.Default);
        //    doc.Load(ms, Encoding.UTF8);

        //    // 裝載第一層查詢結果 
        //    HtmlDocument hdc = new HtmlDocument();

        //    //XPath 來解讀它 /html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1] 
        //    hdc.LoadHtml(doc.DocumentNode.SelectSingleNode("//table[@id='gvwSel']").InnerHtml);

        //    //徵選編號的Xpath //*[@id="gvwSel_ctl03_lblPsnSelectNo"]
        //    // → //td[(count(//tr/th[.='徵選編號']/preceding-sibling::*)+1) ]
        //    //HtmlNodeCollection columnPsnSelectNo = hdc.DocumentNode.SelectNodes("(//td[(count(//tr/th[.='徵選編號']/preceding-sibling::*)+1)]/font/span)[position()>1 and position()<last()]");
        //    //取得標頭
        //    HtmlNodeCollection htnode = hdc.DocumentNode.SelectNodes("tr/th/font");
        //    //
        //    HtmlNodeCollection hrValue = hdc.DocumentNode.SelectNodes("tr/td/font/span");
        //    List<string> listHeader= new List<string>();
        //    List<Hr> listContent = new List<Hr>();
        //    HtmlNodeCollection[] htmlNodeCollection=new HtmlNodeCollection[htnode.Count];
        //    for (int i = 0; i < htnode.Count; i++)
        //    {
        //        listHeader.Add(htnode[i].InnerHtml);
        //        htmlNodeCollection[i] = hdc.DocumentNode.SelectNodes(
        //            $"(//td[(count(//tr/th[.='{listHeader[i]}']/preceding-sibling::*)+1)]/font/span)[position()>1 and position()<last()]");

        //    }
        //    //for (int j = 0; j < columnPsnSelectNo.Count; j++)
        //    //{
        //    //    Hr hrTemp = new Hr
        //    //    {
        //    //        PsnSelectNo = columnPsnSelectNo[j].InnerHtml
        //    //    };
        //    //    listContent.Add(hrTemp);
        //    //}


        //    //for(int i = 0; i < hrValue.Count; i+=12)
        //    //{
        //    //    if(i+12> hrValue.Count)
        //    //    {
        //    //        break;
        //    //    }
        //    //    Hr hrTemp = new Hr {
        //    //        PsnSelectNo = hrValue[i].InnerHtml,
        //    //        Unit = hrValue[i+1].InnerHtml,
        //    //        Title = hrValue[i+2].InnerHtml,
        //    //        PsnCnt = hrValue[i+3].InnerHtml,
        //    //        SelectBeginDate = hrValue[i+4].InnerHtml,
        //    //        SelectEndDate = hrValue[i+5].InnerHtml,
        //    //        SelectTypeName = hrValue[i+6].InnerHtml,
        //    //        RegisterDocUri = hrValue[i+7].InnerHtml,
        //    //        RegisterUri = hrValue[i+8].InnerHtml,
        //    //        Register1 = hrValue[i+9].InnerHtml,
        //    //        Register2 = hrValue[i+10].InnerHtml,
        //    //        Register3 = hrValue[i+11].InnerHtml,
        //    //    };
        //    //    listContent.Add(hrTemp);
        //    //}
        //    //var HrHeader = new Hr()
        //    //{
        //    //    PsnSelectNo
        //    //};
        //    ViewBag.Headers = listHeader;
        //    ViewBag.Contents = listContent;
        //    return View();
        //}
    }
}
