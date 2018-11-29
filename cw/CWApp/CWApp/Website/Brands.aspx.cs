using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Brands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadAllBrands();
        }
        /// <summary>
        /// Loads all Brand data in from the Database
        /// </summary>
        public void loadAllBrands()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient(); // make svc have access to webmethods
            var Brands = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllBrands())// foreach element of data sent
            {
                var obj = new { row.BrandID, row.BrandName };// add into obj the data recieved that you wish to use
                Brands.Add(obj);// add data within obj to the list of objects
            }
            gvBrands.DataSource = Brands;// sets the datasource of the gridview to the list of objects
            gvBrands.DataBind();// binds the data to the gridview
        }
        /// <summary>
        /// On select button pressed pulls the data for the selected brand and places it in the corrosponding labels and text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var record = svc.SelectBrandsByID(gvBrands.SelectedRow.Cells[1].Text);// calls the SelectBrandsByID WebMethod
            lblBrandID.Text = record[0].BrandID.ToString();// change the text of BrandID
            txtBrandName.Text = record[0].BrandName.ToString();// change the text of BrandName
        }
        /// <summary>
        /// Inserts a new brand into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewBrand_Click(object sender, EventArgs e)
        {
            if (txtBrandName.Text.Equals("")) // if BrandName is empty
            {
                lblStatus.CssClass = "alert-danger"; // change the class of status
                lblStatus.Text = "Please fill in Brand Name before adding a new Brand";// change the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                svc.InsertBrands(txtBrandName.Text); // calls the InsertBrands WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = "Added a new Brand";// change the text of status
                loadAllBrands();// calls the loadAllBrands method
            }
        }
        /// <summary>
        /// Updates the selected brand information and sends it back to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {
            if (lblBrandID.Text.Equals(""))// if BrandID is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "No Brand selected";// change the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient(); // make svc have access to WebMethods
                svc.UpdateBrand(txtBrandName.Text, lblBrandID.Text);// calls the UpdateBrands WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = $"Updated {lblBrandID.Text}";// change the text of status
                lblBrandID.Text = "";// change the text of BrandID
                loadAllBrands();// calls the loadAllBrands method
            }
        }
        /// <summary>
        /// Deletes the select brand from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteBrand_Click(object sender, EventArgs e)
        {
            if (lblBrandID.Text.Equals(""))// if BrandID is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "No Brand selected";// change the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                svc.RemoveBrand(lblBrandID.Text); // calls the RemoveBrand WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = $"Removed a Brand";// change the text of status
                lblBrandID.Text = "";// change the text of BrandID
                loadAllBrands();// calls the loadAllBrands method
            }
        }
    }
}