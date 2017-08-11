using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace XSCP.Core
{
    public class DirectoryUtility
    {
        /// <summary>
        /// Get the install path (not include "/")
        /// </summary>
        /// <returns></returns>
        public static string GetInstallDirectory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = assembly.CodeBase;
            path = path.Replace(@"file:///", "");
            int i = path.LastIndexOf('/');
            return path.Substring(0, i);
        }
    }
}
