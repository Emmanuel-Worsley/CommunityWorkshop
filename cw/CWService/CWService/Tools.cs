﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWService
{
    public class Tools
    {
        public long ToolID { get; set; }
        public long BrandID{ get; set; }
        public string ToolType { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }

    }
}