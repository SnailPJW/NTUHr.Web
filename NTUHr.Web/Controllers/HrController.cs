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
                var arrColumn = row.FindElements(By.XPath("td"))
                    .Select(td => td.Text).ToArray();
                if (arrColumn.Contains("資訊室"))
                {
                    dataTable.Rows.Add(arrColumn);
                }
            }
        }
    }
}
