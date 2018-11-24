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
            loadLoanData();
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
    }
}