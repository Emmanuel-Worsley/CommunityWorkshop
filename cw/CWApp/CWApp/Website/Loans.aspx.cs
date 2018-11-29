using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWApp.Website
{
    public partial class Loans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)// if the page is not being loaded in response to a post back
            {
                loadAll();//calls the loadAll method
            }
        }
        /// <summary>
        /// Used to load all data required for the page
        /// </summary>
        public void loadAll()
        {
            loadLoanData();// calls the loadLoanData method
            loadPatronData();// calls the loadPatronData method
            loadToolData();// calls the loadToolData method
        }
        /// <summary>
        /// Loads all tool data
        /// </summary>
        public void loadLoanData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var loans = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllLoanData())// foreach element of data sent
            {
                var obj = new { row.LoanID, row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn, row.PatronName, row.StaffName };// add into obj the data recieved that you wish to use
                loans.Add(obj);// add data within obj to the list of objects
            }
            gvLoans.DataSource = loans;// sets the datasource of the gridview to the list of objects
            gvLoans.DataBind();// binds the data to the gridview
        }
        /// <summary>
        /// Loads all loan data
        /// </summary>
        public void loadPatronData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var patrons = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllPatrons())// foreach element of data sent
            {
                var obj = new { row.PatronName, row.PatronID };// add into obj the data recieved that you wish to use
                patrons.Add(obj);// add data within obj to the list of objects
            }
            ddlPatrons.DataValueField = "PatronID";// sets where the value of each item in the list comes from
            ddlPatrons.DataTextField = "PatronName";// sets where the value of each item in the list comes from
            ddlPatrons.DataSource = patrons;// sets the datasource of the drop-down list to the list of objects
            ddlPatrons.DataBind();// binds the data to the drop-down list
        }
        /// <summary>
        /// Loads all tool data
        /// </summary>
        public void loadToolData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var tools = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllActiveToolsAndNotOnLoan())// foreach element of data sent
            {
                var obj = new { row.ToolID, row.ToolType};// add into obj the data recieved that you wish to use
                tools.Add(obj);// add data within obj to the list of objects        
            }
            ddlTools.DataValueField = "ToolID";// sets where the value of each item in the list comes from
            ddlTools.DataTextField = "ToolType";// sets where the value of each item in the list comes from
            ddlTools.DataSource = tools;// sets the datasource of the drop-down list to the list of objects
            ddlTools.DataBind();// binds the data to the drop-down list

        }
        /// <summary>
        /// Inserts a new loan into the Loans table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewLoan_Click(object sender, EventArgs e)
        {
            if(txtWorkstation.Text.Equals("")) // if WorkStation is empty
            {
                lblStatus.CssClass = "alert-danger"; // change the class of status
                lblStatus.Text = "Please fill in which workstation this is being loaned to"; // update the text of status
            }
            else
            {
                var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
                var employee = svc.SelectEmployeesByName(Context.User.Identity.Name); // Calls the SelectEmployeesByName WebMethod and sets employee to the result
                svc.InsertLoan(ddlPatrons.SelectedValue, ddlTools.SelectedValue, employee[0].EmployeeID.ToString(), txtWorkstation.Text); // Calls the InsertLoan WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = "Created a new Loan";// update the text of status
                loadAll(); // calls the loadAll method
            }
            
        }
        /// <summary>
        /// On select button being pressed pull the data for the selected loan and place it in the corrosponding labels and text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLoans_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var record = svc.SelectLoanByID(gvLoans.SelectedRow.Cells[1].Text); // Calls the SelectLoanByID WebMethod and sets record to results
            lblLoanID.Text = record[0].LoanID.ToString(); // change the text of LoanID
            txtWorkstation.Text = record[0].WorkStation; // change the text of Workstation
            ddlPatrons.SelectedValue = record[0].PatronID.ToString(); // change the selected text in ddlPatrons
            //ddlTools.SelectedValue = record[0].ToolID.ToString();
        }
        /// <summary>
        /// Deletes a laon from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteLoan_Click(object sender, EventArgs e)
        {
            if(lblLoanID.Text.Equals("")) // if LoanID is empty
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "No loan selected";// update the text of status
            }
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            svc.RemoveLoan(lblLoanID.Text);// calls the RemoveLoan WebMethod
            lblStatus.Text = "Deleted a loan";// update the text of status
            loadAll();// calls the loadAll  method
            lblLoanID.Text = "";// update the text of LoanID
        }
        /// <summary>
        /// returns a loan based on ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoanReturn_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            if (!gvLoans.SelectedRow.Cells[6].Text.Equals(DateTime.MinValue.ToString())) // if the sixth row of the selected row from the gridview is not equal to the minimum value of a datatime
            {
                lblStatus.CssClass = "alert-danger";// change the class of status
                lblStatus.Text = "Loan has already been returned or no loan selected.";// update the text of status
            }
            else
            {
                svc.ReturnLoan(lblLoanID.Text);// calls the ReturnLoan WebMethod
                lblStatus.CssClass = "alert-success";// change the class of status
                lblStatus.Text = "Returned Loan " + lblLoanID.Text;
                loadAll();// calls the loadAll  method
                lblLoanID.Text = "";// update the text of status
            }
        }
    }
}