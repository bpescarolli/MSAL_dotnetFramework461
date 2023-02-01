using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAL_dotnetFramework461
{
    public class Production
    {
        public string address { get; set; }
        public List<string> AllowedGroupsID { get; set; }
        public string ClientID { get; set; }
        public string TenantID { get; set; }
    }

    public class Test
    {
        /// <summary> 
        /// Endereços de teste 
        /// <para>address[0] = API 0016SP</para> 
        /// <para>address[1] = API 0014SP</para> 
        /// </summary> 
        public List<string> address { get; set; }
        public List<string> AllowedGroupsID { get; set; }
        public string ClientID { get; set; }
        public string TenantID { get; set; }
    }
}
