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
            if (!IsPostBack)
            {
                loadAll();
            }
        }

        public void loadAll()
        {
            loadLoanData();
            loadPatronData();
            loadToolData();
        }

        public void loadLoanData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var loans = new List<object>();
            foreach (var row in svc.SelectAllLoanData())
            {
                var obj = new { row.LoanID, row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn, row.PatronName, row.StaffName};
                loans.Add(obj);
            }
            gvLoans.DataSource = loans;
            gvLoans.DataBind();
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

        public void loadToolData()
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

        protected void btnNewLoan_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var employee = svc.SelectEmployeesByName(Context.User.Identity.Name);
            svc.InsertLoan(ddlPatrons.SelectedValue, ddlTools.SelectedIndex.ToString(), employee[0].EmployeeID.ToString(), txtWorkstation.Text);
            lblStatus.Text = "Created a new Loan";
            loadAll();
        }

        protected void gvLoans_SelectedIndexChanged(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var record = svc.SelectLoanByID(gvLoans.SelectedRow.Cells[1].Text);
            lblLoanID.Text = record[0].LoanID.ToString();
            txtWorkstation.Text = record[0].WorkStation;
            ddlPatrons.SelectedValue = record[0].PatronID.ToString();
            ddlTools.SelectedValue = record[0].ToolID.ToString();
        }

        protected void btnDeleteLoan_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            svc.RemoveLoan(lblLoanID.Text);
            lblStatus.Text = "Deleted a loan";
            loadAll();
        }

        protected void btnLoanReturn_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            if (gvLoans.SelectedRow.Cells[6].Text.Equals(DateTime.MinValue.ToString()))
            {
                svc.UpdateLoan(lblLoanID.Text);
                lblStatus.Text = "Returned Loan " + lblLoanID.Text;
                loadAll();
            }
            else
            {
                lblStatus.CssClass = "alert-danger";
                lblStatus.Text = "This Loan has already been returned.";
            }
        }
    }
}