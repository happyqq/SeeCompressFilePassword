using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace libcomm
{
   public class Internet
    {
        [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        private static extern bool InternetGetConnectedState(ref IntPtr lpdwFlags, int dwReserved);
        private  const int MODEM = 1;
        private  const int LAN = 2;
        private  const int PROXY = 4;
        public static bool InternetConnected()//返回连接状态
        {
            IntPtr p = new IntPtr(MODEM & LAN & PROXY);
            return InternetGetConnectedState(ref p, 0);
        }
        public static string InternetState()//返回连接模式
        {
            IntPtr p = IntPtr.Zero;
            InternetGetConnectedState(ref p, 0);
            switch (p.ToInt32() & MODEM)
            {
                case MODEM:
                    return "本机连接互联网的方式：MODEM";
                case LAN:
                    return "本机连接互联网的方式：LAN";
                case PROXY:
                    return "本机连接互联网的方式：PROXY";
                default:
                    return "本机连接互联网的方式：UNKNOWN";
            }
        }
    }
}
