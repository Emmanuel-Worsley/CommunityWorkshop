using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWService
{
    public class Loans
    {
        public long LoanID { get; set; }
        public long PatronID { get; set; }
        public long ToolID { get; set; }
        public long EmployeeID { get; set; }
        public string WorkStation { get; set; }
        public DateTime DateLoaned { get; set; }
        public DateTime DateReturned { get; set; }
    }
}