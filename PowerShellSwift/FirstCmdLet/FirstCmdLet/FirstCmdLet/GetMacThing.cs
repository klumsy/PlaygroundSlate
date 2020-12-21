using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FirstCmdLet
{
    [StructLayout(LayoutKind.Sequential)]
    public class StatClass
    {
        public uint DeviceID;
        public uint InodeNumber;
        public uint Mode;
        public uint HardLinks;
        public uint UserID;
        public uint GroupID;
        public uint SpecialDeviceID;
        public ulong Size;
        public ulong BlockSize;
        public uint Blocks;
        public long TimeLastAccess;
        public long TimeLastModification;
        public long TimeLastStatusChange;
    }
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
        
        // The native callback takes a pointer to a struct. The below class
        // represents that struct in managed code.
        
        
        
        [DllImport("libSystem.dylib")]
        private static extern int getpid();
        
        
        private delegate int DirClbk(string fName, StatClass stat, int typeFlag);

        // Import the libc and define the method to represent the native function.
        [DllImport("libSystem.dylib")]
        private static extern int ftw(string dirpath, DirClbk cl, int descriptors);

        // Implement the above DirClbk delegate;
        // this one just prints out the filename that is passed to it.
        private static int DisplayEntry(string fName, StatClass stat, int typeFlag)
        {
            Console.WriteLine(fName);
            return 0;
        }

        public static void GetFiles()
        {
            // Call the native function.
            // Note the second parameter which represents the delegate (callback).
            ftw(".", DisplayEntry, 10);
            //next time make it a cmdlet, and create a rich wrapper to return rich object in PSObject
        }
        
        public static int GetThePid()
        {
            return getpid();
        }
    }
}