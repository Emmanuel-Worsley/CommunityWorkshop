using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWService
{
    public class AllData
    {
        public long BrandID { get; set; }
        public string BrandName { get; set; }

        public long ToolID { get; set; }
        public string ToolType { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }

        public long PatronID { get; set; }
        public string PatronName { get; set; }
        public string ContactNumber { get; set; }

        public long LoanID { get; set; }
        public DateTime DateLoaned { get; set; }
        public DateTime DateReturn { get; set; }
        public string WorkStation { get; set; }


        public long EmployeeID { get; set; }
        public string StaffName { get; set; }
        public string StaffPin { get; set; }
    }
}