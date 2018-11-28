using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Patrons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadPatrons();
        }
        /// <summary>
        /// Gets all the Patron data from the database and loads it into a gridview
        /// </summary>
        public void loadPatrons()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var patrons = new List<object>();
            foreach (var row in svc.SelectAllPatrons())
            {
                var obj = new { row.PatronID, row.PatronName, row.ContactNumber };
                patrons.Add(obj);
            }
            gvPatrons.DataSource = patrons;
            gvPatrons.DataBind();
        }
        /// <summary>
        /// Insert a new Patron into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPatron_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            svc.InsertPatrons(txtPatronName.Text, txtContactNumber.Text);
            lblStatus.Text = "Added a new Patron";
        }
        /// <summary>
        /// Updates a selected patrons information and sends it back to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdatePatron_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            svc.UpdatePatron(txtPatronName.Text, txtContactNumber.Text, lblPatronID.Text);
            lblStatus.Text = $"Updated {lblPatronID.Text}";
            lblPatronID.Text = "";
        }
        /// <summary>
        /// Delete a selected patron from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePatron_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            svc.RemovePatron(lblPatronID.Text);
            lblStatus.Text = $"Removed a patron";
           
        }
        /// <summary>
        /// On select button being pressed pull the data for the selected patron and place it in the corrosponding labels and text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPatrons_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var record = svc.SelectPatronsByID(gvPatrons.SelectedRow.Cells[1].Text);
            lblPatronID.Text = record[0].PatronID.ToString();
            txtPatronName.Text = record[0].PatronName;
            txtContactNumber.Text = record[0].ContactNumber;
        }
    }
}