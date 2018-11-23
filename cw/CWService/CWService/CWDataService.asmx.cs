using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Dapper;

namespace CWService
{
    /// <summary>
    /// Summary description for CWDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CWDataService : System.Web.Services.WebService
    {
        #region Tools
        [WebMethod]
        public List<Tools> SelectAllTools ()
        {
            var query = "SELECT * FROM Tools";
            return Model.Database.GetConnection().Query<Tools>(query).ToList();
        }
        [WebMethod]
        public List<Tools> SelectToolsByID(string ID)
        {
            var query = $"SELECT * FROM Tools WHERE ToolID = {ID}";
            return Model.Database.GetConnection().Query<Tools>(query).ToList();
        }

        [WebMethod]
        public void InsertTool(string brandID, string toolType, string comment, int active)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"INSERT INTO Tools (BrandID, ToolType, Comment, Active) VALUES ({brandID}, {toolType}, {comment}, {active})";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void UpdateTool(string brandID, string description, int active, string toolID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"UPDATE Tools SET BrandID = {brandID}, Description = {description}, Active = {active} WHERE ToolID = {toolID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void RemoveTool(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM Tools WHERE ToolID = {ID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion

        #region Brands
        [WebMethod]
        public List<Brands> SelectAllBrands()
        {
            var query = "SELECT * FROM Brands";
            return Model.Database.GetConnection().Query<Brands>(query).ToList();
        }

        [WebMethod]
        public List<Brands> SelectBrandsByID(string ID)
        {
            var query = $"SELECT * FROM Brands WHERE BrandID = {ID}";
            return Model.Database.GetConnection().Query<Brands>(query).ToList();
        }

        [WebMethod]
        public void InsertBrands(string BrandName)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = "INSERT INTO Brands (BrandName) VALUES (@brandName)";
                    var param = new { brandName = BrandName };
                    var results = db.Execute(query, param, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void UpdateBrand(string brandName, string brandID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"UPDATE Brands SET BrandName = {brandName} WHERE BrandID = {brandID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void RemoveBrand(string brandID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM Brands WHERE BrandID = {brandID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        #endregion

        #region Patrons

        [WebMethod]
        public List<Patrons> SelectAllPatrons()
        {
            var query = "SELECT * FROM Patrons";
            return Model.Database.GetConnection().Query<Patrons>(query).ToList();
        }

        [WebMethod]
        public List<Patrons> SelectPatronsByID(string ID)
        {
            var query = $"SELECT * FROM Patrons WHERE PatronID = {ID}";
            return Model.Database.GetConnection().Query<Patrons>(query).ToList();
        }

        [WebMethod]
        public void InsertPatrons(string PatronName, string contactNumber)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"INSERT INTO Patrons (PatronName, ContactNumber) VALUES ({PatronName}, {contactNumber})";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void UpdatePatron(string patronName, string contactNumber, string patronID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"UPDATE Patrons SET PatronName = {patronName}, ContactNumber = {contactNumber}, WHERE BrandID = {patronID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void RemovePatron(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM Patrons WHERE PatronID = {ID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion

        #region Employees


        /*
         * Used to encrypt the password of users 
        */
        private static string Encrypt(String Data)
        {
            const string ENCRYPTION_KEY = "5w1p3r n0 5w1p1n6"; // to be added to the start and end of the supplied password
            string saltedPassword = ENCRYPTION_KEY + Data + ENCRYPTION_KEY; // adds key infront of the supplied password
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(saltedPassword); // creates an array of bytes from the saltedPassword
            bytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytes); // hashes whats stored in bytes and puts it back into bytes
            string hashed = System.Text.Encoding.ASCII.GetString(bytes); // takes the hashed bytes and turns it back into a string
            return hashed; // returns the now hashed password
        }

        /*
        * Checks to see if the username and password inputted are correct
        */
        [WebMethod]
        public static bool ValidateUserCredentials(string user, string pass)
        {
            using (var db = Model.Database.GetConnection())
            {
                var query = "SELECT COUNT (*) FROM Employees WHERE StaffName = @StaffName AND StaffPin = @StaffPin";
                var param = new { StaffName = user, StaffPin = Encrypt(pass) };
                var results = (long)db.ExecuteScalar(query, param);
                return results > 0;
            }
        }

        [WebMethod]
        public List<Employees> SelectAllEmployees()
        {
            var query = "SELECT * FROM Employees";
            return Model.Database.GetConnection().Query<Employees>(query).ToList();
        }

        [WebMethod]
        public List<Employees> SelectEmployeesByID(string ID)
        {
            var query = $"SELECT * FROM Employees WHERE EmployeeID = {ID}";
            return Model.Database.GetConnection().Query<Employees>(query).ToList();
        }

        [WebMethod]
        public void InsertEmployee(string StaffName, string StaffPin)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = "INSERT INTO Employees (StaffName, StaffPin) VALUES (@staffName, @staffPin)";
                    var param = new { staffName = StaffName, staffPin = Encrypt(StaffPin) };
                    var results = db.Execute(query, param, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void UpdateEmployee(string StaffName, string StaffPin, string EmployeeID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"UPDATE Employees SET StaffName = {StaffName}, StaffPin = {StaffPin}, WHERE EmployeeID = {EmployeeID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void RemoveEmployee(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM Employees WHERE EmployeeID = {ID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion

        #region Loans
        [WebMethod]
        public List<Loans> SelectAllLoans()
        {
            var query = "SELECT * FROM Loans";
            return Model.Database.GetConnection().Query<Loans>(query).ToList();
        }

        [WebMethod]
        public List<Loans> SelectLoanByID(string ID)
        {
            var query = $"SELECT * FROM Loans WHERE LoanID = {ID}";
            return Model.Database.GetConnection().Query<Loans>(query).ToList();
        }

        [WebMethod]
        public void InsertLoan(string PatronID, string ToolID, string EmployeeID, string WorkStation)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var time = DateTime.Now.Day;
                    var query = $"INSERT INTO Loans (PatronID, ToolID, EmployeeID, WorkStation, DateLoaned) VALUES ({PatronID}, {ToolID}, {EmployeeID}, {WorkStation}, {time})";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void UpdateLoan(string LoanID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var time = DateTime.Now.Day;
                    var query = $"UPDATE Loans SET DateReturn = {time} WHERE BrandID = {LoanID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        [WebMethod]
        public void RemoveLoan(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM Loans WHERE LoanID = {ID}";
                    var results = db.Execute(query, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion
    }
}
