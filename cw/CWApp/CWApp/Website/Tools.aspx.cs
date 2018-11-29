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
            if (!IsPostBack)// if the page is not being loaded in response to a post back
            {
                loadData(); // calls the loadData method
            }
        }
        /// <summary>
        /// Loads in all data used on this page
        /// </summary>
        public void loadData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var brands = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllBrands())// foreach element of data sent
            {
                var obj = new { row.BrandID, row.BrandName };// add into obj the data recieved that you wish to use
                brands.Add(obj);// add data within obj to the list of objects
            }
            ddlBrand.DataTextField = "BrandName";// sets where the value of each item in the list comes from
            ddlBrand.DataValueField = "BrandID";// sets where the value of each item in the list comes from
            ddlBrand.DataSource = brands;// sets the datasource of the drop-down list to the list of objects
            ddlBrand.DataBind();// binds the data to the drop-down list

            var display = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllToolsAndBrand())// foreach element of data sent
            {
                var obj = new { row.ToolID, row.ToolType, row.Active, row.BrandName, row.Comment};// add into obj the data recieved that you wish to us
                display.Add(obj);// add data within obj to the list of objects
            }
            gvToolList.DataSource = display;// sets the datasource of the gridview to the list of objects
            gvToolList.DataBind();// binds the data to the gridview

        }
        /// <summary>
        /// On select button being pressed pull the data for the selected tool and place it in the corrosponding labels, text boxes, ddl, and checks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvToolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var record = svc.SelectToolsByID(gvToolList.SelectedRow.Cells[1].Text); // calls the SelectToolsByID WebMethod and sets record to the results
            lblToolID.Text = record[0].ToolID.ToString(); // change the text of ToolID
            txtToolType.Text = record[0].ToolType.ToString();// change the text of ToolType
            txtComment.Text = record[0].Comment.ToString();// change the text of Comment
            ddlBrand.SelectedValue = record[0].BrandID.ToString(); // change the select value in ddlBrand
            chkActive.Checked = record[0].Active; // change if active is checked or not
        }
        /// <summary>
        /// Deletes a tool from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteTool_Click(object sender, EventArgs e)
        {
            if (lblToolID.Text.Equals("")) // if ToolID is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "There is no tool selected";// update the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                svc.RemoveTool(lblToolID.Text);
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = "Tool Removed";// update the text of status
                loadData();// calls the loadData method
                lblToolID.Text = "";// update the text of ToolID
            }
        }
        /// <summary>
        /// Updates a tool and reloads the gridview with changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateTool_Click(object sender, EventArgs e)
        {
            if (lblToolID.Text.Equals(""))
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "There is no tool selected";// update the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                int trueCheck; // make an int called true check
                if (chkActive.Checked) // if chkActive is checked
                {
                    trueCheck = 1; // set trueCheck to 1
                }
                else
                {
                    trueCheck = 0; // sets trueCheck to 0
                }
                svc.UpdateTool(ddlBrand.SelectedValue, txtToolType.Text, txtComment.Text, trueCheck, lblToolID.Text);// Calls UpdateTool WebMethod
                loadData();// Calls the loadData method
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = $"Updated Tool with ID {lblToolID.Text}";// update the text of status
                lblToolID.Text = "";// update the text of ToolID
            }

        }
        /// <summary>
        /// Inserts a new tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewTool_Click(object sender, EventArgs e)
        {
            if(txtToolType.Text.Equals("") || txtComment.Text.Equals("")) // if ToolType or Comment is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "Please fill in both Comment and Tool Type before adding a new Tool";// update the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                int trueCheck;// make an int called true check
                if (chkActive.Checked)// if chkActive is checked
                {
                    trueCheck = 1;// set trueCheck to 1
                }
                else
                {
                    trueCheck = 0;// sets trueCheck to 0
                }
                svc.InsertTool(ddlBrand.SelectedValue, txtToolType.Text, txtComment.Text, trueCheck);// Calls the InsertTool WebMethod
                loadData();// Calls the loadData method
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = "New Tool Added";// update the text of status
            }
        }
    }
}