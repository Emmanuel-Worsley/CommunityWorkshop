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
        public static string export; // string used for exporting our csv files
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // if the page is not being loaded in response to a post back
            {
                loadTools(); // calls the load tools method
                loadPatronData(); // calls the loadPatronData method
                loadBrands(); // calls the loadBrands method
            }
           
        }
        /// <summary>
        /// Loads in data from tools and sets it to the tools drop down list
        /// </summary>
        public void loadTools()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var tools = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllTools())// foreach element of data sent
            {
                var obj = new { row.ToolID, row.ToolType};// add into obj the data recieved that you wish to use
                tools.Add(obj);// add data within obj to the list of objects
            }
            ddlTools.DataValueField = "ToolID"; // sets where the value of each item in the list comes from
            ddlTools.DataTextField = "ToolType"; // sets where the value of each item in the list comes fromm
            ddlTools.DataSource = tools; // sets the datasource of the drop-down list to the list of objects
            ddlTools.DataBind(); // binds the data to the drop-down list
        }
        /// <summary>
        /// Loads in data from brands and sets it to the brands drop down list
        /// </summary>
        public void loadBrands()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var brands = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllBrands())// foreach element of data sent
            {
                var obj = new { row.BrandID, row.BrandName };// add into obj the data recieved that you wish to use
                brands.Add(obj);// add data within obj to the list of objects
            }
            ddlBrands.DataValueField = "BrandID";// sets where the value of each item in the list comes from
            ddlBrands.DataTextField = "BrandName";// sets where the value of each item in the list comes from
            ddlBrands.DataSource = brands;// sets the datasource of the drop-down list to the list of objects
            ddlBrands.DataBind();// binds the data to the drop-down list
        }
        /// <summary>
        /// Loads in data from Patrons and sets it to the patron drop-down list
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
        /// Creates a Csv based on the data thats being put into the grid view
        /// </summary>
        /// <param name="data">The list that is being supplied to the current gridview</param>
        public void createCSV(List<object> data)

        {
            var csv = new StringBuilder(); // makes csv new string builder
            for (int i = 0; i < data.Count; i++) // for as long as i is less than the size of the supplied list
            {
                csv.Append(data[i].ToString()); // append on to the end of csv what ever data is in the list at entry corrosponding to i
                if(i == 0) // if i is equal to 0
                {
                    csv.Remove(csv.Length - (data[i].ToString().Length), 2); // removes first 2 from the csv
                }
                else
                {
                    csv.Remove(csv.Length - (data[i].ToString().Length - 1), 1); // removes first 2 from the csv
                    csv.Replace('{', ','); // replaces all { within csv with ,
                }
                csv.Remove(csv.Length - 2, 2); // removes last 2 from the csv
            }
            csv.Replace(", ", ","); // replaces all commas followed by a space with a comma
            export = csv.ToString(); // sets the global string export to equal csv
        }
        /// <summary>
        /// Prompts the user to save the csv file or download the csv file 
        /// </summary>
        /// <param name="csv">The csv that will be exported</param>
        public void exportCsv(string csv)
        {
            var response = HttpContext.Current.Response; // Create a HttpContext
            string attachment = "attachment; filename=Report.csv";
            response.ClearContent(); // Clears all content from the buffer stream
            response.Clear(); // Clears all content from the buffer stream
            response.ClearHeaders(); // Clears all headers from the bufferstream
            response.AddHeader("Content-Disposition", attachment); // Content-Disposition tells the browser to treat the file like a attachment      
            response.ContentType = "text/csv";
            response.Write(csv); // write the file out to user
            response.End(); // sends all output
        }
        /// <summary>
        /// Loads all tools that are currently on loan within the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckedOutTools_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient(); // make svc have access to WebMethods
            var tools = new List<object>();// creates a new list of objects
            foreach (var row in svc.SelectAllCheckedoutTools())// foreach element of data sent
            {
                var obj = new { row.ToolType, row.Comment, row.Active };// add into obj the data recieved that you wish to use
                tools.Add(obj);// add data within obj to the list of objects
            }
            gvShowReports.DataSource = tools;// sets the datasource of the gridview to the list of objects
            gvShowReports.DataBind();// binds the data to the gridview
            createCSV(tools); // calls the createCSV method and supplies the list of objects
        }
        /// <summary>
        /// Load all tools that are currently active or inactive within the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActiveTools_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var tools = new List<object>();// creates a new list of objects
            if (chkActiveCheck.Checked) // if active is checked
            {
                foreach (var row in svc.SelectAllActiveTools("1"))// foreach element of data sent
                {
                    var obj = new { row.ToolID, row.ToolType, row.Comment, row.Active };// add into obj the data recieved that you wish to use
                    tools.Add(obj);// add data within obj to the list of objects
                }
            }
            else
            {
                foreach (var row in svc.SelectAllActiveTools("0"))// foreach element of data sent
                {
                    var obj = new { row.ToolID, row.ToolType, row.Comment, row.Active };// add into obj the data recieved that you wish to use
                    tools.Add(obj);// add data within obj to the list of objects
                }
            }
            gvShowReports.DataSource = tools;// sets the datasource of the gridview to the list of objects
            gvShowReports.DataBind();// binds the data to the gridview
            createCSV(tools);// calls the createCSV method and supplies the list of objects
        }

        /// <summary>
        /// Exports the csv to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            if(gvShowReports.HeaderRow == null) // if the gridview has no data in it
            {
                lblStatus.CssClass = "alert-danger"; // change status class
                lblStatus.Text = "There is no data in the table to export"; // change status text
            }
            else
            {
                exportCsv(export); // calls the exportCsv method and supplies it with the csv
            }
            
            
        }

        /// <summary>
        /// Loads all tools within a brand that are either active or inactive in the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadBrandHistory_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var brands = new List<object>();// creates a new list of objects
            if (chkBrandsActiveCheck.Checked)// if active is checked
            {
                foreach (var row in svc.SelectAllActiveBrands("1", ddlBrands.SelectedItem.Text))// foreach element of data sent
                {
                    var obj = new { row.BrandName, row.ToolType, row.Comment };// add into obj the data recieved that you wish to use
                    brands.Add(obj);// add data within obj to the list of objects
                }
                gvShowReports.DataSource = brands;// sets the datasource of the gridview to the list of objects
                gvShowReports.DataBind();// binds the data to the gridview
            }
            else
            {
                foreach (var row in svc.SelectAllActiveBrands("0", ddlBrands.SelectedItem.Text))// foreach element of data sent
                {
                    var obj = new { row.BrandName, row.ToolType, row.Comment };// add into obj the data recieved that you wish to use
                    brands.Add(obj);// add data within obj to the list of objects
                }
                gvShowReports.DataSource = brands;// sets the datasource of the gridview to the list of objects
                gvShowReports.DataBind();// binds the data to the gridview
            }
            createCSV(brands);// calls the createCSV method and supplies the list of objects
        }
        /// <summary>
        /// Loads in all rental history of a patron and displays within the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadPatronHistory_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var patrons = new List<object>();// creates a new list of objects
            foreach (var row in svc.loadHistoryOfPatron(ddlPatrons.SelectedValue))// foreach element of data sent
            {
                var obj = new { row.PatronName, row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn };// add into obj the data recieved that you wish to use
                patrons.Add(obj);// add data within obj to the list of objects
            }
            gvShowReports.DataSource = patrons;// sets the datasource of the gridview to the list of objects
            gvShowReports.DataBind();// binds the data to the gridview
            createCSV(patrons);// calls the createCSV method and supplies the list of objects
        }
        /// <summary>
        /// Loads in all the rental history of a tool and displays within the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void loadToolHistory_Click(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();// make svc have access to WebMethods
            var tools = new List<object>();// creates a new list of objects
            foreach (var row in svc.loadHistoryOfTool(ddlTools.SelectedValue))// foreach element of data sent
            {
                var obj = new { row.ToolType, row.WorkStation, row.DateLoaned, row.DateReturn, row.PatronName };// add into obj the data recieved that you wish to use
                tools.Add(obj);// add data within obj to the list of objects
            }
            gvShowReports.DataSource = tools;// sets the datasource of the gridview to the list of objects
            gvShowReports.DataBind();// binds the data to the gridview
            createCSV(tools);// calls the createCSV method and supplies the list of objects
        }
    }
}