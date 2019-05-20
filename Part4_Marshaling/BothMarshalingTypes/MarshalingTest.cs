using System.Runtime.InteropServices;

namespace BothMarshalingTypes
{
    public static class MarshalingTest
    {
        public static void AutoMarshaling()
        {
            var generator = new RandomGenerator();

            var buffer = new byte[0x10];

            for (var index = 0L; index < 1000000000; index++)
            {
                generator.Generate(buffer);
            }
        }

        public static void ManualMarshaling()
        {
            var generator = new RandomGenerator();

            var buffer = new byte[0x10];
            var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                var pBuffer = bufferHandle.AddrOfPinnedObject();
                var size = (uint)buffer.Length;

                for (var index = 0L; index < 1000000000; index++)
                {
                    generator.Generate(pBuffer, size);
                }
            }
            finally
            {
                bufferHandle.Free();
            }
        }
    }
}
