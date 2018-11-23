using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CWApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            //svc.InsertEmployee("emmanuel", "admin"); -- my one staff member
            /*svc.InsertBrands("Generic");
             svc.InsertBrands("Ryobi");
             svc.InsertBrands("Ridgid");
             svc.InsertBrands("Milwaukee");
             svc.InsertBrands("Makita");
             svc.InsertBrands("DeWalt");
             svc.InsertBrands("Bosch");
             svc.InsertBrands("Dremel");
             svc.InsertBrands("test");
            */
        }

        protected void ctlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var svc = new CommunityWorkshopService.CWDataServiceSoapClient();
            //bool validLogin = svc.ValidateUserCredentials(ctlLogin.UserName, ctlLogin.Password);
            bool validLogin = true;
            if (validLogin)
            {
                FormsAuthentication.RedirectFromLoginPage(ctlLogin.UserName, ctlLogin.RememberMeSet);
            }

        }
    }
}