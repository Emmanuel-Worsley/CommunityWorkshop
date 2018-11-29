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
        /// <summary>
        /// Selects all tools and brands from the database
        /// </summary>
        /// <returns>
        /// A list of tools and brands
        /// </returns>
        [WebMethod]
        public List<AllData> SelectAllToolsAndBrand()
        {
            var query = "SELECT Brands.BrandName, Tools.* FROM Brands INNER JOIN Tools on Brands.BrandID = Tools.BrandID;";
            return Model.Database.GetConnection().Query<AllData>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all Tools from the database
        /// </summary>
        /// <returns>
        /// A list of tools
        /// </returns>

        [WebMethod]
        public List<Tools> SelectAllTools()
        {
            var query = "SELECT * FROM Tools";
            return Model.Database.GetConnection().Query<Tools>(query).ToList();// runs the query and and places a result into a list which is then returned when called
        }

        /// <summary>
        /// Selects all Tools from the database
        /// </summary>
        /// <returns>
        /// A list of tools
        /// </returns>
        [WebMethod]
        public List<Tools> SelectToolsByID(string ID)
        {
            var query = "SELECT * FROM Tools WHERE ToolID = @ToolID";
            var param = new { ToolID = ID }; // takes the parameter and stores it
            return Model.Database.GetConnection().Query<Tools>(query, param).ToList();// runs the query and then supplies param and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Inserts data into the Tools table based on the parameters supplied
        /// </summary>
        /// <param name="brandID">The ID of the brand being placed into the table</param>
        /// <param name="toolType">The type of tool being placed into the table</param>
        /// <param name="comment">A comment on the tools condition being placed into the table</param>
        /// <param name="active">Whether the tool is active or not being placed into the table</param>
        [WebMethod]
        public void InsertTool(string brandID, string toolType, string comment, int active)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction 
            {
                try
                {
                    var query = "INSERT INTO Tools (BrandID, ToolType, Comment, Active) VALUES (@BrandID, @ToolType, @Comment, @Active)";
                    var param = new { BrandID = brandID, ToolType = toolType, Comment = comment, Active = active };  // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); // writes the exception caught to the console
                    transaction.Rollback(); // rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Updates a tools data based on the ToolID
        /// </summary>
        /// <param name="brandID">The updated BrandID</param>
        /// <param name="toolType">The updated ToolType</param>
        /// <param name="comment">The updated Comment</param>
        /// <param name="active">Updated if active or not</param>
        /// <param name="toolID">Which tool is being updated</param>
        [WebMethod]
        public void UpdateTool(string brandID, string toolType, string comment, int active, string toolID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "UPDATE Tools SET BrandID = @BrandID, ToolType = @ToolType, Comment = @Comment, Active = @Active WHERE ToolID = @ToolID";
                    var param = new { BrandID = brandID, ToolType = toolType, Comment = comment, Active = active, ToolID = toolID };// takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex); // writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Delets a tool from the database based on the supplied ToolID
        /// </summary>
        /// <param name="ID">The ID of the tool that is to be deleted</param>
        [WebMethod]
        public void RemoveTool(string ID)
        { 
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "DELETE FROM Tools WHERE ToolID = @id";
                    var param = new { id = ID }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); // writes the exception caught to the console
                    transaction.Rollback(); // rollsback the transaction so the database isn't updated
                }
            }
        }
        #endregion

        #region Brands
        /// <summary>
        /// Selects all Brands from the Brands Table in the database
        /// </summary>
        /// <returns>
        /// A list of all data stored in Brands
        /// </returns>
        [WebMethod]
        public List<Brands> SelectAllBrands()
        {
            var query = "SELECT * FROM Brands";
            return Model.Database.GetConnection().Query<Brands>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all data on a brand based on a BrandID supplied
        /// </summary>
        /// <param name="ID">The ID supplied for the brand we want</param>
        /// <returns></returns>
        [WebMethod]
        public List<Brands> SelectBrandsByID(string ID)
        {
            var query = "SELECT * FROM Brands WHERE BrandID = @id";
            var param = new { id = ID }; // takes the parameter and stores it
            return Model.Database.GetConnection().Query<Brands>(query, param).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        ///  Inserts data into the Brand Table
        /// </summary>
        /// <param name="BrandName">The name of the brand to be inserted</param>
        [WebMethod]
        public void InsertBrands(string BrandName)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "INSERT INTO Brands (BrandName) VALUES (@brandName)";
                    var param = new { brandName = BrandName }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); // writes the exception caught to the console
                    transaction.Rollback(); // rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Update a Brands Data based on supplied BrandID
        /// </summary>
        /// <param name="brandName">The data to be updated</param>
        /// <param name="brandID">The ID of the brand that is getting updated</param>
        [WebMethod]
        public void UpdateBrand(string brandName, string brandID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = $"UPDATE Brands SET BrandName = @BrandName WHERE BrandID = @BrandID";
                    var param = new { BrandName = brandName, BrandID = brandID }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); // writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Delete a brands data based on supplied BrandID
        /// </summary>
        /// <param name="brandID">The supplied BrandID to be deleted</param>
        [WebMethod]
        public void RemoveBrand(string brandID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = $"DELETE FROM Brands WHERE BrandID = @BrandID";
                    var param = new { BrandID = brandID };// takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }

        #endregion

        #region Patrons
        /// <summary>
        /// Selects all data in the Patrons table
        /// </summary>
        /// <returns>
        /// A list of Data from Patrons
        /// </returns>
        [WebMethod]
        public List<Patrons> SelectAllPatrons()
        {
            var query = "SELECT * FROM Patrons";
            return Model.Database.GetConnection().Query<Patrons>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all data on a patron based on the supplied ID
        /// </summary>
        /// <param name="ID">The ID of the patron</param>
        /// <returns></returns>
        [WebMethod]
        public List<Patrons> SelectPatronsByID(string ID)
        {
            var query = "SELECT * FROM Patrons WHERE PatronID = @id";
            var param = new { id = ID }; // takes the parameter and stores it
            return Model.Database.GetConnection().Query<Patrons>(query, param).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Inserts a new entry into the Patron Table
        /// </summary>
        /// <param name="PatronName">The name of new patron</param>
        /// <param name="contactNumber">The contact number of the new patron</param>
        [WebMethod]
        public void InsertPatrons(string PatronName, string contactNumber)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "INSERT INTO Patrons (PatronName, ContactNumber) VALUES (@patronName, @ContactNumber)";
                    var param = new { patronName = PatronName, ContactNumber = contactNumber }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Updates the data of a patron based upon supplied ID
        /// </summary>
        /// <param name="patronName">New data of the patron name</param>
        /// <param name="contactNumber">New data of the contact number</param>
        /// <param name="patronID">The ID of the patron to be updated</param>
        [WebMethod]
        public void UpdatePatron(string patronName, string contactNumber, string patronID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "UPDATE Patrons SET PatronName = @PatronName, ContactNumber = @ContactNumber WHERE PatronID = @PatronID";
                    var param = new { PatronName = patronName, ContactNumber = contactNumber, PatronID = patronID };// takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Deletes a patron from the database based upon supplied PatronID
        /// </summary>
        /// <param name="ID">The ID of the patron to be deleted</param>
        [WebMethod]
        public void RemovePatron(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "DELETE FROM Patrons WHERE PatronID = @id";
                    var param = new { id = ID };// takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        #endregion

        #region Employees
        //Most of these aren't currently in use placed them here if the client requests a way to update and insert employee data inside the web application


        
        /// <summary>
        /// Encrypts the users password by salting it and then hashing it
        /// </summary>
        /// <param name="Data">The password being supplied for encryption</param>
        /// <returns></returns>
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
        /// <summary>
        /// Confirms if the supplied username and password matches any within the database and returns true or false
        /// </summary>
        /// <param name="user">the username being entered</param>
        /// <param name="pass">the password being entered</param>
        /// <returns></returns>
        [WebMethod]
        public bool confirmAccess(string user, string pass) // was having difficulty getting this to be called turns out WebMethods outside of a WebService are meant to be static inside a WebService can't be static
        {
            using (var db = Model.Database.GetConnection()) // makes db an instance of database.getconnection
            {
                var query = "SELECT COUNT (*) FROM Employees WHERE StaffName = @StaffName AND StaffPin = @StaffPin";
                var param = new { StaffName = user, StaffPin = Encrypt(pass) }; // takes the parameters and stores it
                var results = (long)db.ExecuteScalar(query, param); // runs the query and then supplies param
                return results > 0; // returns results if there is more than 0
            }
        }
        /// <summary>
        /// Selects all employees from the Employees table
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Employees> SelectAllEmployees()
        {
            var query = "SELECT * FROM Employees";
            return Model.Database.GetConnection().Query<Employees>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all data for an employee based on the supplied EmployeeID
        /// </summary>
        /// <param name="ID">The ID of the employee data required</param>
        /// <returns></returns>
        [WebMethod]
        public List<Employees> SelectEmployeesByID(string ID)
        {
            var query = "SELECT * FROM Employees WHERE EmployeeID = @id";
            var param = new { id = ID }; // takes the parameters and stores it 
            return Model.Database.GetConnection().Query<Employees>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects an employee based on the name provided
        /// </summary>
        /// <param name="Name">The name of the employee</param>
        /// <returns></returns>
        [WebMethod]
        public List<Employees> SelectEmployeesByName(string Name)
        {
            var query = "SELECT * FROM Employees WHERE StaffName = @name";
            var param = new { name = Name }; // takes the parameters and stores it 
            return Model.Database.GetConnection().Query<Employees>(query, param).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Inserts a new Employee into the Employee table
        /// </summary>
        /// <param name="StaffName">The name of the employee</param>
        /// <param name="StaffPin">The password for the employee</param>
        [WebMethod]
        public void InsertEmployee(string StaffName, string StaffPin)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "INSERT INTO Employees (StaffName, StaffPin) VALUES (@staffName, @staffPin)";
                    var param = new { staffName = StaffName, staffPin = Encrypt(StaffPin) }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Updates data on an employee based on EmployeeID
        /// </summary>
        /// <param name="StaffName">Updated name</param>
        /// <param name="StaffPin">Updated Password</param>
        /// <param name="EmployeeID">The ID of the employee being updated</param>
        [WebMethod]
        public void UpdateEmployee(string StaffName, string StaffPin, string EmployeeID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = $"UPDATE Employees SET StaffName = @staffName, StaffPin = @staffPin, WHERE EmployeeID = @employeeID";
                    var param = new { staffName = StaffName, staffPin = Encrypt(StaffPin), employeeID = EmployeeID }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Delete an employee from the database based on ID provided
        /// </summary>
        /// <param name="ID">ID of the employee that is being removed</param>
        [WebMethod]
        public void RemoveEmployee(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = $"DELETE FROM Employees WHERE EmployeeID = @id";
                    var param = new { id = ID }; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        #endregion

        #region Loans
        /// <summary>
        /// Selects all tools that are currently not on loan and active
        /// </summary>
        /// <returns>A list of tools data that are currently active and not on loan </returns>
        [WebMethod]
        public List<Tools> SelectAllActiveToolsAndNotOnLoan()
        {
            // Old query that was broken
            //var query = "SELECT Tools.ToolType, Tools.ToolID, Tools.Comment, Tools.Active FROM Tools LEFT JOIN Loans ON Loans.ToolID = Tools.ToolID WHERE Tools.Active = @Active AND Loans.DateLoaned IS NULL AND Loans.DateReturn IS NULL;";
            var query = "SELECT Tools.ToolType, Tools.ToolID, Tools.Comment, Tools.Active FROM Tools WHERE Tools.ToolID NOT IN(SELECT Tools.ToolID FROM Loans INNER JOIN Tools ON Tools.ToolID = Loans.ToolID WHERE Loans.DateReturn IS NULL)  AND Tools.Active = 1";
            return Model.Database.GetConnection().Query<Tools>(query).ToList(); // runs the query and then places the result into a list which is then returned when called
        }

        /// <summary>
        /// Selects all data on loans from multiple tables by utilising inner joins
        /// </summary>
        /// <returns>Returns a list of the data pulled from the database</returns>
        [WebMethod]
        public List<AllData> SelectAllLoanData()
        {
            var query = "SELECT Loans.*, Employees.StaffName, Tools.ToolType, Patrons.PatronName FROM Tools INNER JOIN (Patrons INNER JOIN " +
                "(Employees INNER JOIN Loans ON Employees.EmployeeID = Loans.EmployeeID) ON Patrons.PatronID = Loans.PatronID) ON Tools.ToolID = Loans.ToolID";
            return Model.Database.GetConnection().Query<AllData>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all data from the Loans Table
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Loans> SelectAllLoans()
        {
            var query = "SELECT * FROM Loans";
            return Model.Database.GetConnection().Query<Loans>(query).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects data on a loan based on supplied ID
        /// </summary>
        /// <param name="ID">The ID of the loan that we want data from</param>
        /// <returns></returns>
        [WebMethod]
        public List<Loans> SelectLoanByID(string ID)
        {
            var query = $"SELECT * FROM Loans WHERE LoanID = @id";
            var param = new { id = ID }; // takes the parameters and stores it
            return Model.Database.GetConnection().Query<Loans>(query, param).ToList(); // runs the query then supplies the param and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Inserts a new loan into the Loans table
        /// </summary>
        /// <param name="PatronID">The ID of the patron using the tool</param>
        /// <param name="ToolID">The ID of the tool being loaned</param>
        /// <param name="EmployeeID">The ID of the employee who did the loan</param>
        /// <param name="WorkStation">Where the tool is being loaned to</param>
        [WebMethod]
        public void InsertLoan(string PatronID, string ToolID, string EmployeeID, string WorkStation)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var time = DateTime.Now; // create a new DateTime and sets it to the current time of when its created
                    var query = $"INSERT INTO Loans (PatronID, ToolID, EmployeeID, WorkStation, DateLoaned) VALUES (@patronID, @toolID, @employeeID, @workStation, {time})";
                    var param = new { patronID = PatronID, toolID = ToolID, employeeID = EmployeeID, workstation = WorkStation}; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the database
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Returns a loan based on the ID provided
        /// </summary>
        /// <param name="ID"></param>
        [WebMethod]
        public void ReturnLoan(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn()) // makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction()) // makes transaction an instance of beining a transaction
            {
                try
                {
                    var time = DateTime.Now; // create a new DateTime and sets it to the current time of when its created
                    var query = $"UPDATE Loans SET DateReturn = {time} WHERE LoanID = @LoanID";
                    var param = new { LoanID = ID}; // takes the parameters and stores it
                    var results = db.Execute(query, param, transaction); // runs the query and then supplies param and starts the transaction
                    transaction.Commit(); // commits the transaction to the databas
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        /// <summary>
        /// Deletes a loan from the database based on the LoanID provided
        /// </summary>
        /// <param name="ID">ID of the loan that is being deleted</param>
        [WebMethod]
        public void RemoveLoan(string ID)
        {
            using (var db = Model.Database.GetConnection().OpenAndReturn())// makes db an instance of database.getconnection
            using (var transaction = db.BeginTransaction())// makes transaction an instance of beining a transaction
            {
                try
                {
                    var query = "DELETE FROM Loans WHERE LoanID = @id";
                    var param = new { id = ID };// takes the parameters and stores it
                    var results = db.Execute(query, param, transaction);// runs the query and then supplies param and starts the transaction
                    transaction.Commit();// commits the transaction to the databas
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);// writes the exception caught to the console
                    transaction.Rollback();// rollsback the transaction so the database isn't updated
                }
            }
        }
        #endregion

        #region Report Queries
        /// <summary>
        /// Selects all Tools that are either active or inactive based on suppled parameter 
        /// </summary>
        /// <param name="active">Data saying whether the tool is active or not</param>
        /// <returns>A list of data on tools that are either active or inactive</returns>
        [WebMethod]
        public List<Tools> SelectAllActiveTools(string active)
        {
            var query = "SELECT Tools.ToolID, Tools.ToolType, Tools.Comment, Tools.Active FROM Tools WHERE Tools.Active = @active";
            var param = new { Active = active }; // takes the parameters and stores it
            return Model.Database.GetConnection().Query <Tools>(query, param).ToList(); // runs the query and and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all tools that are currently on loan
        /// </summary>
        /// <returns>A list of data on tools that are currently on loan</returns>
        [WebMethod]
        public List<Tools> SelectAllCheckedoutTools()
        {
            var query = "SELECT Tools.ToolType, Tools.ToolID, Tools.Comment, Tools.Active, Loans.ToolID FROM Loans INNER JOIN Tools ON Tools.ToolID = Loans.ToolID WHERE Loans.DateReturn IS NULL;";
            return Model.Database.GetConnection().Query<Tools>(query).ToList(); // runs the query and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Loads history of a Tools loan from multiple tables based on the ToolID of the tool
        /// </summary>
        /// <param name="id">The ID of the tool that the history is being loaded of</param>
        /// <returns>A list of data from multiple tables relevant to the tools loan history</returns>
        [WebMethod]
        public List<AllData> loadHistoryOfTool(string id)
        {
            var query = "SELECT Tools.ToolType, Loans.WorkStation, Loans.DateLoaned, Loans.DateReturn, Patrons.PatronName FROM Tools INNER JOIN (Loans INNER JOIN Patrons ON Loans.PatronID = Patrons.PatronID) ON Loans.ToolID = Tools.ToolID " +
                "WHERE Tools.ToolID = @ToolID";
            var param = new { ToolID = id }; // takes the parameters and stores it
            return Model.Database.GetConnection().Query<AllData>(query, param).ToList(); // runs the query and then the param and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Loads loan History of a Patron from mutliple Tables pased upon Supplied Patron
        /// </summary>
        /// <param name="id">The ID of the patron the loan history is being loaded for</param>
        /// <returns>A list of data from multiple tables relevant to the patrons loan history</returns>
        [WebMethod]
        public List<AllData> loadHistoryOfPatron(string id)
        {
            var query = "SELECT Tools.ToolType, Loans.WorkStation, Loans.DateLoaned, Loans.DateReturn, Patrons.PatronName FROM Tools INNER JOIN (Loans INNER JOIN Patrons ON Loans.PatronID = Patrons.PatronID) ON Loans.ToolID = Tools.ToolID " +
                "WHERE Patrons.PatronID = @PatronID";
            var param = new { PatronID = id }; // takes the parameters and stores it
            return Model.Database.GetConnection().Query<AllData>(query, param).ToList(); // runs the query and then the param and places a result into a list which is then returned when called
        }
        /// <summary>
        /// Selects all tools that are active or inactive based on supplied data. Aswell as the brand name based on the supplied data.
        /// </summary>
        /// <param name="active">If the tool is active or not</param>
        /// <param name="brandName">The name of the brand we are selecting from</param>
        /// <returns></returns>
        [WebMethod]
        public List <AllData> SelectAllActiveBrands(string active, string brandName)
        {
            var query = "SELECT Tools.Active, Tools.ToolType, Tools.Comment, Brands.BrandName FROM Tools LEFT JOIN Brands ON Tools.BrandID = Brands.BrandID WHERE Brands.BrandName = @BrandName AND Tools.Active = @Active ORDER BY Tools.ToolType";
            var param = new { Active = active, BrandName = brandName }; // takes the parameters and stores it
            return Model.Database.GetConnection().Query<AllData>(query, param).ToList(); // runs the query and then the param and places a result into a list which is then returned when called
        }
        #endregion
    }
}
