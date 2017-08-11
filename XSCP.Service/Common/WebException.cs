using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Service.Common
{
   public  class WebException: Exception
    { 
       public WebException():base(){
       
       }

       public WebException(string msg) : base(msg) { 
       
       }
 
       public WebException(string msg, Exception e) : base(msg, e) { 
       
       }
    }
}
