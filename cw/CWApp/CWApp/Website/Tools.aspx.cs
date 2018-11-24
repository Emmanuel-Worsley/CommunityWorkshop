using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Tools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        public void loadData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var brands = new List<object>();
            foreach (var row in svc.SelectAllBrands())
            {
                var obj = new { row.BrandID, row.BrandName };
                brands.Add(obj);
            }
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataSource = brands;
            ddlBrand.DataBind();

            var display = new List<object>();
            foreach (var row in svc.SelectAllToolsAndBrand())
            {
                var obj = new { row.ToolID, row.ToolType, row.Active, row.BrandName, row.Comment};
                display.Add(obj);
            }
            gvToolList.DataSource = display;
            gvToolList.DataBind();
          
        }

        protected void gvToolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var record = svc.SelectToolsByID(gvToolList.SelectedRow.Cells[1].Text);
            lblToolID.Text = record[0].ToolID.ToString();
            txtToolType.Text = record[0].ToolType.ToString();
            txtComment.Text = record[0].Comment.ToString();
            ddlBrand.SelectedValue = record[0].BrandID.ToString();
            chkActive.Checked = record[0].Active;
        }

        protected void btnDeleteTool_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            svc.RemoveTool(lblToolID.Text);
            lblStatus.Text = "Tool Deleted";
            loadData();
        }

        protected void btnUpdateTool_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            int trueCheck;
            if (chkActive.Checked)
            {
                trueCheck = 1;
            }
            else
            {
                trueCheck = 0;
            }
            svc.UpdateTool(ddlBrand.SelectedValue, txtToolType.Text, txtComment.Text, trueCheck, lblToolID.Text);
            loadData();
            lblStatus.Text = "Tool Updated";
        }

        protected void btnNewTool_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            int trueCheck;
            if (chkActive.Checked)
            {
                trueCheck = 1;
            }
            else
            {
                trueCheck = 0;
            }
            svc.InsertTool(ddlBrand.SelectedValue, txtToolType.Text, txtComment.Text, trueCheck);
            loadData();
            lblStatus.Text = "New Tool Added";
        }
    }
}