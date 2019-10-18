using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    class StructWrapper : IDisposable
    {
        public IntPtr Ptr { get; private set; }
        public int Size { get; private set; }
        public StructWrapper(object obj)
        {
            Size = Marshal.SizeOf(obj);
            Ptr = Marshal.AllocHGlobal(Size);
            Win32Api.ZeroMemory(Ptr, Size);
            Marshal.StructureToPtr(obj, Ptr, false);
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
