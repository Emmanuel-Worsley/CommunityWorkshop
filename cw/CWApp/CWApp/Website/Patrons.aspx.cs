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
            loadPatrons(); // calls the load patrons method
        }
        /// <summary>
        /// Gets all the Patron data from the database and loads it into a gridview
        /// </summary>
        public void loadPatrons()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient(); // make svc have access to webmethods
            var patrons = new List<object>(); // creates a new list of objects
            foreach (var row in svc.SelectAllPatrons()) // foreach element of data sent
            {
                var obj = new { row.PatronID, row.PatronName, row.ContactNumber }; // add into obj the data recieved that you wish to use
                patrons.Add(obj); // add data within obj to the list of objects
            }
            gvPatrons.DataSource = patrons; // sets the datasource of the gridview to the list of objects
            gvPatrons.DataBind(); // binds the data to the gridview
        }

        /// <summary>
        /// Insert a new Patron into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPatron_Click(object sender, EventArgs e)
        {
            if(txtPatronName.Text.Equals("") || txtContactNumber.Text.Equals("")) // if either patron name or contact number are empty
            {
                lblStatus.CssClass = "alert-danger"; // change the class of status
                lblStatus.Text = "Please fill in both Patron Name and Contact Number before adding a new Patron"; // update the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to webmethods
                svc.InsertPatrons(txtPatronName.Text, txtContactNumber.Text); // calls the insert patron webmethod
                lblStatus.CssClass = "alert-success"; // change the class of status
                lblStatus.Text = $"Added Patron {txtPatronName.Text}"; // change the text of status
                loadPatrons(); // calls the load patron method
            }

        }
        /// <summary>
        /// Updates a selected patrons information and sends it back to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdatePatron_Click(object sender, EventArgs e)
        {
            if(lblPatronID.Text.Equals("")) // if PatronID is empty
            {
                lblStatus.CssClass = "alert-danger"; // change the class of status
                lblStatus.Text = "No Patron selected"; // change the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to webmethods
                svc.UpdatePatron(txtPatronName.Text, txtContactNumber.Text, lblPatronID.Text); // calls the updatePatron webmethod
                lblStatus.CssClass = "alert-success"; // change the class of status
                lblStatus.Text = $"Updated {lblPatronID.Text}"; // change the text of status
                lblPatronID.Text = ""; // change the text of patronID
                loadPatrons();
            }
        }
        /// <summary>
        /// Delete a selected patron from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePatron_Click(object sender, EventArgs e)
        {
            if(lblPatronID.Text.Equals("")) // if PatronID is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "No Patron selected";// change the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                svc.RemovePatron(lblPatronID.Text); // calls the RemovePatron WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = $"Removed a patron";// change the text of status
                lblPatronID.Text = "";// change the text of PatronID
                loadPatrons();// calls the loadPatron method
            }    
        }
        /// <summary>
        /// On select button being pressed pull the data for the selected patron and place it in the corrosponding labels and text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPatrons_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var record = svc.SelectPatronsByID(gvPatrons.SelectedRow.Cells[1].Text);// calls the SelectPatronByID WebMethod and sets record to the result
            lblPatronID.Text = record[0].PatronID.ToString(); // change the text of PatronID
            txtPatronName.Text = record[0].PatronName;// change the text of PatronName
            txtContactNumber.Text = record[0].ContactNumber;// change the text of ContactNumber
        }
    }
}