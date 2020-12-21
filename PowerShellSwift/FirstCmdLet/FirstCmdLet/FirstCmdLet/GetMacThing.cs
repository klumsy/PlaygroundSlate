using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FirstCmdLet
{
    public class GetMacThingy
    {
        public int ReturnSomething()
        {
            return 1;
        }
        
        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            if (nativeUtf8 != IntPtr.Zero)
            {
                int len = 0;
                while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
                byte[] buffer = new byte[len];
                Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            } else {
                return null;
            }
        }
        [DllImport("libSystem.dylib")]
        private static extern int getpid();
        
        public static int GetThePid()
        {
            return getpid();
        }
    }
}