using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadTools();
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
        }

        protected void gvShowReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                object test = e.Row.DataItem;
                if(test.Equals(false))
                {
                    e.Row.Cells[Convert.ToInt32(e.Row.DataItem)].Text = "1";
                }
                else
                {
                    e.Row.Cells[Convert.ToInt32(e.Row.DataItem)].Text = "0";
                }
                /*
                e.Row.Cells[0].Text = "Tool Type";
                e.Row.Cells[1].Text = "Comment";
                e.Row.Cells[2].Text = "Active";
                */
            }
        }
    }
}