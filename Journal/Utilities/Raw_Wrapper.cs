using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    class Raw_Array_Wrapper: IDisposable
    {
        public IntPtr Ptr { get; private set; }
        public int Size { get; private set; }
        public Raw_Array_Wrapper(int sizeinbytes)
        {
            Size = sizeinbytes;
            Ptr = Marshal.AllocHGlobal(Size);
            Win32Api.ZeroMemory(Ptr, Size);
        }

        public void Dispose()
        {
            if(Ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Ptr);
                Ptr = IntPtr.Zero;
            }

        }
    }
}
