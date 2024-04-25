using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func_reporting_demo
{
    public class ConfigurationSettings
    {
        public bool EnableMockAPI { get; set; }
        public string MockApiUrl { get; set; }
        public string PowerBIApiUrl { get; set; }
    }
}
