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
            loadData();
        }

        public void loadData()
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            var brands = new List<object>();
            foreach (var row in svc.SelectAllBrands())
            {
                //var obj = new { row.BrandID, row.BrandName };
                brands.Add(row.BrandID + row.BrandName);
            }
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataSource = brands;
        }

        protected void gvToolList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDeleteTool_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateTool_Click(object sender, EventArgs e)
        {

        }

        protected void btnNewTool_Click(object sender, EventArgs e)
        {

        }
    }
}