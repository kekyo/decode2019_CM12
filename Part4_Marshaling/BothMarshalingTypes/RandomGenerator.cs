using System;
using System.Runtime.InteropServices;

namespace BothMarshalingTypes
{
    public sealed class RandomGenerator
    {
        // extern "C" bool __stdcall GenerateData(uint8_t* pBuffer, uint32_t size);

        [DllImport("Win32NativeLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool GenerateData(byte[] buffer, uint size);

        [DllImport("Win32NativeLibrary.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool GenerateData(IntPtr pBuffer, uint size);

        public void Generate(byte[] buffer)
        {
            if (!GenerateData(buffer, (uint)buffer.Length))
            {
                throw new InvalidOperationException();
            }
        }

        public void Generate(IntPtr pBuffer, uint size)
        {
            if (!GenerateData(pBuffer, size))
            {
                throw new InvalidOperationException();
            }
        }
    }
}
