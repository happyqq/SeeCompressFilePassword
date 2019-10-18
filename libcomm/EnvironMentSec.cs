using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace libcomm
{
   
    public static class EnvironMentSec
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern  bool IsDebuggerPresent();

        public static bool isDebugger()
        {
            return IsDebuggerPresent();
        }
    }
}
