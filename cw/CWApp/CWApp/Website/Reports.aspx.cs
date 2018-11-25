using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Reports : System.Web.UI.Page
    {
        public static string export;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadTools();
                loadPatronData();
            }
        }

        public void loadTools()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var tools = new List<object>();
            foreach (var row in svc.SelectAllTools())
            {
                var obj = new { row.ToolID, row.ToolType };
                tools.Add(obj);
            }
            ddlTools.DataValueField = "ToolID";
            ddlTools.DataTextField = "ToolType";
            ddlTools.DataSource = tools;
            ddlTools.DataBind();
        }

        public void loadPatronData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var patrons = new List<object>();
            foreach (var row in svc.SelectAllPatrons())
            {
                var obj = new { row.PatronName, row.PatronID };
                patrons.Add(obj);
            }
            ddlPatrons.DataValueField = "PatronID";
            ddlPatrons.DataTextField = "PatronName";
            ddlPatrons.DataSource = patrons;
            ddlPatrons.DataBind();
        }

        public void createCSV(List<object> data)

        {
            var csv = new StringBuilder();
            for (int i = 0; i < data.Count; i++)
            {
                csv.Append(data[i].ToString());
                if(i == 0)
                {
                    csv.Remove(csv.Length - (data[i].ToString().Length), 2); // removes first 2 
                }
                else
                {
                    csv.Remove(csv.Length - (data[i].ToString().Length - 1), 1); // removes first 2 
                    csv.Replace('{', ',');
                }
                csv.Remove(csv.Length - 2, 2); // removes last 2 from the csv
            }
            csv.Replace(", ", ",");
            export = csv.ToString();
        }

        public void exportCsv(string csv)
        {
            string attachment = "attachment; fileName=Testing.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            /*
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "Public");
            */
            HttpContext.Current.Response.Write(csv);
            HttpContext.Current.Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnCheckedOutTools_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var tools = new List<object>();
            foreach (var row in svc.SelectAllCheckedoutTools())
            {
                var obj = new { row.ToolType, row.Comment, row.Active };
                tools.Add(obj);
            }
            gvShowReports.DataSource = tools;
            gvShowReports.DataBind();
        }

        protected void btnActiveTools_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var tools = new List<object>();
            if (chkActiveCheck.Checked)
            {
                foreach (var row in svc.SelectAllActiveTools("1"))
                {
                    var obj = new { row.ToolID, row.ToolType, row.Comment, row.Active };
                    tools.Add(obj);
                }
            }
            else
            {
                foreach (var row in svc.SelectAllActiveTools("0"))
                {
                    var obj = new { row.ToolID, row.ToolType, row.Comment, row.Active };
                    tools.Add(obj);
                }
            }
            gvShowReports.DataSource = tools;
            gvShowReports.DataBind();
            createCSV(tools);
        }

        protected void ddlTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var tools = new List<object>();
            foreach(var row in svc.loadHistoryOfTool(ddlTools.SelectedValue))
            {
                var obj = new { row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn, row.PatronName };
                tools.Add(obj);
            }
            gvShowReports.DataSource = tools;
            gvShowReports.DataBind();
        }

        protected void ddlPatrons_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var patrons = new List<object>();
            foreach (var row in svc.loadHistoryOfPatron(ddlPatrons.SelectedValue))
            {
                var obj = new { row.PatronName, row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn };
                patrons.Add(obj);
            }
            gvShowReports.DataSource = patrons;
            gvShowReports.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            exportCsv(export);
        }
    }
}